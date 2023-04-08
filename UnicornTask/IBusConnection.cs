namespace UnicornTask;

public interface IBusConnection
{ 
    Task PublishAsync(byte[] array);
}