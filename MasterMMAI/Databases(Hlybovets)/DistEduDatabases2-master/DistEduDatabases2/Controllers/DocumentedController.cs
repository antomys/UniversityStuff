using DistEduDatabases2.Common.Entities;
using DistEduDatabases2.Common.Models;
using DistEduDatabases2.Mongo.Services;
using DistEduDatabases2.Mongo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistEduDatabases2.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DocumentedController : ControllerBase
{
    private readonly IDocumentedCvService _documentedCvService;

    public DocumentedController(IDocumentedCvService documentedCvService)
    {
        _documentedCvService = documentedCvService ?? throw new ArgumentNullException(nameof(documentedCvService));
    }

    [HttpGet]
    public Task SeedDatabase(int toBeGenerated)
    {
        return _documentedCvService.FillWithRandomAsync(toBeGenerated);
    }
    
    [HttpGet]
    public Task<CvModel?> GetAsync(Guid cvId)
    {
        return _documentedCvService.GetAsync(cvId);
    }
    
    [HttpPost]
    public Task CreateAsync(CV cv)
    {
        return _documentedCvService.CreateAsync(cv);
    }

    [HttpGet]
    public Task<PassionModel[]?> GetPassionsAsync(Guid cvId)
    {
        return _documentedCvService.GetPassionsAsync(cvId);
    }

    [HttpGet]
    public Task<IEnumerable<string>> GetCitiesAsync(Guid cvId)
    {
        return _documentedCvService.GetCitiesAsync(cvId);
    }

    [HttpGet]
    public Task<List<PassionModel>?> GetPassionsByCityAsync(string city)
    {
        return _documentedCvService.GetPassionsByCityAsync(city);
    }
    
    [HttpGet]
    public Task<List<CvModel?>> GetByLanguageAsync(string language)
    {
        return _documentedCvService.GetByLanguageAsync(language);
    }

    [HttpGet]
    public Task<Dictionary<string, IEnumerable<UserModel>>> GetUsersByCompaniesAsync()
    {
        return _documentedCvService.GetUsersByCompaniesAsync();
    }
}