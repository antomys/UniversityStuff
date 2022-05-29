using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DistEduDatabases2.Common.Entities;

[Table(nameof(Education))]
public class Education
{
    [Key]
    [BsonRepresentation(BsonType.String)]
    [BsonId(IdGenerator = typeof(GuidGenerator))]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string City { get; set; }
    
    public DateTimeOffset StartTime { get; set; }
    
    public DateTimeOffset FinishTime { get; set; }
    
    public string Degree { get; set; }
    
    public string Description { get; set; }
    
    [NotMapped]
    public Guid CvMongoId { get; set; }
}