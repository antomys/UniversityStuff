using DistEduDatabases2.Common.Enum;

namespace DistEduDatabases2.Common.Models;

public sealed class SkillModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Level Level { get; set; }
}