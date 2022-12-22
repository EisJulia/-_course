using Lab2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<MainHostService>();
        services.AddScoped<Princess>();
        services.AddScoped<Friend>();
        services.AddScoped<Hall>();
        services.AddScoped<ContendersGenerator>();
    })
    .Build();
host.Run();
