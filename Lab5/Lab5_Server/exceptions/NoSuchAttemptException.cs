namespace Lab5.exceptions;

public class NoSuchAttemptException : Exception
{
    public NoSuchAttemptException(string message) : base(message)
    {
    }

    public NoSuchAttemptException()
    {
    }
}