using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Generation;
using DistEduDatabases2.Common.Models;
using DistEduDatabases2.Mongo.Extensions;
using DistEduDatabases2.Mongo.Services.Interfaces;
using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DistEduDatabases2.Mongo.Services;

internal sealed class DocumentedCvService : IDocumentedCvService
{
    private readonly IMongoCollection<CV> _cvCollection;
    private readonly IMongoCollection<Passion> _passionCollection;
    private readonly IDocumentedUserService _documentedUserService;
    private readonly IMongoCollection<Language> _languageCollection;
    private readonly IMongoCollection<Education> _educationCollection;
    private readonly IMongoCollection<Experience> _experienceCollection;
    private readonly IMongoCollection<PersonalInformation> _personalInformationCollection;

    private static readonly FilterDefinitionBuilder<CV> CvFilter = new();
    private static readonly FilterDefinitionBuilder<Passion> PassionFilter = new();
    
    private static readonly FindOneAndUpdateOptions<Language> LanguageUpsertOptions = new() { ReturnDocument = ReturnDocument.After };
    private static readonly FindOneAndUpdateOptions<Passion> PassionUpsertOptions = new() { ReturnDocument = ReturnDocument.After };

    public DocumentedCvService(
        IOptions<MongoSettings> mongoSettings,
        IDocumentedUserService documentedUserService)
    {
        _documentedUserService = documentedUserService;
        var mongoClient = new MongoClient(mongoSettings.Value.DocumentedDb);
        
        var mongoDatabase = mongoClient.GetDatabase("default");

        _cvCollection = mongoDatabase.GetCollection<CV>(nameof(CV));
        _passionCollection = mongoDatabase.GetCollection<Passion>(nameof(Passion));
        _languageCollection = mongoDatabase.GetCollection<Language>(nameof(Language));
        _educationCollection = mongoDatabase.GetCollection<Education>(nameof(Education));
        _experienceCollection = mongoDatabase.GetCollection<Experience>(nameof(Experience));
        _personalInformationCollection = mongoDatabase.GetCollection<PersonalInformation>(nameof(PersonalInformation));

        _documentedUserService = documentedUserService;
    }
    
    public async Task FillWithRandomAsync(int toBeGenerated)
    {
        var defaultSeeds = await CheckDefaultSeeds();
        
        var users = toBeGenerated.GenerateUsers();
        await _documentedUserService.InsertManyAsync(users);
        
        var cvs = new List<CV>(toBeGenerated);

        var updateUsersTasks = new List<Task>(users.Count);
        
        foreach (var user in users)
        {
            var cvId = Guid.NewGuid();

            var passions = 4.GeneratePassions(cvId);
            var languages = 4.GenerateLanguages(cvId);
            var educations = 4.GenerateEducations(cvId);
            var experiences = 4.GenerateExperiences(cvId);
            var personalInformation = GenExtensions.GeneratePersonalInformation(user.Id, cvId);

            languages.Add(defaultSeeds.language);
            passions.Add(defaultSeeds.passion);
            
            var similarLanguages = languages
                .Select(lang => _languageCollection.FindOneAndUpdateAsync(
                    Builders<Language>.Filter.Where(rec => rec.Name.Equals(lang.Name)),
                    Builders<Language>.Update.AddToSet(x => x.CvMongoIds, cvId),
                    LanguageUpsertOptions))
                .ToArray();
            
            var similarPassions = passions
                .Select(pass => _passionCollection.FindOneAndUpdateAsync(
                    Builders<Passion>.Filter.Where(p => p.Name.Equals(pass.Name)),
                    Builders<Passion>.Update.AddToSet(x => x.CvMongoIds, cvId),
                    PassionUpsertOptions))
                .ToArray();

            await Task.WhenAll(Task.WhenAll(similarLanguages), Task.WhenAll(similarPassions));

            languages = similarLanguages
                .Where(x => x.Result is not null)
                .Select(x => x.Result)
                .Union(languages)
                .DistinctBy(x => x.Name)
                .ToArray();
            
            passions = similarPassions
                .Where(x => x.Result is not null)
                .Select(x => x.Result)
                .Union(passions)
                .DistinctBy(x => x.Name)
                .ToArray();

            var tasks = new List<Task>(5)
            {
                _educationCollection.InsertManyAsync(educations),
                _experienceCollection.InsertManyAsync(experiences),
                _personalInformationCollection.InsertOneAsync(personalInformation)
            };

            if (passions.Any(passion => passion.Id == Guid.Empty))
            {
                tasks.Add(_passionCollection.InsertManyAsync(passions.Where(language => language.Id == Guid.Empty)));
            }
            
            if (languages.Any(language => language.Id == Guid.Empty))
            {
                tasks.Add(_languageCollection.InsertManyAsync(languages.Where(language => language.Id == Guid.Empty)));
            }

            await Task.WhenAll(tasks);

            cvs.Add(GenExtensions.GetCvFaker()
                .RuleFor(x => x.Id, _ => cvId)
                .RuleFor(x=> x.User, _ => user)
                .RuleFor(x => x.UserMongoId, _ => user.Id)
                .RuleFor(x => x.LanguageMongoIds, _ => languages.Select(y=> y.Id).ToArray())
                .RuleFor(x=> x.Languages, _ => languages)
                .RuleFor(x => x.EducationMongoIds, _ => educations.Select(y=> y.Id).ToArray())
                .RuleFor(x=> x.Educations, _ => educations)
                .RuleFor(x => x.ExperienceMongoIds, _ => experiences.Select(y=> y.Id).ToArray())
                .RuleFor(x=> x.Experiences, _ => experiences)
                .RuleFor(x => x.PassionMongoIds, _ => passions.Select(y=> y.Id).ToList())
                .RuleFor(x=> x.Passions, _ => passions)
                .RuleFor(x => x.PersonalInformationMongoId, _ => personalInformation.Id)
                .RuleFor(x=> x.PersonalInformation, _ => personalInformation)
                .Generate());

            updateUsersTasks.Add(_documentedUserService.InsertCvIdAsync(user.Id, cvId));
        }

        await Task.WhenAll(updateUsersTasks);
        await _cvCollection.InsertManyAsync(cvs);
    }

    public Task CreateAsync(CV cv)
    {
        return _cvCollection.InsertOneAsync(cv);
    }

    public async Task<CvModel?> GetAsync(Guid cvId)
    {
        var cv = await _cvCollection.Find(cv => cv.Id == cvId).FirstOrDefaultAsync();
       
        return cv.Adapt<CvModel?>();
    }

    public async Task<List<CV>> GetAsync(IEnumerable<Guid> cvIds)
    {
        var cvs = await _cvCollection.Find(CvFilter.In(x => x.Id, cvIds)).ToListAsync();

        return cvs;
    }
    
    public async Task<CvModel?> GetByPersonAsync(Guid userId)
    {
        var cv = await _cvCollection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.UserMongoId.Equals(userId));

        return cv?.Adapt<CvModel>();
    }

    public async Task<List<CvModel?>> GetByLanguageAsync(string language)
    {
        var cvs = await _cvCollection.Find(x => x.Languages.Any(y => y.Name.Equals(language))).ToListAsync();

        return cvs.Adapt<List<CvModel?>>();
    }

    public async Task<PassionModel[]?> GetPassionsAsync(Guid cvId)
    {
        var filter = PassionFilter.Where(x => x.CvMongoIds.Contains(cvId));
        var passions = await _passionCollection
            .Find(filter)
            .ToListAsync();
        
        return passions?.Adapt<PassionModel[]?>();
    }

    public async Task<IEnumerable<string>?> GetCitiesAsync(Guid cvId)
    {
        var cv = await _cvCollection
            .AsQueryable()
            .FirstOrDefaultAsync(cv => cv.Id == cvId);

        if (cv is null)
        {
            return default;
        }

        return cv.Educations.Select(x => x.City).Union(cv.Experiences.Select(x => x.City))
            .Union(new[] { cv.PersonalInformation.City });
    }

    public async Task<List<PassionModel>?> GetPassionsByCityAsync(string city)
    {
        var cvIdsRaw = await _cvCollection
            .AsQueryable()
            .Where(x => x.PersonalInformation.City == city 
                        || x.Experiences.Any(y => y.City == city)
                        || x.Educations.Any(y=> y.City == city))
            .SelectMany(x=> x.Passions)
            .ToListAsync();

        if (cvIdsRaw is null)
        {
            return default;
        }

        return cvIdsRaw.Adapt<List<PassionModel>>();
    }

    public async Task<Dictionary<string, IEnumerable<UserModel>>> GetUsersByCompaniesAsync()
    {
        var cvIds = await _experienceCollection
            .AsQueryable()
            .Select(y => new { y.CvMongoIds, Company = y.Name })
            .ToListAsync();

        var result = cvIds
            .Select(async x => (await _documentedUserService.GetModelAsync(x.CvMongoIds), x.Company));

        return result.Select(x=> x.Result)
            .GroupBy(x=> x.Company)
            .ToDictionary(y=> y.Key, y=> y.SelectMany(z=> z.Item1));
    }

    private async Task<(Language language, Passion passion)> CheckDefaultSeeds()
    {
        var language = await _languageCollection.Find(rec => rec.Name.Equals("English")).FirstOrDefaultAsync();
        var passion = await _passionCollection.Find(rec => rec.Name.Equals("CS")).FirstOrDefaultAsync();
        
        if (language is null)
        {
            language = new Language
            {
                Name = "English",
                Level = "B2",
                CvMongoIds = new List<Guid>(),
            };
            
            await _languageCollection.InsertOneAsync(language);
        }

        if (passion is not null)
        {
            return (language, passion);
        }
        
        passion = new Passion
        {
            Name = "CS",
            CvMongoIds = new List<Guid>(),
        };
            
        await _passionCollection.InsertOneAsync(passion);

        return (language, passion);
    }
}