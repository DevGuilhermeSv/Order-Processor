using MassTransit;

namespace ProducerOrder;

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = Host.CreateApplicationBuilder(args);
        
        var rabbitConfig = builder.Configuration.GetSection("RabbitMq");
        var services = builder.Services;
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((ctx, cfg) => 
            {
                cfg.Host(rabbitConfig["Host"], "/", h =>
                {
                    h.Username(rabbitConfig["Username"]);
                    h.Password(rabbitConfig["Password"]);
                });
                
            });
        });
        
        builder.Services.AddHostedService<Worker>();
        
        var host = builder.Build();
        host.Run();
    }
}