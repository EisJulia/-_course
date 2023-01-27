namespace Lab5.exceptions;

public class NameListIsNotUniqueException : Exception
{
    public NameListIsNotUniqueException(string message) : base(message)
    {
    }
}