public class TaskList
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();
}