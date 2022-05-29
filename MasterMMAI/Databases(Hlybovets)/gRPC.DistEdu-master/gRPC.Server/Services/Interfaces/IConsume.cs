using Confluent.Kafka;

namespace gRPC.Server.Services.Interfaces;

public interface IConsume
{
    IObservable<ConsumeResult<string, string>> ConsumeFrom();
}