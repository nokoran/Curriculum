namespace Curriculum.Entities;

public class Change : IBaseEntity
{
    public Guid id { get; set; }
    public Guid schedule_id { get; set; }
    public Guid modified_by_user_id { get; set; }
    public DateTime modification_date { get; set; }
    public string desc { get; set; }
    
}
