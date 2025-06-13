namespace ToDoList.Application.ToDoTasks.Dtos;

public class UpdateToDoTaskDto
{
    public string Title { get; set; }
    public string Notes { get; set; }
    public DateTime DueDateTime { get; set; }
    public bool IsDone { get; set; }
    public Guid ParentListId { get; set; } // FK in external identifier to make sure ToDoTask is dependent on a ToDoTaskList.

    public UpdateToDoTaskDto(string title, string notes, DateTime dueDateTime, bool isDone, Guid parentListId)
    {
        Title = title;
        Notes = notes;
        DueDateTime = dueDateTime;
        IsDone = isDone;
        ParentListId = parentListId;
    }
}