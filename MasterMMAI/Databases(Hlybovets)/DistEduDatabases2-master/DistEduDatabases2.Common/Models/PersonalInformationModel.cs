using DistEduDatabases2.Common.Enum;

namespace DistEduDatabases2.Common.Models;

public sealed class PersonalInformationModel : BaseClass
{
    public Guid Id { get; set; }

    public string MiddleName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTimeOffset Birthday { get; set; }
    
    public string BirthPlace { get; set; }
    
    public string City { get; set; }
    
    public int Age { get; set; }
    
    public string Nationality { get; set; }
    
    public string Religion { get; set; }
    
    public CivilStatus CivilStatus { get; set; } 
    
    public string Phone { get; set; }
    
    public string Email { get; set; }
    
    public string Description { get; set; }
}