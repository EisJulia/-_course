namespace Lab4.exceptions;

public class NameListIsNotUniqueException : Exception
{
    public NameListIsNotUniqueException(string message) : base(message)
    {
    }
}