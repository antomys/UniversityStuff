using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DistEduDatabases2.Common.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DistEduDatabases2.Common.Entities;

[Table(nameof(Skill))]
public class Skill
{
    [Key]
    [BsonRepresentation(BsonType.String)]
    [BsonId(IdGenerator = typeof(GuidGenerator))]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Level Level { get; set; }
    
    [NotMapped]
    public ICollection<Guid> CvMongoIds { get; set; }
}