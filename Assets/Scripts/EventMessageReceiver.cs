using UnityEngine;

public abstract class EventMessageReceiver<T> : IEventMessageReceiver where T : IEventMessage
{
    protected EventMessageReceiver()
    {
        EventMessageDispatcher.AddReceiver(this);
    }
    
    public void ApplyMessage(IEventMessage message)
    {
        if (message is T)
        {
            ReadMessage((T) message);
        }
        else
        {
            Debug.LogErrorFormat("Message with code {0} received but it's type isn't assignable to {1}",
                message.GetMessageCode(), typeof(T));
        }
    }

    protected abstract void ReadMessage(T message);
}