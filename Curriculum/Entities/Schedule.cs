namespace Curriculum.Entities;

public class Schedule : IBaseEntity
{
    public Guid id { get; set; }
    public Guid course_id { get; set; }
    public Guid group_id { get; set; }
    public string place { get; set; }
    public Guid teacher_id { get; set; }
    public DateTime start_time { get; set; }
    public DateTime end_time { get; set; }
}