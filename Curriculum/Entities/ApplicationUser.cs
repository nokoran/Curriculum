using Microsoft.AspNetCore.Identity;

namespace Curriculum.Entities;

public class ApplicationUser : IdentityUser
{
    public Guid? teacher_id { get; set; }
    public Guid? CourseId { get; set; }
    public Guid? GroupId { get; set; }
}