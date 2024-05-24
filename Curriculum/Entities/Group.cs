namespace Curriculum.Entities;

public class Group : IBaseEntity
{
    public Guid id { get; set; }
    public string group_name { get; set; }
    public Guid course_id { get; set; }
}