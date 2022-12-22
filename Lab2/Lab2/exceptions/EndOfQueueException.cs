namespace Lab2;

public class EndOfQueueException : Exception
{
    public EndOfQueueException()
    {
    }

    public EndOfQueueException(string message) : base(message)
    {
    }
}