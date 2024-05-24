namespace Curriculum.Models;

public class BCreateSchedule
{
    public Guid id { get; set; }
    public Guid subject_id { get; set; }
    public Guid teacher_id { get; set; }
    public Guid course_id { get; set; }
    public Guid group_id { get; set; }
    public string place { get; set; }
    public DateTime start_time { get; set; }
    public DateTime end_time { get; set; }
    public DateTime start_date { get; set; } // Added
    public DateTime end_date { get; set; } // Added
    public bool is_alternating { get; set; } // Added
}