using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.ToDoTasks.Dtos;

public class UpdateToDoTaskDto
{
    [Required(ErrorMessage = "Task ID is required.")]
    public Guid Id { get; } 

    public string? Title { get; set; }
    public string? Notes { get; set; }
    public DateTime? DueDateTime { get; set; }
    public bool? IsDone { get; set; }

    [Required(ErrorMessage = "Parent List ID is required.")]
    public Guid ParentListId { get; set; } // FK in external identifier to make sure ToDoTask is dependent on a ToDoTaskList.

    public UpdateToDoTaskDto(Guid id, string? title, string? notes, DateTime? dueDateTime, bool? isDone, Guid parentListId)
    {
        Id = id;
        Title = title;
        Notes = notes;
        DueDateTime = dueDateTime;
        IsDone = isDone;
        ParentListId = parentListId;
    }
}