using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware.Options;
using VRCZ.Core.Exceptions;
using VRCZ.Core.Mappers;
using VRCZ.Core.Messages;
using VRCZ.Core.Models;
using VRCZ.Core.Models.VRChat.TrackedEntities;
using VRCZ.Core.Models.VRChat.WebSocket;
using VRCZ.Core.Models.VRChat.WebSocket.Payload;
using VRCZ.VRChatApi.Generated;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Services.Tracking;

// TODO: Logging, Game Logger Event Handle
public class VRChatTrackedEntitiesService(
    VRChatApiClient vrchatApiClient,
    MessengerService messengerService,
    ILogger<VRChatTrackedEntitiesService> logger)
{
    private CurrentUser? _loggedInUser;

    private readonly ConcurrentDictionary<string, TrackedVRChatUser> _friends = new();
    private readonly ConcurrentDictionary<string, UserLocation> _userLocations = new();

    private readonly ConcurrentDictionary<string, World> _worlds = [];
    private readonly ConcurrentDictionary<InstanceIdentity, TrackedVRChatInstance> _instances = [];

    private CancellationTokenSource? _cancellationTokenSource;
    private readonly Channel<VRChatPipelineMessage> _messageChannel = Channel.CreateUnbounded<VRChatPipelineMessage>();

    public event EventHandler<CurrentUser?>? LoggedInUserUpdated;

    public event EventHandler<LimitedUser>? FriendAdded;
    public event EventHandler<LimitedUser>? FriendRemoved;
    public event EventHandler<LimitedUser>? FriendUpdated;

    public event EventHandler<UserLocationUpdatedEventArgs>? UserLocationUpdated;

    public async Task StartAsync()
    {
        await StopAsync();

        messengerService.Register<VRChatPipelineMessage>(WriteMessage);

        _cancellationTokenSource = new CancellationTokenSource();

        _ = Task.Run(HandleEventLoop);
    }

    public async Task StopAsync()
    {
        messengerService.Unregister<VRChatPipelineMessage>(WriteMessage);

        if (_cancellationTokenSource is null)
            return;

        await _cancellationTokenSource.CancelAsync();
        _cancellationTokenSource.Dispose();
    }

    #region Event Handling

    #region Pipeline

    private void WriteMessage(VRChatPipelineMessage message)
    {
        _messageChannel.Writer.TryWrite(message);
    }

    private async Task HandleEventLoop()
    {
        try
        {
            while (!_cancellationTokenSource?.IsCancellationRequested ?? false)
            {
                var message = await _messageChannel.Reader.ReadAsync(_cancellationTokenSource.Token);

                try
                {
                    await HandleEventAsync(message.Payload);
                }
                catch (Exception)
                {
                    logger.LogError("Error handling entities tracking event: {Message}", message);
                }
            }
        }
        catch (TaskCanceledException)
        {
            // ignore
        }
    }

    private async Task HandleEventAsync(VRChatWebSocketPayloadBase payload)
    {
        if (payload is IVRChatWebSocketWorldPayload { World: { } world })
            AddOrUpdateWorld(world);

        switch (payload)
        {
            case FriendDeleteEvent friendDeleteEvent:
                RemoveFriend(friendDeleteEvent.UserId);
                break;
            case IVRChatCurrentUserPayload currentUserPayload:
                SetLoggedInUser(currentUserPayload.User!);
                break;
            case IVRChatWebSocketFriendUserPayload limitedUserPayload:
                AddOrUpdateFriend(limitedUserPayload.User!);
                break;
        }

        switch (payload)
        {
            case FriendOfflineEvent friendOfflineEvent:
                AddOrUpdateUserLocation(friendOfflineEvent.UserId,
                    new UserLocation(UserLocationType.Offline));
                break;
            case IVRChatWebSocketLocationPayload locationPayload:
                var userId = locationPayload switch
                {
                    IVRChatCurrentUserPayload currentUserPayload => currentUserPayload.User?.Id ??
                                                                    throw new UnexpectedApiBehaviourException(
                                                                        "currentUserPayload.User is null"),
                    IVRChatWebSocketFriendUserPayload friendUserPayload => friendUserPayload.User?.Id ??
                                                                           throw new
                                                                               UnexpectedApiBehaviourException(
                                                                                   "friendUserPayload.User is null"),
                    _ => throw new UnexpectedApiBehaviourException("Unknown location payload type")
                };

                if (!string.IsNullOrWhiteSpace(locationPayload.Location) &&
                    !locationPayload.Location.Contains("traveling"))
                {
                    await AddOrUpdateUserLocationWithWorldInstanceAsync(userId,
                        UserLocation.Parse(locationPayload.Location));
                    break;
                }

                if (!string.IsNullOrWhiteSpace(locationPayload.TravelingToLocation))
                {
                    await AddOrUpdateUserLocationWithWorldInstanceAsync(userId,
                        UserLocation.Parse(locationPayload.TravelingToLocation) with
                        {
                            LocationType = UserLocationType.Traveling
                        });
                    break;
                }

                AddOrUpdateUserLocation(userId,
                    new UserLocation(UserLocationType.Unknown));
                break;
        }
    }

    #endregion

    #endregion

    public CurrentUser? GetLoggedInUser()
    {
        return _loggedInUser;
    }

    #region Cache AddOrUpdate

    internal void SetLoggedInUser(CurrentUser user)
    {
        _loggedInUser = user;

        LoggedInUserUpdated?.Invoke(this, user);
    }

    #region Friends

    internal void AddOrUpdateFriend(TrackedVRChatUser user)
    {
        if (!user.IsFriend ?? false)
            throw new ArgumentException("User is not a friend", nameof(user));

        var userExist = _friends.ContainsKey(user.Id);

        _friends.AddOrUpdate(user.Id, user, (_, _) => user);

        if (user.Location is { } newLocationString)
        {
            var newUserLocation = UserLocation.Parse(newLocationString);

            if (!_userLocations.TryGetValue(user.Id, out var userLocation) || userLocation != newUserLocation)
            {
                AddOrUpdateUserLocation(user.Id, newUserLocation);
            }
        }

        if (userExist)
        {
            FriendUpdated?.Invoke(this, TrackedVRChatEntitiesMapper.ToLimitedUser(user));
            return;
        }

        FriendAdded?.Invoke(this, TrackedVRChatEntitiesMapper.ToLimitedUser(user));
    }

    internal void AddOrUpdateFriend(LimitedUser user)
    {
        if (user.Id is null)
            throw new ArgumentException("User ID cannot be null", nameof(user));

        TrackedVRChatUser trackedUser;
        if (_friends.GetValueOrDefault(user.Id) is { } friend)
        {
            trackedUser = TrackedVRChatEntitiesMapper.ApplyUpdateToTrackedUser(friend, user);
        }
        else
        {
            trackedUser = TrackedVRChatEntitiesMapper.ToTrackedUser(user);
        }

        AddOrUpdateFriend(trackedUser);
    }

    internal void AddOrUpdateFriend(User user)
    {
        if (user.Id is null)
            throw new ArgumentException("User ID cannot be null", nameof(user));

        TrackedVRChatUser trackedUser;
        if (_friends.GetValueOrDefault(user.Id) is { } friend)
        {
            trackedUser = TrackedVRChatEntitiesMapper.ApplyUpdateToTrackedUser(friend, user);
        }
        else
        {
            trackedUser = TrackedVRChatEntitiesMapper.ToTrackedUser(user);
        }

        AddOrUpdateFriend(trackedUser);
    }

    internal void AddOrUpdateFriend(VRChatWebSocketFriendUser user)
    {
        if (user.Id is null)
            throw new ArgumentException("User ID cannot be null", nameof(user));

        TrackedVRChatUser trackedUser;
        if (_friends.GetValueOrDefault(user.Id) is { } friend)
        {
            trackedUser = TrackedVRChatEntitiesMapper.ApplyUpdateToTrackedUser(friend, user);
        }
        else
        {
            trackedUser = TrackedVRChatEntitiesMapper.ToTrackedUser(user);
        }

        AddOrUpdateFriend(trackedUser);
    }

    internal void RemoveFriend(string userId)
    {
        if (!_friends.TryRemove(userId, out var removedFriend))
            return;

        var user = TrackedVRChatEntitiesMapper.ToLimitedUser(removedFriend);
        FriendRemoved?.Invoke(this, user);

        RunUserLocationGc();
    }

    #endregion

    #region User Location

    internal void AddOrUpdateUserLocation(string userId, UserLocation userLocation)
    {
        var locationString = userLocation.ToLocationString();

        if (_friends.TryGetValue(userId, out var trackedUser) && trackedUser.Location != locationString)
        {
            trackedUser.Location = locationString;
            AddOrUpdateFriend(trackedUser);
        }

        _userLocations.AddOrUpdate(userId, userLocation, (_, _) => userLocation);
        UserLocationUpdated?.Invoke(this, new UserLocationUpdatedEventArgs(userId, userLocation));

        RunWorldsInstancesGc();
    }

    internal async Task AddOrUpdateUserLocationWithWorldInstanceAsync(string userId, UserLocation userLocation)
    {
        if (userLocation.WorldId is { } worldId)
        {
            await GetCachedWorldAsync(worldId);

            if (userLocation.InstanceId is { } instanceId)
            {
                await GetCachedInstanceAsync(worldId, instanceId);
            }
        }

        AddOrUpdateUserLocation(userId, userLocation);
    }

    #endregion

    #region Worlds

    internal void AddOrUpdateWorld(VRChatWebSocketWorld world)
    {
        var result = _worlds.TryGetValue(world.Id, out var cachedWorld)
            ? TrackedVRChatEntitiesMapper.ApplyUpdateToWorld(cachedWorld, world)
            : TrackedVRChatEntitiesMapper.ToWorld(world);

        AddOrUpdateWorld(result);
    }

    internal void AddOrUpdateWorld(World world)
    {
        if (world.Id is not { } id)
            throw new ArgumentException("World ID is null", nameof(world));

        _worlds.AddOrUpdate(id, world, (_, _) => world);
    }

    #endregion

    #region Instances

    internal void AddOrUpdateInstance(Instance instance)
    {
        if (instance.InstanceId is not { } id)
            throw new ArgumentException("Instance ID is null", nameof(instance));

        if (instance.World is not { } world)
            throw new ArgumentException("Instance world is null", nameof(instance));

        if (world.Id is not { } worldId)
            throw new ArgumentException("Instance world ID is null", nameof(instance));

        AddOrUpdateWorld(world);

        if (instance.Users is { } users)
        {
            foreach (var limitedUser in users.Where(user => user.Id is not null && (user.IsFriend ?? false)).ToArray())
            {
                AddOrUpdateFriend(limitedUser);
            }
        }

        var result = TrackedVRChatEntitiesMapper.ToTrackedInstance(instance);

        _instances.AddOrUpdate(new InstanceIdentity(worldId, id), result, (_, _) => result);
    }

    #endregion

    #endregion

    #region Cache GC

    internal void RunWorldsInstancesGc()
    {
        var instancesToRemove = _instances
            .Where(instance =>
                _userLocations.All(location =>
                    location.Value.InstanceId != instance.Value.InstanceId &&
                    location.Value.WorldId != instance.Value.WorldId))
            .ToArray();

        foreach (var instance in instancesToRemove)
        {
            _instances.TryRemove(instance);
        }

        var worldsToRemove = _worlds
            .Where(world =>
                _userLocations.All(location => location.Value.WorldId != world.Value.Id) &&
                _instances.All(instance => instance.Value.WorldId != world.Value.Id))
            .ToArray();

        foreach (var world in worldsToRemove)
        {
            _worlds.TryRemove(world);
        }
    }

    internal void RunUserLocationGc()
    {
        var userLocationsToRemove = _userLocations
            .Where(location =>
                _friends.All(user => user.Key != location.Key) &&
                _loggedInUser?.Id != location.Key)
            .ToArray();

        foreach (var location in userLocationsToRemove)
        {
            _userLocations.TryRemove(location);
        }

        RunWorldsInstancesGc();
    }

    #endregion

    #region Get Methods

    public async ValueTask<User> GetUserAsync(string userId)
    {
        var user = await vrchatApiClient.Users[userId].GetAsync();

        if (user is null)
            throw new UnexpectedApiBehaviourException("User response body is null");

        if (_loggedInUser?.Id == userId)
        {
            _loggedInUser = VRChatUserMapper.UserToCurrentUser(user);
        }

        if ((_loggedInUser?.Friends?.Contains(userId) ?? false) || _friends.ContainsKey(userId))
        {
            AddOrUpdateFriend(user);
        }

        return user;
    }

    public UserLocation? GetUserLocation(string userId)
    {
        return _userLocations.GetValueOrDefault(userId);
    }

    #region Worlds

    public async ValueTask<World> GetAndCacheWorldAsync(string worldId)
    {
        var world = await vrchatApiClient.Worlds[worldId].GetAsync();

        if (world is null)
            throw new UnexpectedApiBehaviourException("World response body is null");

        AddOrUpdateWorld(world);

        return world;
    }

    public async ValueTask<World> GetCachedWorldAsync(string worldId)
    {
        if (_worlds.TryGetValue(worldId, out var world))
            return world;

        return await GetAndCacheWorldAsync(worldId);
    }

    public async ValueTask<World> GetWorldAsync(string worldId)
    {
        var world = await vrchatApiClient.Worlds[worldId].GetAsync();

        if (world is null)
            throw new UnexpectedApiBehaviourException("World response body is null");

        if (_worlds.ContainsKey(worldId))
            AddOrUpdateWorld(world);

        return world;
    }

    #endregion

    #region Instanaces

    public async ValueTask<Instance> GetAndCacheInstanceAsync(string worldId, string instanceId)
    {
        var instance = await vrchatApiClient.Instances.WithWorldIdWithInstanceId(instanceId, worldId)
            .GetAsync(config =>
            {
                var handler = new UriReplacementHandlerOption(true, [
                    new KeyValuePair<string, string>("%28", "("),
                    new KeyValuePair<string, string>("%29", ")")
                ]);

                config.Options.Add(handler);
            });

        if (instance is null)
            throw new UnexpectedApiBehaviourException("Instance response body is null");

        AddOrUpdateInstance(instance);

        return instance;
    }

    public async ValueTask<Instance> GetCachedInstanceAsync(string worldId, string instanceId)
    {
        if (!_instances.TryGetValue(new InstanceIdentity(worldId, instanceId), out var instance))
            return await GetAndCacheInstanceAsync(worldId, instanceId);

        var result = TrackedVRChatEntitiesMapper.ToInstance(instance);
        var world = await GetCachedWorldAsync(worldId);

        result.World = world;
        result.Id = worldId;

        return result;
    }

    #endregion

    #region Freinds

    public LimitedUser[] GetFriends()
    {
        return _friends.Values.Select(TrackedVRChatEntitiesMapper.ToLimitedUser).ToArray();
    }

    public async ValueTask<LimitedUser[]> GetFriendsAsync()
    {
        var onlineOrActiveFriends = await GetAllFriendsInternalAsync(false);
        var offlineFriends = await GetAllFriendsInternalAsync(true);

        var friends = onlineOrActiveFriends.Concat(offlineFriends).ToArray();

        foreach (var friend in friends)
        {
            if (friend.Location is null)
                throw new UnexpectedApiBehaviourException("Friend location is null");

            var location = UserLocation.Parse(friend.Location);

            if (location.WorldId is { } worldId)
            {
                if (location.InstanceId is { } instanceId)
                {
                    await GetCachedInstanceAsync(worldId, instanceId);
                }
                else
                {
                    await GetCachedWorldAsync(worldId);
                }
            }

            AddOrUpdateFriend(friend);
        }

        return friends;
    }

    private async ValueTask<LimitedUser[]> GetAllFriendsInternalAsync(bool offline)
    {
        List<LimitedUser> friends = [];

        const int pageSize = 100;
        for (var offset = 0;; offset += pageSize)
        {
            var response = await vrchatApiClient.Auth.User.Friends.GetAsync(config =>
            {
                config.QueryParameters.Offline = offline;
                config.QueryParameters.N = pageSize;
                config.QueryParameters.Offset = offset;
            });

            if (response is null)
                throw new UnexpectedApiBehaviourException("Friends response body is null");

            friends.AddRange(response);

            if (response.Count < pageSize)
                break;
        }

        return friends.ToArray();
    }

    #endregion

    #endregion
}

public class UserLocationUpdatedEventArgs(string userId, UserLocation userLocation) : EventArgs
{
    public string UserId { get; } = userId;
    public UserLocation UserLocation { get; } = userLocation;
}

public record InstanceIdentity(string WorldId, string InstanceId);
