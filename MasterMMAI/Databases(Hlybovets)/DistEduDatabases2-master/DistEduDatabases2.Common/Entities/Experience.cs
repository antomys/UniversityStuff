using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DistEduDatabases2.Common.Entities;

[Table(nameof(Experience))]
public class Experience
{
    [Key]
    [BsonRepresentation(BsonType.String)]
    [BsonId(IdGenerator = typeof(GuidGenerator))]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string City { get; set; }
    
    public string Designation { get; set; }

    public DateTimeOffset StartDate { get; set; }
    
    public DateTimeOffset? FinishDate { get; set; }
    
    public string MajorProjects { get; set; }
    
    [NotMapped]
    public ICollection<Guid> CvMongoIds { get; set; }
}