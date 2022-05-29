using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DistEduDatabases2.Common.Entities;

[Table(nameof(CV))]
public class CV
{
    [Key]
    [BsonRepresentation(BsonType.String)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [NotMapped]
    public Guid UserMongoId { get; set; }
    
    [NotMapped]
    [BsonIgnore]
    public string Name { get; set; }
    

    public virtual User User { get; set; }

    [NotMapped]
    public Guid PersonalInformationMongoId { get; set; }
    
    public virtual PersonalInformation PersonalInformation { get; set; }
    
    [NotMapped]
    public ICollection<Guid> ExperienceMongoIds { get; set; }
    
    public virtual ICollection<Experience> Experiences { get; set; }
    
    [NotMapped]
    public ICollection<Guid> PassionMongoIds { get; set; }
    
    public virtual ICollection<Passion> Passions { get; set; }
    
    [NotMapped]
    public List<Guid> SkillMongoIds { get; set; }
    
    [BsonIgnore]
    public virtual ICollection<Skill> Skills { get; set; }
    
    [NotMapped]
    public ICollection<Guid> LanguageMongoIds { get; set; }
    
    public virtual ICollection<Language> Languages { get; set; }
    
    [NotMapped]
    public ICollection<Guid> EducationMongoIds { get; set; }
    
    public virtual ICollection<Education> Educations { get; set; }
}