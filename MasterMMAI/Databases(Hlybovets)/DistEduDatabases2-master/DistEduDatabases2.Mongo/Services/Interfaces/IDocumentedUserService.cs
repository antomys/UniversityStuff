using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Models;

namespace DistEduDatabases2.Mongo.Services.Interfaces;

public interface IDocumentedUserService
{
    Task<User> GetAsync(Guid id);

    Task<List<User>> GetAsync(IEnumerable<Guid> cvIds);

    Task<List<UserModel>?> GetModelAsync(IEnumerable<Guid> cvIds);

    Task CreateAsync(User newUser);

    Task InsertManyAsync(IEnumerable<User> users);

    Task InsertCvIdAsync(Guid id, Guid cvId);
}