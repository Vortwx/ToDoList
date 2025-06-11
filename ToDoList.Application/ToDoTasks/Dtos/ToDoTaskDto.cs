namespace ToDoList.Application.ToDoTasks.Dtos;

public class ToDoTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Notes { get; set; }
    public DateTime DueDateTime { get; set; }
    public bool IsDone { get; set; }

    public ToDoTaskDto(Guid id, string title, string notes, DateTime dueDateTime, bool isDone)
    {
        Id = id;
        Title = title;
        Notes = notes;
        DueDateTime = dueDateTime;
        IsDone = isDone;
    }
}