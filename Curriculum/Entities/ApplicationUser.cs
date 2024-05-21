using Microsoft.AspNetCore.Identity;

namespace Curriculum.Enitities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public Guid CourseId { get; set; }
    public Guid GroupId { get; set; }
}