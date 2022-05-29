using Bogus;
using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Enum;

namespace DistEduDatabases2.Common.Generation;

public static class GenExtensions
{
    private static readonly Faker Faker = new();
    private static readonly Faker<CV> CvFaker = new();
    private static readonly Faker<Language> LangFaker = new();
    private static readonly Faker<Passion> PassionFaker = new();
    private static readonly Faker<Experience> ExperienceFaker = new();
    private static readonly Faker<User> UserFaker = new();
    private static readonly Faker<Education> EducationFaker = new();
    public static Faker<CV> GetCvFaker() => CvFaker;
    public static List<User> GenerateUsers(this int toBeGenerated)
    {
        return UserFaker
            .RuleFor(x => x.Login, y => y.Person.UserName)
            .RuleFor(x => x.Password, y => y.Random.String2(5,10))
            .Generate(toBeGenerated);
    }

    public static List<User> GenerateUsersGraph(this int toBeGenerated)
    {
        return UserFaker
            .RuleFor(x=> x.Id, _ => Guid.NewGuid())
            .RuleFor(x=> x.Name, y => y.Name.FirstName())
            .RuleFor(x => x.Login, y => y.Person.UserName)
            .RuleFor(x => x.Password, y => y.Random.String2(5,10))
            .Generate(toBeGenerated);
    }
    
    public static ICollection<Education> GenerateEducations(this int toBeGenerated)
    {
        return EducationFaker
            .RuleFor(x => x.City, y => y.Address.City())
            .RuleFor(x => x.Degree, y => y.Person.Company.Name)
            .RuleFor(x => x.StartTime, y => y.Date.PastOffset())
            .RuleFor(x => x.FinishTime, y => y.Date.FutureOffset())
            .RuleFor(x => x.Name, y => y.Company.CompanyName())
            .RuleFor(x=>x.Description, y=>y.Company.CatchPhrase())
            .Generate(toBeGenerated);
    }
    
    public static ICollection<Education> GenerateEducationsGraph(this int toBeGenerated)
    {
        return EducationFaker
            .RuleFor(x=> x.Id, _ => Guid.NewGuid())
            .RuleFor(x=> x.Name, y => y.Name.FirstName())
            .RuleFor(x => x.City, y => y.Address.City())
            .RuleFor(x => x.Degree, y => y.Person.Company.Name)
            .RuleFor(x => x.StartTime, y => y.Date.PastOffset())
            .RuleFor(x => x.FinishTime, y => y.Date.FutureOffset())
            .RuleFor(x => x.Name, y => y.Company.CompanyName())
            .RuleFor(x=>x.Description, y=>y.Company.CatchPhrase())
            .Generate(toBeGenerated);
    }
    
    public static ICollection<Education> GenerateEducations(this int toBeGenerated, Guid cvId)
    {
        return EducationFaker
            .RuleFor(x => x.City, y => y.Address.City())
            .RuleFor(x => x.Degree, y => y.Person.Company.Name)
            .RuleFor(x => x.StartTime, y => y.Date.PastOffset())
            .RuleFor(x => x.FinishTime, y => y.Date.FutureOffset())
            .RuleFor(x => x.Name, y => y.Company.CompanyName())
            .RuleFor(x=>x.Description, y=>y.Company.CatchPhrase())
            .RuleFor(x=> x.CvMongoId, _ => cvId)
            .Generate(toBeGenerated);
    }

    public static ICollection<Experience> GenerateExperiences(this int toBeGenerated)
    {
        return ExperienceFaker
            .RuleFor(x => x.City, y => y.Address.City())
            .RuleFor(x=>x.Name, y=>y.Company.CompanyName())
            .RuleFor(x=>x.MajorProjects,y=>y.Random.Words())
            .RuleFor(x=>x.Designation, y=>y.Finance.AccountName())
            .RuleFor(x => x.StartDate, y => y.Date.PastOffset())
            .RuleFor(x => x.FinishDate, y => y.Date.FutureOffset())
            .Generate(toBeGenerated);
    }
    
    public static ICollection<Experience> GenerateExperiencesGraph(this int toBeGenerated)
    {
        return ExperienceFaker
            .RuleFor(x=> x.Id, _ => Guid.NewGuid())
            .RuleFor(x=> x.Name, y => y.Name.FirstName())
            .RuleFor(x => x.City, y => y.Address.City())
            .RuleFor(x=>x.MajorProjects,y=>y.Random.Words())
            .RuleFor(x=>x.Designation, y=>y.Finance.AccountName())
            .RuleFor(x => x.StartDate, y => y.Date.PastOffset())
            .RuleFor(x => x.FinishDate, y => y.Date.FutureOffset())
            .Generate(toBeGenerated);
    }
    
    public static ICollection<Experience> GenerateExperiences(this int toBeGenerated, Guid cvId)
    {
        return ExperienceFaker
            .RuleFor(x => x.City, y => y.Address.City())
            .RuleFor(x=> x.Name, y=>y.Company.CompanyName())
            .RuleFor(x=> x.MajorProjects,y=>y.Random.Words())
            .RuleFor(x=> x.Designation, y=>y.Finance.AccountName())
            .RuleFor(x => x.StartDate, y => y.Date.PastOffset())
            .RuleFor(x => x.FinishDate, y => y.Date.FutureOffset())
            .RuleFor(x=> x.CvMongoIds, _ => new List<Guid> { cvId })
            .Generate(toBeGenerated);
    }

    public static ICollection<Passion> GeneratePassions(this int toBeGenerated)
    {
        return PassionFaker
            .RuleFor(x => x.Name, y => y.Music.Genre())
            .Generate(toBeGenerated);
    }
    
    public static ICollection<Passion> GeneratePassionsGraph(this int toBeGenerated)
    {
        return PassionFaker
            .RuleFor(x=> x.Id, _ => Guid.NewGuid())
            .RuleFor(x=> x.Name, y => y.Name.FirstName())
            .RuleFor(x => x.Name, y => y.Music.Genre())
            .Generate(toBeGenerated);
    }
    
    public static ICollection<Passion> GeneratePassions(this int toBeGenerated, Guid cvId)
    {
        return PassionFaker
            .RuleFor(x => x.Name, y => y.Music.Genre())
            .RuleFor(x=> x.CvMongoIds, _ => new List<Guid> { cvId })
            .Generate(toBeGenerated);
    }

    public static ICollection<Language> GenerateLanguages(this int entitiesCount)
    {
        return LangFaker
            .RuleFor(x => x.Level, y => y.Company.CompanyName())
            .RuleFor(x => x.Name, y => y.Hacker.Verb())
            .Generate(entitiesCount);
    }
    
    public static ICollection<Language> GenerateLanguagesGraph(this int entitiesCount)
    {
        return LangFaker
            .RuleFor(x=> x.Id, _ => Guid.NewGuid())
            .RuleFor(x=> x.Name, y => y.Name.FirstName())
            .RuleFor(x => x.Level, y => y.Company.CompanyName())
            .Generate(entitiesCount);
    }
    
    public static ICollection<Language> GenerateLanguages(this int entitiesCount, Guid cvId)
    {
        return LangFaker
            .RuleFor(x => x.Level, y => y.Company.CompanyName())
            .RuleFor(x => x.Name, y => y.Name.FirstName())
            .RuleFor(x=> x.CvMongoIds, _ => new List<Guid> { cvId })
            .Generate(entitiesCount);
    }
    
    public static PersonalInformation GeneratePersonalInformation(Guid userId, Guid cvId)
    {
        return new PersonalInformation
        {
            UserMongoId = userId,
            CvMongoId = cvId,
            Name = Faker.Person.FirstName,
            MiddleName = Faker.Person.UserName,
            LastName = Faker.Person.LastName,
            Birthday = Faker.Person.DateOfBirth,
            BirthPlace = Faker.Address.FullAddress(),
            City = Faker.Address.City(),
            Age = Faker.Random.Int(18,60),
            Nationality = Faker.Random.String2(5, 10),
            Religion = Faker.Random.String2(5, 10),
            CivilStatus = Faker.Random.Enum<CivilStatus>(),
            Phone = Faker.Person.Phone,
            Email = Faker.Person.Email,
            Description = Faker.Lorem.Paragraph()
        };
    }
    
    public static PersonalInformation GeneratePersonalInformationGraph(Guid userId, Guid cvId)
    {
        return new PersonalInformation
        {
            Id = Guid.NewGuid(),
            UserMongoId = userId,
            CvMongoId = cvId,
            Name = Faker.Person.FirstName,
            MiddleName = Faker.Person.UserName,
            LastName = Faker.Person.LastName,
            Birthday = Faker.Person.DateOfBirth,
            BirthPlace = Faker.Address.FullAddress(),
            City = Faker.Address.City(),
            Age = Faker.Random.Int(18,60),
            Nationality = Faker.Random.String2(5, 10),
            Religion = Faker.Random.String2(5, 10),
            CivilStatus = Faker.Random.Enum<CivilStatus>(),
            Phone = Faker.Person.Phone,
            Email = Faker.Person.Email,
            Description = Faker.Lorem.Paragraph()
        };
    }
    
    public static PersonalInformation GeneratePersonalInformation(this Faker faker, User user)
    {
        return new PersonalInformation
        {
            User = user,
            Name = faker.Person.FirstName,
            MiddleName = faker.Person.UserName,
            LastName = faker.Person.LastName,
            Birthday = faker.Person.DateOfBirth,
            BirthPlace = faker.Address.FullAddress(),
            City = faker.Address.City(),
            Age = faker.Random.Int(18,60),
            Nationality = faker.Random.String2(5, 10),
            Religion = faker.Random.String2(5, 10),
            CivilStatus = faker.Random.Enum<CivilStatus>(),
            Phone = faker.Person.Phone,
            Email = faker.Person.Email,
            Description = faker.Lorem.Paragraph()
        };
    }
}