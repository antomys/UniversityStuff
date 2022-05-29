namespace gRPC.Server.Services.Interfaces;

public interface IProduce
{
    Task PublishAsync(ProductRequest productRequest);
}