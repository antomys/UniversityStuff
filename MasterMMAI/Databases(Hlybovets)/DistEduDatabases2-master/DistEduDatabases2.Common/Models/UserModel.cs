namespace DistEduDatabases2.Common.Models;

public sealed class UserModel : BaseClass
{
    public Guid Id { get; set; }
    
    public string Login { get; set; }
}