namespace Curriculum.Entities;

public class Course : IBaseEntity
{
    public Guid id { get; set; }
    public string course_name { get; set; }
    public string desc { get; set; }
    public double credit_hours { get; set; }
}