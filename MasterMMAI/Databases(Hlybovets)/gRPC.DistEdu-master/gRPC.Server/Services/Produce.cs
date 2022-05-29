using System.Text.Json;
using Confluent.Kafka;
using gRPC.Server.Services.Interfaces;

namespace gRPC.Server.Services;

internal sealed class Produce : IProduce
{
    private readonly ProducerBuilder<string, string> _producerBuilder;

    public Produce()
    {
        var producerConfig = new ProducerConfig
        {
            ClientId = $"AppDomain.CurrentDomain.FriendlyName-{Guid.NewGuid().ToString()}",
            BootstrapServers = "localhost:9092"
        };

        _producerBuilder = new ProducerBuilder<string, string>(producerConfig);
    }

    public async Task PublishAsync(ProductRequest productRequest)
    {
        using var p = _producerBuilder.Build();
        try
        {
            var msg = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(productRequest)
            };
            
            var dr = await p.ProduceAsync(Consume.Topic, msg);
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
        catch (ProduceException<string, ProductRequest> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
    }
}