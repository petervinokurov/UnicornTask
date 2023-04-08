namespace UnicornTask;

public class BusConnectionDummy : IBusConnection
{
    public readonly List<long> QueueMonitor = new();
    public Task PublishAsync(byte[] array)
    {
        QueueMonitor.Add(array.Length);
        return Task.CompletedTask;
    }
}