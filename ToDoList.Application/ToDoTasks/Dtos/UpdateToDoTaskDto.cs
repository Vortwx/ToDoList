namespace ToDoList.Application.ToDoTasks.Dtos;

public class UpdateToDoTaskDto
{
    public string Title { get; set; }
    public string Notes { get; set; }
    public DateTime DueDateTime { get; set; }
    public bool IsDone { get; set; }

    public UpdateToDoTaskDto(string title, string notes, DateTime dueDateTime, bool isDone)
    {
        Title = title;
        Notes = notes;
        DueDateTime = dueDateTime;
        IsDone = isDone;
    }
}