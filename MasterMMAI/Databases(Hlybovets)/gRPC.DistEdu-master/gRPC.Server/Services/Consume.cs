using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Confluent.Kafka;
using gRPC.Server.Services.Interfaces;

namespace gRPC.Server.Services;

internal sealed class Consume : IConsume
{
    public const string Topic = "gRPC-quickstart";
    
    private readonly IConsumer<string, string> _consumer;

    private ConcurrentDictionary<string, ISubject<ConsumeResult<string, string>>> _consumeSubjects;

    public Consume()
    {
        _consumer = CreateConsumer();
    }

    public IObservable<ConsumeResult<string, string>> ConsumeFrom()
    {
        if (!_consumer.Subscription.Contains(Topic))
        {
            _consumer.Subscribe(Topic);
        }

        if (_consumeSubjects.ContainsKey(Topic))
        {
            return _consumeSubjects[Topic].AsObservable();
        }

        _consumeSubjects.TryAdd(Topic, new Subject<ConsumeResult<string, string>>());

        return _consumeSubjects[Topic].AsObservable();
    }
    
    public IConsumer<string, string> CreateConsumer()
    {
        var cts = new CancellationTokenSource();
        _consumeSubjects = new ConcurrentDictionary<string, ISubject<ConsumeResult<string, string>>>();
        
        var config = new ConsumerConfig
        {
            GroupId = $"{AppDomain.CurrentDomain.FriendlyName}-{Guid.NewGuid().ToString()}",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        var consumer = new ConsumerBuilder<string, string>(config).Build();

        Task.Factory.StartNew(_ =>
                StartConsumerWork(cts.Token),
            cts.Token,
            TaskCreationOptions.LongRunning);

        return consumer;
    }

    private void StartConsumerWork(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var res = _consumer.Consume(cancellationToken);
                
                Console.WriteLine($"Consumed Entity {res.Message.Key}; {res.Message.Value}; {res.Topic}");
                _consumeSubjects[Topic].OnNext(res);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}