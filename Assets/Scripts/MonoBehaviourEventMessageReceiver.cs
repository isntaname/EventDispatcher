using UnityEngine;

public abstract class MonoBehaviourEventMessageReceiver<T> : MonoBehaviour, IEventMessageReceiver where T : IEventMessage
{
    protected virtual void Awake()
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