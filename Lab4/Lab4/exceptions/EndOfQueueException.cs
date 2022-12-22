namespace Lab4.exceptions;

public class EndOfQueueException : Exception
{
    public EndOfQueueException()
    {
    }

    public EndOfQueueException(string message) : base(message)
    {
    }
}