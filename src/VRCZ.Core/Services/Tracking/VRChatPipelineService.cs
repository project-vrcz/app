using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Kiota.Abstractions.Serialization;
using VRCZ.Core.Exceptions;
using VRCZ.Core.Exceptions.Pipeline;
using VRCZ.Core.Messages;
using VRCZ.Core.Models;
using VRCZ.Core.Models.VRChat.WebSocket;
using VRCZ.Core.Models.VRChat.WebSocket.Payload;
using VRCZ.Core.Services.Profile;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Services.Tracking;

public class VRChatPipelineService(
    HttpClient httpClient,
    CurrentUserProfileService currentUserProfileService,
    MessengerService messengerService,
    ILogger<VRChatPipelineService> logger)
{
    public PipelineStatus Status { get; private set; }

    private bool _requestDisconnect;

    private ClientWebSocket _webSocket = new();

    private CancellationTokenSource? _receiveLoopCancellationTokenSource;

    public async Task ConnectAsync()
    {
        if (_receiveLoopCancellationTokenSource is not null)
        {
            await _receiveLoopCancellationTokenSource.CancelAsync();
            _receiveLoopCancellationTokenSource.Dispose();
        }

        await ConnectAsyncInternal();

        _receiveLoopCancellationTokenSource = new CancellationTokenSource();

        _ = Task.Run(() => StartReceiveLoop(_receiveLoopCancellationTokenSource.Token),
            _receiveLoopCancellationTokenSource.Token);
    }

    private async Task ConnectAsyncInternal()
    {
        logger.LogInformation("Requesting Pipeline WebSocket connection");

        var authToken = currentUserProfileService.GetAuthCookie();

        if (authToken is null)
            throw new InvalidOperationException("Auth token is null");

        var webSocketUriBuilder = new UriBuilder(new Uri("wss://pipeline.vrchat.cloud"));

        var query = HttpUtility.ParseQueryString(webSocketUriBuilder.Query);
        query.Add("authToken", authToken);
        webSocketUriBuilder.Query = query.ToString();

        var webSocketUri = webSocketUriBuilder.Uri;

        await DisconnectAsync();

        _requestDisconnect = false;

        logger.LogInformation("Connecting to Pipeline WebSocket");

        await _webSocket.ConnectAsync(webSocketUri, httpClient, CancellationToken.None);

        logger.LogInformation("Connected to Pipeline WebSocket");
    }

    public async Task DisconnectAsync()
    {
        _requestDisconnect = true;

        if (_receiveLoopCancellationTokenSource is not null)
        {
            await _receiveLoopCancellationTokenSource.CancelAsync();
            _receiveLoopCancellationTokenSource.Dispose();
        }

        if (_webSocket.State is WebSocketState.Open or WebSocketState.CloseReceived or WebSocketState.CloseSent)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closed", CancellationToken.None);
        }

        ResetWebSocket();
    }

    private void ResetWebSocket()
    {
        _webSocket.Dispose();
        _webSocket = new ClientWebSocket();
    }

    private async Task StartReceiveLoop(CancellationToken cancellationToken)
    {
        while (!_requestDisconnect && !cancellationToken.IsCancellationRequested)
        {
            try
            {
                await ReceiveLoop(cancellationToken);
            }
            catch (Exception ex)
            {
                if (!_requestDisconnect)
                {
                    logger.LogError(ex, "WebSocket exception occurred");
                }
                else if (ex is not OperationCanceledException)
                {
                    logger.LogError(ex, "WebSocket Disconnect, but a unexpected exception occurred");
                    return;
                }
            }

            if (_requestDisconnect)
            {
                logger.LogInformation("WebSocket disconnected");
                return;
            }

            logger.LogError("WebSocket unexpected disconnect, Reconnecting in 5 seconds...");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }

                ResetWebSocket();

                try
                {
                    await ConnectAsyncInternal();
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to reconnect, Retrying in 5 seconds...");
                }
            }
        }
    }

    private async Task ReceiveLoop(CancellationToken cancellationToken)
    {
        var buffer = new byte[10240];
        var memoryBuffer = new Memory<byte>(buffer);

        var stream = new MemoryStream();

        try
        {
            while (_webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                var result = await _webSocket.ReceiveAsync(memoryBuffer, cancellationToken);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    logger.LogDebug("WebSocket Close message received");

                    await stream.DisposeAsync();
                    break;
                }

                await stream.WriteAsync(memoryBuffer[..result.Count], cancellationToken);

                if (!result.EndOfMessage) continue;

                stream.Position = 0;
                VRChatWebSocketEvent eventResult;
                try
                {
                    var eventNode = await JsonNode.ParseAsync(stream, cancellationToken: cancellationToken) ??
                                    throw new NullWebSocketEventException();
                    if ((string?)eventNode["err"] is not null)
                    {
                        var error = eventNode.Deserialize(VRChatWebSocketEventContext.Default
                            .VRChatWebSocketErrorEvent) ?? throw new NullWebSocketEventException();

                        logger.LogError("Received Pipeline Error Event: {Message}", error.ErrorMessage);
                        break;
                    }

                    eventResult =
                        eventNode.Deserialize(VRChatWebSocketEventContext.Default
                            .VRChatWebSocketEvent) ?? throw new NullWebSocketEventException();
                }
                catch (Exception ex)
                {
                    var content = await GetStreamContentAsync(stream, cancellationToken);
                    logger.LogError(ex, "Failed to parse event");
                    logger.LogTrace("Raw event: {Raw}", content);
                    break;
                }

                try
                {
                    await HandleEventAsync(eventResult);
                }
                catch (UnknownWebSocketEventTypeException ex)
                {
                    logger.LogWarning("Unknown WebSocket event type: {Type}", ex.EventType);
                    logger.LogTrace("Raw event: {Raw}", eventResult.Content);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to handle event, Type: {Type}", eventResult.Type);
                    logger.LogTrace("Raw event: {Raw}", eventResult.Content);
                }

                stream.Position = 0;
                stream.SetLength(0);
            }
        }
        finally
        {
            await stream.DisposeAsync();
        }
    }

    private static async Task<string> GetStreamContentAsync(Stream stream, CancellationToken cancellationToken)
    {
        stream.Position = 0;
        using var reader = new StreamReader(stream, Encoding.UTF8);
        return await reader.ReadToEndAsync(cancellationToken);
    }

    private async Task HandleEventAsync(VRChatWebSocketEvent eventResult)
    {
        switch (eventResult.Type)
        {
            case "friend-add":
            case "friend-delete":
            case "friend-online":
            case "friend-active":
            case "friend-offline":
            case "friend-update":
            case "friend-location":
            case "group-joined":
            case "group-left":
            case "group-member-update":
            case "group-role-update":
            case "instance-queue-joined":
            case "instance-queue-ready":
            case "response-notification":
            case "user-update":
            case "user-location":
            case "user-badge-assigned":
            case "user-badge-unassigned":
            case "content-refresh":
                var jsonTypeInfo = GetPayloadJsonTypeInfo(eventResult.Type);
                var contentNodes = JsonNode.Parse(eventResult.Content!);

                if (contentNodes?.Deserialize(jsonTypeInfo) is not VRChatWebSocketPayloadBase payload)
                {
                    logger.LogWarning("Failed to parse payload, Raw: {Raw}", eventResult.Content);
                    break;
                }

                if (payload is IVRChatCurrentUserPayload or IVRChatWebSocketFriendUserPayload)
                {
                    if (contentNodes["user"] is not { } userNode)
                        throw new UnexpectedApiBehaviourException("event user field not exist");

                    switch (payload)
                    {
                        case IVRChatCurrentUserPayload currentUserPayload:
                            var currentUser =
                                await KiotaJsonSerializer.DeserializeAsync(userNode.ToJsonString(),
                                    CurrentUser.CreateFromDiscriminatorValue);

                            if (currentUser is null)
                                throw new UnexpectedApiBehaviourException("event user is null");

                            currentUserPayload.User = currentUser;
                            break;
                        case IVRChatWebSocketFriendUserPayload limitedUserPayload:
                            var websocketFriend = JsonSerializer.Deserialize(userNode.ToJsonString(),
                                VRChatWebSocketEventContext.Default.VRChatWebSocketFriendUser);

                            if (websocketFriend is null)
                                throw new UnexpectedApiBehaviourException("event user is null");

                            limitedUserPayload.User = websocketFriend;
                            break;
                    }
                }

                SendMessageToMessenger(payload);

                break;
            case "see-notification":
                if (eventResult.Content is null)
                    throw new UnexpectedApiBehaviourException("hide-notification Content is null");

                SendMessageToMessenger(new SeeNotificationEvent(eventResult.Content));
                break;
            case "hide-notification":
                if (eventResult.Content is null)
                    throw new UnexpectedApiBehaviourException("hide-notification Content is null");

                SendMessageToMessenger(new HideNotificationEvent(eventResult.Content));
                break;
            case "clear-notification":
                SendMessageToMessenger(new ClearNotificationEvent());
                break;
            case "notification":
                VRChatWebSocketPayloadBase? notificationPayload = null;
                try
                {
                    notificationPayload = GetNotificationPayload(eventResult.Content!);
                }
                catch (UnknownNotificationTypeException ex)
                {
                    logger.LogWarning("Unknown notification type: {Type}", ex.EventType);
                }

                if (notificationPayload is null)
                {
                    logger.LogWarning("Failed to parse notification payload, Raw: {Raw}", eventResult.Content);
                }
                else
                {
                    SendMessageToMessenger(notificationPayload);
                }

                break;
            default:
                throw new UnknownWebSocketEventTypeException(eventResult.Type);
        }
    }

    private void SendMessageToMessenger(VRChatWebSocketPayloadBase payload)
    {
        messengerService.Send(new VRChatPipelineMessage(payload));
    }

    private static VRChatWebSocketPayloadBase? GetNotificationPayload(string rawJson)
    {
        var notificationJson = JsonNode.Parse(rawJson);
        if (notificationJson?["type"]?.GetValue<string>() is not { } notificationType)
            return null;

        return notificationType switch
        {
            "invite" => notificationJson.Deserialize(VRChatWebSocketEventContext.Default
                .WebSocketNotificationV1WebSocketNotificationV1InviteDetail),
            "inviteResponse" => notificationJson.Deserialize(VRChatWebSocketEventContext.Default
                .WebSocketNotificationV1WebSocketNotificationV1InviteResponseDetail),
            "requestInvite" => notificationJson.Deserialize(VRChatWebSocketEventContext.Default
                .WebSocketNotificationV1WebSocketNotificationV1RequestInviteDetail),
            "requestInviteResponse" => notificationJson.Deserialize(VRChatWebSocketEventContext.Default
                .WebSocketNotificationV1WebSocketNotificationV1RequestInviteResponseDetail),
            "voteToKick" => notificationJson.Deserialize(VRChatWebSocketEventContext.Default
                .WebSocketNotificationV1WebSocketNotificationV1VoteToKickDetail),
            _ => throw new UnknownNotificationTypeException(notificationType)
        };
    }

    private static JsonTypeInfo GetPayloadJsonTypeInfo(string type)
    {
        return type switch
        {
            "friend-add" => VRChatWebSocketEventContext.Default.FriendAddEvent,
            "friend-delete" => VRChatWebSocketEventContext.Default.FriendDeleteEvent,
            "friend-online" => VRChatWebSocketEventContext.Default.FriendOnlineEvent,
            "friend-active" => VRChatWebSocketEventContext.Default.FriendActiveEvent,
            "friend-offline" => VRChatWebSocketEventContext.Default.FriendOfflineEvent,
            "friend-update" => VRChatWebSocketEventContext.Default.FriendUpdateEvent,
            "friend-location" => VRChatWebSocketEventContext.Default.FriendLocationEvent,
            "group-joined" => VRChatWebSocketEventContext.Default.GroupJoinedEvent,
            "group-left" => VRChatWebSocketEventContext.Default.GroupLeftEvent,
            "group-member-update" => VRChatWebSocketEventContext.Default.GroupMemberUpdateEvent,
            "group-role-update" => VRChatWebSocketEventContext.Default.GroupRoleUpdateEvent,
            "instance-queue-joined" => VRChatWebSocketEventContext.Default.InstanceQueueJoinedEvent,
            "instance-queue-ready" => VRChatWebSocketEventContext.Default.InstanceQueueReadyEvent,
            "response-notification" => VRChatWebSocketEventContext.Default.ResponseNotificationEvent,
            "user-update" => VRChatWebSocketEventContext.Default.UserUpdateEvent,
            "user-location" => VRChatWebSocketEventContext.Default.UserLocationEvent,
            "user-badge-assigned" => VRChatWebSocketEventContext.Default.UserBadgeAssignedEvent,
            "user-badge-unassigned" => VRChatWebSocketEventContext.Default.UserBadgeUnassignedEvent,
            "content-refresh" => VRChatWebSocketEventContext.Default.ContentRefreshEvent,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
