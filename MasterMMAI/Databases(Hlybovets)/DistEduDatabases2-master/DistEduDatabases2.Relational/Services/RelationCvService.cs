using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Generation;
using DistEduDatabases2.Common.Models;
using DistEduDatabases2.Relational.Context;
using DistEduDatabases2.Relational.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DistEduDatabases2.Relational.Services;

internal sealed class RelationCvService : IRelationCvService
{
    private readonly ServerContext _serverContext;
    
    public RelationCvService(ServerContext serverContext)
    {
        _serverContext = serverContext ?? throw new ArgumentNullException(nameof(serverContext));
    }

    public async Task FillWithRandomAsync(int toBeGenerated)
    {
        var users = toBeGenerated.GenerateUsers();

        var lang = await _serverContext.Languages.FirstOrDefaultAsync(x => x.Name.Equals("English"));
        
        if (lang is null)
        {
            lang = new Language
            {
                Name = "English",
                Level = "B2"
            };

            _serverContext.Languages.Add(lang);
        }

        var passion = await _serverContext.Passions.FirstOrDefaultAsync(x => x.Name.Equals("CS"));
        
        if (passion is null)
        {
            passion = new Passion
            {
                Name = "CS"
            };

            _serverContext.Passions.Add(passion);
        }

        var cvs = new List<CV>(toBeGenerated);
        
        _serverContext.AddRange(users);

        await _serverContext.SaveChangesAsync();

        cvs.AddRange(users.Select(user => GenExtensions.GetCvFaker().RuleFor(x => x.User, _ => user)
            .RuleFor(x => x.Languages, _ =>
            {
                var languages = 4.GenerateLanguages();
                languages.Add(lang);

                return languages;
            })
            .RuleFor(x => x.Educations, _ => 4.GenerateEducations())
            .RuleFor(x => x.Experiences, _ => 4.GenerateExperiences())
            .RuleFor(x => x.Passions, _ =>
            {
                var passions = 4.GeneratePassions();
                passions.Add(passion);

                return passions;
            })
            .RuleFor(x => x.PersonalInformation, y => y.GeneratePersonalInformation(user))
            .Generate()));

        _serverContext.AddRange(cvs);

        await _serverContext.SaveChangesAsync();
    }
    public Task CreateAsync(CV cv)
    {
        _serverContext.Add(cv);

        return _serverContext.SaveChangesAsync();
    }
    
    public async Task<CvModel?> GetAsync(Guid userId)
    {
        var test = await _serverContext.Cvs
            .Include(x=> x.User)
            .Include(x=>x.Educations)
            .Include(x=>x.Experiences)
            .Include(x=>x.Languages)
            .Include(x=>x.Passions)
            .Include(x=>x.PersonalInformation)
            .FirstOrDefaultAsync(cv => cv.User.Id == userId);

        return test?.Adapt<CvModel>();
    }

    public Task<PassionModel[]> GetPassionsAsync(Guid cvId)
    {
        return _serverContext.Cvs
            .Where(cv => cv.Id == cvId)
            .SelectMany(cv => cv.Passions)
            .ProjectToType<PassionModel>()
            .ToArrayAsync();
    }

    public IEnumerable<string> GetCitiesAsync(Guid cvId)
    {
        return _serverContext.Cvs.Where(x => x.Id == cvId)
            .Select(x => new
            {
                EducationCities = x.Educations.Select(y => y.City),
                ExperienceCities = x.Experiences.Select(y => y.City),
                x.PersonalInformation.City
            })
            .AsEnumerable()
            .SelectMany(x => x.EducationCities.Union(x.ExperienceCities).Union(new[] { x.City }));
    }

    public Task<PassionModel[]>? GetPassionsByCityAsync(string city)
    {
        return _serverContext.Cvs
            .Include(x => x.Passions)
            .Include(x=> x.Experiences)
            .Where(x => x.PersonalInformation.City.ToLower().Equals(city.ToLower()) || x.Experiences.Any(y=> y.City.ToLower().Equals(city.ToLower())))
            ?.SelectMany(x => x.Passions)
            ?.ProjectToType<PassionModel>()
            .ToArrayAsync();
    }
    
    public async Task<CvModel[]> GetByLanguageAsync(string language)
    {
        language = language.ToLower();
        var cvIds = await _serverContext.Languages
            .Include(x => x.Cvs)
            .Where(x => x.Name.ToLower().Equals(language))
            .SelectMany(x => x.Cvs.Select(y => y.Id))
            .ToArrayAsync();

        var cvs = _serverContext.Cvs
            .Include(x => x.Educations)
            .Include(x => x.Experiences)
            .Include(x => x.Languages)
            .Include(x => x.Passions)
            .Include(x => x.PersonalInformation)
            .Where(x => cvIds.Contains(x.Id))
            .ProjectToType<CvModel>()
            .ToArrayAsync();

        return await cvs;
    }

    public Task<Dictionary<string, IEnumerable<UserModel>>> GetUsersByCompaniesAsync()
    {
        var users = _serverContext.Cvs
            .Include(x => x.User)
            .Include(x => x.Experiences)
            .AsEnumerable()
            .SelectMany(x => x.Experiences.Select(y => new { x.User, Companies = y.Name }))
            .GroupBy(x => x.Companies)
            .ToDictionary(x => x.Key, x => x.Select(y=> y.User).Adapt<IEnumerable<UserModel>>());

        return Task.FromResult(users);
    }
}