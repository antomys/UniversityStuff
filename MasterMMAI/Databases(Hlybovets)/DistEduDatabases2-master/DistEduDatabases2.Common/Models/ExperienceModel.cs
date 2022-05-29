namespace DistEduDatabases2.Common.Models;

public sealed class ExperienceModel  : BaseClass
{
    public Guid Id { get; set; }

    public string City { get; set; }
    
    public string Designation { get; set; }

    public DateTimeOffset StartDate { get; set; }
    
    public DateTimeOffset? FinishDate { get; set; }
    
    public string MajorProjects { get; set; }
}