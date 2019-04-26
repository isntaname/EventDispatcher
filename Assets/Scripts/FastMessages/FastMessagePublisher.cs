using System.Collections.Generic;

namespace EventDispatcher
{
    public sealed class FastMessagePublisher<T> : IFastMessagePublisher where T : struct, IFastMessage
    {
        private readonly List<IFastMessageListener<T>> listeners;

        public FastMessagePublisher()
        {
            listeners = new List<IFastMessageListener<T>>();
        }

        public void AddListener(IFastMessageListener<T> listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(IFastMessageListener<T> listener)
        {
            listeners.Remove(listener);
        }

        public void SendMessage(T message)
        {
            for (var i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnMessageReceived(message);
            }
        }
    }
}