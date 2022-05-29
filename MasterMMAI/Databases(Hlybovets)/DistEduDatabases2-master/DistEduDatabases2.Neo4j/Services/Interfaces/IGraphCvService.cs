using DistEduDatabases2.Common.Models;

namespace DistEduDatabases2.Neo4j.Services.Interfaces;

public interface IGraphCvService
{
    Task SeedDatabase(int toBeGenerated);
    
    Task<CvModel?> GetCvAsync(string cvName);

    Task<IEnumerable<PassionModel>?> GetPassionsAsync(string cvName);
    
    Task<IEnumerable<string>?> GetCitiesAsync(string cvName);
    
    Task<IEnumerable<PassionModel>> GetPassionsByCityAsync(string city);
    
    Task<Dictionary<string, List<UserModel>?>> GetUsersByCompaniesAsync();
}