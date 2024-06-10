namespace Curriculum.Entities;

public class Room : IBaseEntity
{
    public Guid id { get; set; }
    public string name { get; set; }
}