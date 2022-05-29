using Google.Protobuf.WellKnownTypes;
using gRPC.Server.Entities;
using gRPC.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Server.Services;

internal sealed class ProductService : IProductService
{
    private readonly ServerContext _repository;

    public ProductService(ServerContext repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public Task Process(ProductRequest request, CancellationToken cancellationToken)
    {
        return request.Command switch
        {
            "update" => UpdateAsync(request.Product, cancellationToken),
            "delete" => DeleteAsync(request.Product.ProductId, cancellationToken),
            _ => Task.CompletedTask
        };
    }
    
    public Task<int> AddAsync(Product product, CancellationToken cancellationToken)
    {
        var pr = new ProductEntity
        {
            Id = 0,
            ProductName = product.ProductName,
            ProductId = product.ProductId,
            AddTime = DateTimeOffset.UtcNow
        };

        _repository.Products.Add(pr);
        
        return _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var productRaw = await _repository.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId, cancellationToken: cancellationToken);

        if (productRaw is null)
        {
            return 0;
        }

        productRaw.ProductName = product.ProductName;

        _repository.Products.Update(productRaw);
        
        return await _repository.SaveChangesAsync(cancellationToken);
    }

    public Task<ProductEntity[]> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _repository.Products.Where(x => x.ProductName == name).ToArrayAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var entity = await _repository.Products.FirstOrDefaultAsync(x => x.ProductId == id, cancellationToken);
        if (entity is null)
        {
            return default;
        }
        
        _repository.Products.Remove(entity);
        
        return await _repository.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Product?> GetAsync(string id, CancellationToken cancellationToken)
    {
        var ent = await _repository.Products.FirstOrDefaultAsync(x => x.ProductId == id, cancellationToken: cancellationToken);

        if (ent is null)
        {
            return default;
        }

        return new Product
        {
            ProductName = ent.ProductName,
            ProductId = ent.ProductId,
            AddTime = Timestamp.FromDateTimeOffset(ent.AddTime)
        };
    }
}