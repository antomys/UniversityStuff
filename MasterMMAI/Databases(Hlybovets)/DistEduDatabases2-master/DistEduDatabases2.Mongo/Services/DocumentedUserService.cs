using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Models;
using DistEduDatabases2.Mongo.Extensions;
using DistEduDatabases2.Mongo.Services.Interfaces;
using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DistEduDatabases2.Mongo.Services;

internal sealed class DocumentedUserService : IDocumentedUserService
{
    private readonly IMongoCollection<User> _userCollection;

    public DocumentedUserService(IOptions<MongoSettings> mongoSettings)
    {
        var mongoClient = new MongoClient(mongoSettings.Value.DocumentedDb);
        
        var mongoDatabase = mongoClient.GetDatabase("default");

        _userCollection = mongoDatabase.GetCollection<User>(nameof(User));
    }

    public Task<User> GetAsync(Guid id) =>
        _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public Task<List<User>> GetAsync(IEnumerable<Guid> cvIds)
    {
        return _userCollection.Find(x => cvIds.Contains(x.CvMongoId)).ToListAsync();
    }
    
    public async Task<List<UserModel>?> GetModelAsync(IEnumerable<Guid> cvIds)
    {
        var users = await _userCollection.Find(x => cvIds.Contains(x.CvMongoId)).ToListAsync();

        return users?.Adapt<List<UserModel>>();
    }

    public Task CreateAsync(User newUser)
    {
        return _userCollection.InsertOneAsync(newUser);
    }

    public Task InsertManyAsync(IEnumerable<User> users)
    {
        return _userCollection.InsertManyAsync(users);
    }
    
    public Task InsertCvIdAsync(Guid id, Guid cvId)
    {
        return _userCollection.UpdateOneAsync(
            x => x.Id == id,
            Builders<User>.Update.Set(x=> x.CvMongoId, cvId));
    }
}