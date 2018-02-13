public interface IEventMessageReceiver
{
    void ApplyMessage(IEventMessage message);
}