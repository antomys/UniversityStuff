using DistEduDatabases2.Common.Generation;
using DistEduDatabases2.Common.Models;
using DistEduDatabases2.Neo4j.Services.Interfaces;
using Mapster;
using Neo4jClient;

namespace DistEduDatabases2.Neo4j.Services;

public class GraphCvService : IGraphCvService
{
    private readonly IGraphClient _graphClient;
    
    private static readonly LanguageModel DefaultLanguage = new()
    {
        Name = "English",
        Id = Guid.NewGuid(),
        Level = "B2"
    };
    
    private static readonly PassionModel DefaultPassion = new()
    {
        Name = "CS",
        Id = Guid.NewGuid()
    };
    
    public GraphCvService(IGraphClient graphClient)
    {
        _graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
    }

    public async Task SeedDatabase(int toBeGenerated)
    {
        await CheckDefaults();
       
        var users = toBeGenerated.GenerateUsersGraph().Adapt<ICollection<UserModel>>();
        await Task.WhenAll(users.Select(x => UpsertAsync(x)));

        foreach (var user in users)
        {
            var query = _graphClient.Cypher;
            
            var cvName = $"{user.Name}{nameof(CvModel)}";
            
            var tasks = new List<Task>(16);
            var languages = 4.GenerateLanguagesGraph().Adapt<List<LanguageModel>>();
            languages.Add(DefaultLanguage);
            var experiences = 4.GenerateExperiencesGraph().Adapt<List<ExperienceModel>>();
            var educations = 4.GenerateEducationsGraph().Adapt<List<EducationModel>>();
            var passions = 4.GeneratePassionsGraph().Adapt<List<PassionModel>>();
            passions.Add(DefaultPassion);
        
            tasks.AddRange(languages.Select(x=> UpsertAsync(x)));
            tasks.AddRange(experiences.Select(x=> UpsertAsync(x)));
            tasks.AddRange(educations.Select(x=> UpsertAsync(x)));
            tasks.AddRange(passions.Select(x=> UpsertAsync(x)));

            await Task.WhenAll(tasks);
            
            var cv = GenExtensions.GetCvFaker()
                .RuleFor(x => x.Id, _ => Guid.NewGuid())
                .RuleFor(x => x.Name, _ => cvName)
                .Generate()
                .Adapt<CvModel>();

            var userProfile = GenExtensions.GeneratePersonalInformationGraph(user.Id, cv.Id);
            
            query = query
                .Create($"({cvName}:{nameof(CvModel)} $cv)")
                .WithParam("cv", cv)
                .With(cvName)
                .Match($"(userModel:{nameof(UserModel)})")
                .Where((UserModel userModel) => userModel.Id == user.Id)
                .Merge($"(userModel)-[:OWNS]->({cvName})")
                .With(cvName);

            query = languages.Aggregate(query, (current, language) => current.Match($"(lang:{nameof(LanguageModel)})")
                .Where((LanguageModel lang) => lang.Name == language.Name)
                .Merge($"(lang)-[:IS_KNOWN]->({cvName})")
                .With(cvName));

            query = experiences.Aggregate(query, (current, experience) => current.Match($"(exp:{nameof(ExperienceModel)})")
                .Where((ExperienceModel exp) => exp.Name == experience.Name)
                .Merge($"(exp)-[:EXPERIENCED]->({cvName})")
                .With(cvName));

            query = passions.Aggregate(query, (current, passion) => current.Match($"(pas:{nameof(PassionModel)})")
                .Where((PassionModel pas) => pas.Name == passion.Name)
                .Merge($"(pas)-[:PASSIONED]->({cvName})")
                .With(cvName));

            query = educations.Aggregate(query, (current, education) => current.Match($"(edu:{nameof(EducationModel)})")
                .Where((EducationModel edu) => edu.Name == education.Name)
                .Merge($"(edu)-[:EDUCATED_AT]->({cvName})")
                .With(cvName));

            query = query
                .Create($"({user.Name}Info:{nameof(PersonalInformationModel)} $userProfile)")
                .WithParam("userProfile", userProfile)
                .Merge($"({user.Name}Info)-[:DESCRIBES_USER]->({cvName})");
            
            await query.ExecuteWithoutResultsAsync();
        }
    }

    private Task CheckDefaults()
    {
        return Task.WhenAll(_graphClient.Cypher
            .Merge($"(lang:{nameof(LanguageModel)} {{ Name: $name }})")
            .OnCreate()
            .Set("lang = $language")
            .WithParams(new
            {
                name = DefaultLanguage.Name,
                language = DefaultLanguage
            }).ExecuteWithoutResultsAsync(), 
            _graphClient.Cypher
            .Merge($"(pass:{nameof(PassionModel)} {{ Name: $name }})")
            .OnCreate()
            .Set("pass = $passion")
            .WithParams(new
            {
                name = DefaultLanguage.Name,
                passion = DefaultPassion
            }).ExecuteWithoutResultsAsync());
    }

    private Task UpsertAsync<T>(T newTmp) where T : BaseClass
    {
        return _graphClient.Cypher
            .Merge($"(tmp:{typeof(T).Name} {{ Name: $name }})")
            .OnCreate()
            .Set("tmp = $newTmp")
            .WithParams(new
            {
                name = newTmp.Name,
                newTmp
            }).ExecuteWithoutResultsAsync();
    }

    public async Task<CvModel?> GetCvAsync(string cvName)
    {
        var result = await _graphClient.Cypher
            .OptionalMatch($"(cv:{nameof(CvModel)} {{ Name : $cvName}})-[:PASSIONED]-(passions:{nameof(PassionModel)})")
            .OptionalMatch($"(cv:{nameof(CvModel)} {{ Name : $cvName}})-[:EXPERIENCED]-(experiences:{nameof(ExperienceModel)})")
            .OptionalMatch($"(cv:{nameof(CvModel)} {{ Name : $cvName}})-[:IS_KNOWN]-(languages:{nameof(LanguageModel)})")
            .OptionalMatch($"(cv:{nameof(CvModel)} {{ Name : $cvName}})-[:EDUCATED_AT]-(educations:{nameof(EducationModel)})")
            .OptionalMatch(
                $"(cv:{nameof(CvModel)} {{ Name : $cvName}})-[:DESCRIBES_USER]-(personalInfo:{nameof(PersonalInformationModel)})")
            .WithParam("cvName", cvName)
            .Return((cv, passions, experiences, languages, educations, personalInfo)
                => new
                {
                    cv.As<CvModel>().Name,
                    PersonalInformation = personalInfo.As<PersonalInformationModel>(),
                    Experiences = experiences.CollectAs<ExperienceModel>(),
                    Passions = passions.CollectAs<PassionModel>(),
                    Educations = educations.CollectAs<EducationModel>(),
                    Languages = languages.CollectAs<LanguageModel>(),
                }).ResultsAsync;

        var realRes = result.FirstOrDefault(x => x.Name == cvName);
        
        if (realRes is null)
        {
            return default;
        }
        
        return new CvModel
        {
            Name = realRes.Name,
            PersonalInformation = realRes.PersonalInformation,
            Educations = realRes.Educations.DistinctBy(x=> x.Name).ToArray(),
            Experiences = realRes.Experiences.DistinctBy(x=> x.Name).ToArray(),
            Passions = realRes.Passions.DistinctBy(x=> x.Name).ToArray(),
            Languages = realRes.Languages.DistinctBy(x=> x.Name).ToArray(),
        };
    }

    public async Task<IEnumerable<PassionModel>?> GetPassionsAsync(string cvName)
    {
        var result = await _graphClient.Cypher
            .OptionalMatch($"(cv:{nameof(CvModel)} {{ Name : $cvName}})-[:PASSIONED]-(passions:{nameof(PassionModel)})")
            .WithParam("cvName", cvName)
            .Return((cv, passions) => new
            {
                Cv = cv.As<CvModel>(),
                Passions = passions.CollectAs<PassionModel>()
            }).ResultsAsync;

        var realRes = result.FirstOrDefault(x => x.Cv.Name == cvName);
        
        return realRes?.Passions.DistinctBy(x=> x.Name);
    }

    public async Task<IEnumerable<string>?> GetCitiesAsync(string cvName)
    {
        var result = await _graphClient.Cypher
            .OptionalMatch($"(cv:{nameof(CvModel)} {{ Name : $cvName}})-[:EXPERIENCED]-(experiences:{nameof(ExperienceModel)})")
            .OptionalMatch($"(cv:{nameof(CvModel)} {{ Name : $cvName}})-[:DESCRIBES_USER]-(personalInfo:{nameof(PersonalInformationModel)})")
            .WithParam("cvName", cvName)
            .Return((cv, experiences, personalInfo) => new
            {
                Cv = cv.As<CvModel>(),
                Experiences = experiences.CollectAs<ExperienceModel>(),
                PersonalInfo = personalInfo.As<PersonalInformationModel>()
            }).ResultsAsync;

        var realRes = result.FirstOrDefault(x => x.Cv.Name == cvName);

        if (realRes is null)
        {
            return default;
        }
        
        return realRes.Experiences.Select(y=> y.City).Union(new[] { realRes.PersonalInfo.City });
    }

    public async Task<IEnumerable<PassionModel>> GetPassionsByCityAsync(string city)
    {
        var result = await _graphClient.Cypher
            .OptionalMatch(
                $"(:{nameof(CvModel)})-[:EXPERIENCED]-(experiences:{nameof(ExperienceModel)} {{City : $city}})")
            .OptionalMatch(
                $"(:{nameof(CvModel)})-[:DESCRIBES_USER]-(personalInfo:{nameof(PersonalInformationModel)} {{City : $city}})")
            .OptionalMatch($"(:{nameof(CvModel)})-[:PASSIONED]-(passions:{nameof(PassionModel)})")
            .WithParam("city", city)
            .Return((cv, personalInfo, experiences, passions) => new
            {
                Passions = passions.CollectAs<PassionModel>()
            }).ResultsAsync;
        
        return result.SelectMany(x => x.Passions).DistinctBy(x=> x.Name);
    }

    public async Task<Dictionary<string, List<UserModel>?>> GetUsersByCompaniesAsync()
    {
        var result = await  _graphClient.Cypher
            .OptionalMatch($"(cv:{nameof(CvModel)})-[:EXPERIENCED]-(experiences:{nameof(ExperienceModel)})")
            .OptionalMatch($"(user:{nameof(UserModel)})-[:OWNS]-(cv:{nameof(CvModel)})")
            .Return((cv, experiences, user) => new
            {
                Exp = experiences.CollectAs<ExperienceModel>(),
                User = user.As<UserModel>()
            }).ResultsAsync;

        var resDict = new Dictionary<string, List<UserModel>?>();
        var res = result.ToArray();
        
        var companies = res.SelectMany(x => x.Exp.Select(y => y.Name));

        foreach (var company in companies)
        {
            var intRes = res.Where(x => x.Exp.Any(y => y.Name == company)).Select(y => y.User).DistinctBy(x=> x.Name).ToArray();
            if (resDict.ContainsKey(company))
            {
                var list = resDict[company];
                resDict[company] = list?.Concat(intRes).DistinctBy(x=> x.Name).ToList();
            }
            else
            {
                resDict.Add(company, intRes?.DistinctBy(x=> x.Name).ToList());   
            }
        }

        return resDict;
    }
}