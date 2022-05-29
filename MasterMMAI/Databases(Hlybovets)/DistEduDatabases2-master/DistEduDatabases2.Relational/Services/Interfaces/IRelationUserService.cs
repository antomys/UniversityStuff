using DistEduDatabases2.Common.Entities;

namespace DistEduDatabases2.Relational.Services.Interfaces;

public interface IRelationUserService
{
    Task<bool> CreateAsync(string login, string password);

    Task<User> GetAsync(string login);
}