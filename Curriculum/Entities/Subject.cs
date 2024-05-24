namespace Curriculum.Entities;

public class Subject : IBaseEntity
{
    public Guid id { get; set; }
    public string subject_name { get; set; }
    public Guid course_id { get; set; }
}