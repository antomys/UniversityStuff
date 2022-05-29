using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Models;

namespace DistEduDatabases2.Relational.Services.Interfaces;

public interface IRelationCvService
{
    Task FillWithRandomAsync(int toBeGenerated);
    
    Task CreateAsync(CV cv);
    
    Task<CvModel?> GetAsync(Guid userId);

    Task<CvModel[]> GetByLanguageAsync(string language);
    
    Task<PassionModel[]> GetPassionsAsync(Guid cvId);
    
    IEnumerable<string> GetCitiesAsync(Guid cvId);
    
    Task<PassionModel[]>? GetPassionsByCityAsync(string city);
    
    Task<Dictionary<string, IEnumerable<UserModel>>> GetUsersByCompaniesAsync();
}