using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Models;

namespace DistEduDatabases2.Mongo.Services.Interfaces;

public interface IDocumentedCvService
{
    Task FillWithRandomAsync(int toBeGenerated);
    
    Task CreateAsync(CV cv);
    
    Task<CvModel?> GetAsync(Guid userId);

    Task<CvModel?> GetByPersonAsync(Guid userId);

    Task<List<CvModel?>> GetByLanguageAsync(string language);
    
    Task<PassionModel[]?> GetPassionsAsync(Guid cvId);
    
    Task<IEnumerable<string>?> GetCitiesAsync(Guid cvId);
    
    Task<List<PassionModel>?> GetPassionsByCityAsync(string city);
    
    Task<Dictionary<string, IEnumerable<UserModel>>> GetUsersByCompaniesAsync();
}