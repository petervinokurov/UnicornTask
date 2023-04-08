namespace UnicornTask.Tests;

public class BusMessageWriterTests
{
    [Fact]
    public async Task Send101ByteIn20ThreadsExpectTo2MessagesInQueueWith1010Length()
    {
        var connection = new BusConnectionDummy();
        var busWriter = new BusMessageWriter(connection);
        var tasks = new List<Task>();
        for (var i = 0; i < 20; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                var package = new byte[101];
                await busWriter.SendMessageAsync(package);
            }));
        }

        await Task.WhenAll(tasks);
        Assert.Equal(2, connection.QueueMonitor.Count);
        Assert.All(connection.QueueMonitor, x => Assert.Equal(1010, x));
    }
    
    [Fact]
    public async Task Send101ByteIn20ThreadsToOriginExpectTo2MessageInQueueWith1010Length()
    {
        var connection = new BusConnectionDummy();
        var busWriter = new BusMessageWriter(connection);
        var tasks = new List<Task>();
        for (var i = 0; i < 20; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                var package = new byte[101];
                await busWriter.SendMessageOriginAsync(package);
            }));
        }

        await Task.WhenAll(tasks);
        Assert.Equal(2, connection.QueueMonitor.Count);
        Assert.All(connection.QueueMonitor, x => Assert.Equal(1010, x));
    }
}