using System.Diagnostics;
using Grpc.Core;
using gRPC.Server.Services.Interfaces;

namespace gRPC.Server.Services;

public sealed class GRpcService : GreetService.GreetServiceBase
{
    private readonly ILogger<GRpcService> _logger;
    private readonly IProductService _productService;
    private readonly IProduce _produce;

    public GRpcService(
        ILogger<GRpcService> logger,
        IProductService productService,
        IProduce produce)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _produce = produce;

        var process = Process.GetCurrentProcess();
        var processInfo = $"Id:{process.Id}-Name:{process.ProcessName}-Handle:{process.Handle}";
        
        _logger.LogInformation("Service starting process:{ProcessInfo}\n", processInfo);
    }

    public override async Task<ProductsResponse> ActualizeData(Product request, ServerCallContext context)
    {
        var res = await _productService.AddAsync(request, context.CancellationToken);

        var response = new ProductsResponse
        {
            ProductName = request.ProductName,
            IsProcessed = res > 0,
        };

        return response;
    }

    public override async Task<ProductsFullResponse> ProcessRequest(ProductRequest request, ServerCallContext context)
    {
        switch (request.Command)
        {
            case "update":
            {
                if (request.PushToKafka)
                {
                    await _produce.PublishAsync(request);

                    return new ProductsFullResponse
                    {
                        Product = null,
                        IsPushedToKafka = true,
                        IsProcessed = true
                    };
                }
                
                var res = await _productService.UpdateAsync(request.Product);
                
                return new ProductsFullResponse
                {
                    Product = (await _productService.GetAsync(request.Product.ProductId, context.CancellationToken))!,
                    IsProcessed = res > 0
                };
            }
            case "delete":
            {
                if (request.PushToKafka)
                {
                    await _produce.PublishAsync(request);

                    return new ProductsFullResponse
                    {
                        Product = null,
                        IsPushedToKafka = true,
                        IsProcessed = true
                    };
                }
                
                var res = await _productService.DeleteAsync(request.Product.ProductId, context.CancellationToken);

                return new ProductsFullResponse
                {
                    IsProcessed = res > 0
                };
            }
            default:
            {
                return new ProductsFullResponse
                {
                    IsProcessed = false
                };
            }
        }
    }
}