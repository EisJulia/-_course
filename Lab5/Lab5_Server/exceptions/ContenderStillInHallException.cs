namespace Lab5.exceptions;

public class ContenderStillInHallException : Exception
{
    public ContenderStillInHallException(string message) : base(message)
    {
    }

    public ContenderStillInHallException()
    {
    }
}