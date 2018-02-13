public class EventMessage : IEventMessage
{
    private readonly string messageCode;

    public EventMessage()
    {
    }

    public EventMessage(string messageCode)
    {
        this.messageCode = messageCode;
    }
    
    public string GetMessageCode()
    {
        return messageCode;
    }
}