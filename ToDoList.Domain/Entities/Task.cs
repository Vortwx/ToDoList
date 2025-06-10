public class Task
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Notes { get; set; }
    public DateTime DueDateTime { get; set; }
    public bool IsDone { get; set; }
}