namespace Curriculum.Entities;

public class Teacher : IBaseEntity
{
    public Guid id { get; set; }
    public string full_name { get; set; }
    public string desc { get; set; }
}