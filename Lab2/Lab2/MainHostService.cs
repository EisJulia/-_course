using Microsoft.Extensions.Hosting;

namespace Lab2;

public class MainHostService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;

    private readonly Princess _princess;

    public MainHostService(Princess princess, IHostApplicationLifetime applicationLifetime)
    {
        _princess = princess;
        _applicationLifetime = applicationLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(RunAsync, cancellationToken);
        _applicationLifetime.StopApplication();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    
    public void RunAsync()
    {
        Console.Out.WriteLine(Princess.GetHappiness(_princess.Choose(100)));
    }
}