using DistEduDatabases2.Common.Models;
using DistEduDatabases2.Neo4j.Services;
using DistEduDatabases2.Neo4j.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistEduDatabases2.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class GraphController : ControllerBase
{
    private readonly IGraphUserService _graphUserService;
    private readonly IGraphCvService _graphCvService;

    public GraphController(IGraphUserService graphUserService, IGraphCvService graphCvService)
    {
        _graphUserService = graphUserService ?? throw new ArgumentNullException(nameof(graphUserService));
        _graphCvService = graphCvService;
    }

    [HttpGet]
    public Task SeedDatabaseAsync(int toBeGenerated = 1)
    {
        return _graphCvService.SeedDatabase(toBeGenerated);
    }
    
    [HttpGet]
    public Task<CvModel?> GetCvAsync(string cvName)
    {
        return _graphCvService.GetCvAsync(cvName);
    }
    
    [HttpGet]
    public Task<IEnumerable<PassionModel>?> GetPassionsAsync(string cvName)
    {
        return _graphCvService.GetPassionsAsync(cvName);
    }
    
    [HttpGet]
    public Task<IEnumerable<string>?> GetCitiesAsync(string cvName)
    {
        return _graphCvService.GetCitiesAsync(cvName);
    }

    [HttpGet]
    public Task<IEnumerable<PassionModel>> GetPassionsByCityAsync(string city)
    {
        return _graphCvService.GetPassionsByCityAsync(city);
    }
    [HttpGet]
    public Task<Dictionary<string, List<UserModel>?>> GetUsersByCompaniesAsync()
    {
        return _graphCvService.GetUsersByCompaniesAsync();
    }
    
    [HttpGet]
    public Task<UserModel?> AddUserAsync(string login, string password)
    {
        return _graphUserService.CreateAsync(login, password);
    }
    
    [HttpGet]
    public Task GetUserAsync(Guid userId)
    {
        return _graphUserService.GetAsync(userId);
    }
}