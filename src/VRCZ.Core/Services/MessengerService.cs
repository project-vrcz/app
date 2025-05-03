namespace VRCZ.Core.Services;

public class MessengerService
{
    private readonly Dictionary<Type, HashSet<Action<object>>> _subscribers = [];

    public void Register<T>(Action<T> action)
    {
        var type = typeof(T);
        if (!_subscribers.TryGetValue(type, out var value))
        {
            value = [];
            _subscribers[type] = value;
        }

        value.Add(o => action((T)o));
    }

    public void Unregister<T>(Action<T> action)
    {
        var type = typeof(T);
        if (!_subscribers.TryGetValue(type, out var value)) return;

        value.Remove(o => action((T)o));

        if (value.Count == 0)
            _subscribers.Remove(type);
    }

    public void Send<T>(T message)
    {
        ArgumentNullException.ThrowIfNull(message);

        var type = typeof(T);
        if (!_subscribers.TryGetValue(type, out var value)) return;

        foreach (var action in value)
            action(message);
    }
}
