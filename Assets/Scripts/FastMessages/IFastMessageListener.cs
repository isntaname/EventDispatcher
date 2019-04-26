namespace EventDispatcher
{
    public interface IFastMessageListener<in T> where T : struct, IFastMessage
    {
        void OnMessageReceived(T message);
    }
}