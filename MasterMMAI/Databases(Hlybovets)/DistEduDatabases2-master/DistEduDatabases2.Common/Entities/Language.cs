using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace DistEduDatabases2.Common.Entities;

[Table(nameof(Language))]
public class Language
{
    [Key]
    [BsonRepresentation(BsonType.String)]
    [BsonId(IdGenerator = typeof(GuidGenerator))]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Level { get; set; }
    
    [NotMapped]
    public ICollection<Guid> CvMongoIds { get; set; }

    [BsonIgnore]
    public virtual ICollection<CV> Cvs { get; set; }
}