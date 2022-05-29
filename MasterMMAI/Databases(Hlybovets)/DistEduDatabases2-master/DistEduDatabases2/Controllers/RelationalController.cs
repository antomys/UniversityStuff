using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Models;
using DistEduDatabases2.Relational.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistEduDatabases2.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RelationalController : ControllerBase
{
    private readonly IRelationCvService _relationCvService;

    public RelationalController(IRelationCvService relationCvService)
    {
        _relationCvService = relationCvService ?? throw new ArgumentNullException(nameof(relationCvService));
    }

    [HttpGet]
    public Task SeedDatabase(int toBeGenerated)
    {
        return _relationCvService.FillWithRandomAsync(toBeGenerated);
    }
    
    [HttpGet]
    public Task<CvModel?> GetAsync(Guid userId)
    {
        return _relationCvService.GetAsync(userId);
    }
    
    [HttpPost]
    public Task CreateAsync(CV cv)
    {
        return _relationCvService.CreateAsync(cv);
    }

    [HttpGet]
    public Task<PassionModel[]> GetPassionsAsync(Guid cvId)
    {
        return _relationCvService.GetPassionsAsync(cvId);
    }

    [HttpGet]
    public IEnumerable<string> GetCitiesAsync(Guid cvId)
    {
        return _relationCvService.GetCitiesAsync(cvId);
    }

    [HttpGet]
    public Task<PassionModel[]>? GetPassionsByCityAsync(string city)
    {
        return _relationCvService.GetPassionsByCityAsync(city);
    }
    
    [HttpGet]
    public Task<CvModel[]> GetByLanguageAsync(string language)
    {
        return _relationCvService.GetByLanguageAsync(language);
    }

    [HttpGet]
    public Task<Dictionary<string, IEnumerable<UserModel>>> GetUsersByCompaniesAsync()
    {
        return _relationCvService.GetUsersByCompaniesAsync();
    }
}