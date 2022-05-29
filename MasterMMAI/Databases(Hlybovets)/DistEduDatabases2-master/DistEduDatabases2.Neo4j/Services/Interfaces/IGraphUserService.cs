using DistEduDatabases2.Common.Models;

namespace DistEduDatabases2.Neo4j.Services.Interfaces;

public interface IGraphUserService
{
    Task<UserModel?> CreateAsync(string login, string password);

    Task<UserModel?> GetAsync(Guid userId);
}