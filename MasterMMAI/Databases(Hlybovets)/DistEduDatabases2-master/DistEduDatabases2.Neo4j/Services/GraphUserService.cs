using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Models;
using DistEduDatabases2.Neo4j.Services.Interfaces;
using Neo4jClient;

namespace DistEduDatabases2.Neo4j.Services;

internal sealed class GraphUserService : IGraphUserService
{
    private readonly IGraphClient _graphClient;

    public GraphUserService(IGraphClient graphClient)
    {
        _graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
    }

    public async Task<UserModel?> CreateAsync(string login, string password)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Login = login,
            Password = password,
        };
        
        var users = await _graphClient.Cypher.Create("(usr:User $user)")
            .WithParam("user", user)
            .Return(usr=> usr.As<UserModel>()).ResultsAsync;

        return users?.FirstOrDefault();
    }

    public async Task<UserModel?> GetAsync(Guid userId)
    {
        var user = await _graphClient.Cypher
            .Match("(usr:User)")
            .Where((User usr) => usr.Id == userId)
            .Return(usr=> usr.As<UserModel>()).ResultsAsync;

        return user.FirstOrDefault();
    }
}