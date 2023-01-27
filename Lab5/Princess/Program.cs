using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Princess;

var host = Host.CreateDefaultBuilder(args)
    .UseConsoleLifetime()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<MainHostService>();
        services.AddScoped<Princess.Princess>();
    })
    .Build();
host.Run();