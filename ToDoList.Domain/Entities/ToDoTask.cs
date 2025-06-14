namespace ToDoList.Domain.Entities;
public class ToDoTask
{
    public Guid Id { get; private set; } 

    public ToDoTask(string notes, string title, DateTime dueDateTime, bool isDone)
    {
        Notes = notes;
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Task title cannot be empty for new tasks.", nameof(title));
        }
        Title = title;
        DueDateTime = dueDateTime;
        IsDone = isDone;
    }

    private ToDoTask() {} 

    public string? Title { get; set; }
    public string? Notes { get; set; }
    public DateTime? DueDateTime { get; set; }
    public bool? IsDone { get; set; }

    public void checkedIsDone()
    {
        IsDone = !IsDone;
    }

    public void update(string? notes, string? title, DateTime? dueDateTime, bool? isDone)
    {
        if (notes != null) Notes = notes;
        if (title != null) Title = title;
        if (dueDateTime.HasValue) DueDateTime = dueDateTime.Value;
        if (isDone.HasValue) IsDone = isDone.Value;
    }
}