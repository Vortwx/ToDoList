using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.ToDoTasks.Dtos;

public class CreateToDoTaskDto
{
    [Required(ErrorMessage = "Task title is required.")]
    public string Title { get; set; }

    public string? Notes { get; set; }

    [Required(ErrorMessage = "Task due date is required.")]
    public DateTime DueDateTime { get; set; }

    [Required(ErrorMessage = "Task status is required.")]
    public bool IsDone { get; set; }

    [Required(ErrorMessage = "Parent List ID is required.")]
    public Guid ParentListId { get; set; } // FK in external identifier to make sure ToDoTask is dependent on a ToDoTaskList.

    public CreateToDoTaskDto(string title, string notes, DateTime dueDateTime, bool isDone, Guid parentListId)
    {
        Title = title;
        Notes = notes;
        DueDateTime = dueDateTime;
        IsDone = isDone;
        ParentListId = parentListId;
    }
}