namespace DistEduDatabases2.Common.Models;

public sealed class EducationModel : BaseClass
{
    public Guid Id { get; set; }
    
    
    public string City { get; set; }
    
    public DateTimeOffset StartTime { get; set; }
    
    public DateTimeOffset FinishTime { get; set; }
    
    public string Degree { get; set; }
    
    public string Description { get; set; }
}