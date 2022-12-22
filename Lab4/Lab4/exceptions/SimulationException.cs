namespace Lab4.exceptions;

public class SimulationException : Exception
{
    public SimulationException(string message) : base(message)
    {
    }

    public SimulationException()
    {
    }
}