using gRPC.Server.Entities;

namespace gRPC.Server.Services.Interfaces;

public interface IProductService
{
    Task Process(ProductRequest request, CancellationToken cancellationToken = default);
    Task<Product?> GetAsync(string id, CancellationToken cancellationToken = default);
    
    Task<ProductEntity[]> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default);

    Task<int> UpdateAsync(Product product, CancellationToken cancellationToken = default);

    Task<int> AddAsync(Product product, CancellationToken cancellationToken = default);
}