using Lab4;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .UseConsoleLifetime()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<MainHostService>();
        services.AddScoped<Princess>();
        services.AddScoped<Friend>();
        services.AddScoped<Hall>();
        services.AddScoped<ContendersGenerator>();
        services.AddScoped<Simulator>();
    })
    .Build();
host.Run();
