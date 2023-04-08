namespace UnicornTask;

public class BusMessageWriter : IBusMessageWriter
{
    private readonly IBusConnection _connection;
    private readonly MemoryStream _buffer = new();
    readonly SemaphoreSlim _semaphore = new(1, 1);

    public BusMessageWriter(IBusConnection connection)
    {
        _connection = connection;
    }

    public async Task SendMessageOriginAsync(byte[] nextMessage)
    {
        _buffer.Write(nextMessage, 0,
            nextMessage.Length);
        if (_buffer.Length > 1000)
        {
            await
                _connection.PublishAsync(_buffer.ToArray());
            _buffer.SetLength(0);
        }
    }


    public async Task SendMessageAsync(byte[] nextMessage)
    {
        await _semaphore.WaitAsync();

        try
        {
            _buffer.Write(nextMessage, 0,
                nextMessage.Length);
            if (_buffer.Length > 1000)
            { 
                await _connection.PublishAsync(_buffer.ToArray());
                _buffer.SetLength(0); 
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}