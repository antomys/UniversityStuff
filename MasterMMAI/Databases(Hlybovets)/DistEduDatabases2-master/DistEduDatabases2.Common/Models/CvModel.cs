namespace DistEduDatabases2.Common.Models;

public sealed class CvModel : BaseClass
{
    public Guid Id { get; set; }
    
    public UserModel User { get; set; }
    
    public PersonalInformationModel PersonalInformation { get; set; }
    
    public ICollection<ExperienceModel> Experiences { get; set; }
    
    public ICollection<PassionModel> Passions { get; set; }
    
    public ICollection<SkillModel> Skills { get; set; }
    
    public ICollection<LanguageModel> Languages { get; set; }
    
    public ICollection<EducationModel> Educations { get; set; }
}