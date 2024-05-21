namespace Curriculum.Enitities;

public class Teachers : IBaseEntity
{
    public Guid id { get; set; }
    public string full_name { get; set; }
    public string desc { get; set; }
}