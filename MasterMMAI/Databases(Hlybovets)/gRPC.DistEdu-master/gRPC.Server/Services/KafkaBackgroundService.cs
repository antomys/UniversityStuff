using System.Text.Json;
using gRPC.Server.Services.Interfaces;

namespace gRPC.Server.Services;

public class KafkaBackgroundService : IHostedService
{
    private IDisposable _kafkaConsumer;
    private ILogger<KafkaBackgroundService> _logger;
    private readonly IServiceScopeFactory _factory;

    public KafkaBackgroundService(IServiceScopeFactory factory, ILogger<KafkaBackgroundService> logger)
    {
        _factory = factory;
        _logger = logger;
    }
    

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting consuming from kafka");
        _kafkaConsumer = Subscribe();
        
        return Task.CompletedTask;
    }

    private IDisposable Subscribe()
    {
        var client = new Consume();
        return client.ConsumeFrom()
            .Subscribe(onNext => ProcessValue(onNext.Message.Value).GetAwaiter().GetResult());
    }

    private async Task ProcessValue(string value)
    {
        await using var serviceScope = _factory.CreateAsyncScope();
        var dbService = serviceScope.ServiceProvider.GetRequiredService<IProductService>();
        await dbService.Process(JsonSerializer.Deserialize<ProductRequest>(value)!);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _kafkaConsumer.Dispose();
        
        return Task.CompletedTask;
    }
}