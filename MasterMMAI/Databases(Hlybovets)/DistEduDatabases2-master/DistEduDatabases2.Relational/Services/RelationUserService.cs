using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Relational.Context;
using DistEduDatabases2.Relational.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DistEduDatabases2.Relational.Services;

internal sealed class RelationUserService : IRelationUserService
{
    private readonly ServerContext _serverContext;

    public RelationUserService(ServerContext serverContext)
    {
        _serverContext = serverContext;
    }

    public async Task<bool> CreateAsync(string login, string password)
    {
        if (await _serverContext.Users.AnyAsync(user => user.Login.Equals(login)))
        {
            return false;
        }

        var user = new User
        {
            Login = login,
            Password = password
        };

        _serverContext.Users.Add(user);

        await _serverContext.SaveChangesAsync();

        return true;
    }

    public async Task<User> GetAsync(string login)
    {
        var user = await _serverContext.Users.FirstOrDefaultAsync(user => user.Login.Equals(login));

        if (user is null)
        {
            throw new ArgumentNullException(nameof(User), "User cannot be 'null'.");
        }

        return user;
    }
}