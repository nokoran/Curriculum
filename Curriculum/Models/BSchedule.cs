namespace Curriculum.Models;

public class BSchedule
{
    public Guid id { get; set; }
    public string subject_name { get; set; }
    public string course_name { get; set; }
    public string group_name { get; set; }
    public string place { get; set; }
    public string teacher_name { get; set; }
    public DateTime start_time { get; set; }
    public DateTime end_time { get; set; }
}