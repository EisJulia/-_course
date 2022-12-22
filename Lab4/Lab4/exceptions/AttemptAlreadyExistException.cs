namespace Lab4.exceptions;

public class AttemptAlreadyExistException : Exception
{
    public AttemptAlreadyExistException(string message) : base(message)
    {
    }

    public AttemptAlreadyExistException()
    {
    }
}