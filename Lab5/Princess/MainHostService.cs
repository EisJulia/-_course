using Microsoft.Extensions.Hosting;

namespace Princess;

public class MainHostService : IHostedService
{
    private readonly Princess _princess;

    public MainHostService(Princess princess)
    {
        _princess = princess;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(RunAsync, cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public void RunAsync()
    {
        while (true)
        {
            var command = Console.In.ReadLine();
            if (command == null) continue;
            var commandArray = command.Split();
            switch (commandArray[0])
            {
                case "/generate100":
                {
                    _princess.Generate100();
                    Console.Out.WriteLine("A hundred generated");
                    break;
                }
                case "/generate1":
                {
                    var id = _princess.GenerateAttempt();
                    _princess.Choose(100, id);
                    break;
                }
                case "/avrHappiness":
                {
                    var happiness = _princess.CalcAvrHappiness();
                    if (happiness == null)
                    {
                        Console.Out.WriteLine("No attempts yet");
                    }
                    else
                    {
                        Console.Out.WriteLine("Average happiness = " + happiness);
                    }
                    break;
                }
                case "/help":
                {
                    Console.Out.WriteLine("Command List:");
                    Console.Out.WriteLine(
                        "/generate100 - generates random contenders list, applies strategy and saves to database");
                    Console.Out.WriteLine(
                        "/generate1 - the same the /generate100, but generates 1 attempt");
                    Console.Out.WriteLine("/avrHappiness - calculates average happiness of all attempts containing in database");
                    break;
                }
                default:
                {
                    Console.Out.WriteLine("No such command");
                    if (commandArray[0][0] != '/')
                    {
                        Console.Out.WriteLine("Commands must start with /");
                    }

                    break;
                }
            }
        }
    }

}