namespace ToDoList.Domain.Entities;
public class ToDoTask
{
    public Guid Id { get; private set; }

    public ToDoTask(string notes, string title, DateTime dueDateTime, bool isDone)
    {
        Id = Guid.NewGuid();
        Notes = notes;
        Title = title;
        DueDateTime = dueDateTime;
        IsDone = isDone;
    }

    private ToDoTask() // EF Core requires a parameterless constructor
    {
        Id = Guid.NewGuid();
    }

    public string Title { get; set; }
    public string Notes { get; set; }
    public DateTime DueDateTime { get; set; }
    public bool IsDone { get; set; }

    public void checkedIsDone()
    {
        IsDone = !IsDone;
    }
}