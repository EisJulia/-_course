using Lab4.exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Lab4;

public class MainHostService : IHostedService
{

    private readonly AttemptContext _db;

    private readonly Simulator _simulator;

    public MainHostService(Simulator simulator)
    {
        _db = new AttemptContext();
        _simulator = simulator;
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
                    if (_db.Attempts.Any())
                    {
                        Console.Out.WriteLine("Database already contains attempts");
                        break;
                    }

                    _simulator.GenerateAttempts(100);
                    Console.Out.WriteLine("A hundred generated");
                    break;
                }
                case "/regenerate100":
                {
                    if (_db.Attempts.Any())
                    {
                        _simulator.RegenerateAttempts(100);
                        Console.Out.WriteLine("A new hundred generated");
                        break;
                    }

                    _simulator.GenerateAttempts(100);
                    Console.Out.WriteLine("A new hundred generated, database was empty");
                    break;
                }
                case "/run":
                {
                    if (commandArray.Length < 2)
                    {
                        Console.Out.WriteLine("Enter attempt id");
                        break;
                    }

                    var id = Int32.Parse(commandArray[1]);
                    int contender = -1, happiness = 0;
                    try
                    {
                        _simulator.RunAttempt(id, ref contender, ref happiness);
                        Console.Out.WriteLine("Simulation succeeded: contender number - " + contender +
                                              ", happiness - " + happiness);
                    }
                    catch (SimulationException e)
                    {
                        Console.Out.WriteLine("Simulation failed:" + e.Message);
                    }

                    break;
                }
                case "/avrHappiness":
                {
                    if (_db.Attempts.Any())
                    {
                        Console.Out.WriteLine("Average happiness = " + _simulator.CalcAvrHappiness());
                    }
                    else
                    {
                        Console.Out.WriteLine("Database is empty, generate attempts (command /generate100)");
                    }
                    break;
                }
                case "/help":
                {
                    Console.Out.WriteLine("Command List:");
                    Console.Out.WriteLine(
                        "/generate100 - generates random contenders list, applies strategy and saves to database");
                    Console.Out.WriteLine(
                        "/regenerate100 - the same the /generate100, but rewrites existing attempts in database if there are any");
                    Console.Out.WriteLine("/run %id - receives attempt id, simulates strategy with data from database");
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