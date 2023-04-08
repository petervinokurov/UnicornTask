namespace UnicornTask;

public interface IBusMessageWriter
{
    Task SendMessageOriginAsync(byte[] nextMessage);
    Task SendMessageAsync(byte[] nextMessage);
}