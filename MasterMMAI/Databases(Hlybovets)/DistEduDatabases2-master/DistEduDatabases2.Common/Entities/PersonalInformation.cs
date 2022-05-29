using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DistEduDatabases2.Common.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DistEduDatabases2.Common.Entities;

[Table(nameof(PersonalInformation))]
public class PersonalInformation
{
    [Key]
    [BsonRepresentation(BsonType.String)]
    [BsonId(IdGenerator = typeof(GuidGenerator))]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [NotMapped]
    public Guid UserMongoId { get; set; }
    
    [BsonIgnore]
    public virtual User User { get; set; }
    
    public string Name { get; set; }
    
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
    
    [NotMapped]
    public Guid CvMongoId { get; set; }
}