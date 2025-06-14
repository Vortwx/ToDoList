using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.ToDoTasks.Dtos;

public class ToDoTaskDto
{
    [Required(ErrorMessage = "Task id is required.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Task title is required.")]
    public string Title { get; set; }

    public string? Notes { get; set; }

    [Required(ErrorMessage = "Task due date is required.")]
    public DateTime DueDateTime { get; set; }

    [Required(ErrorMessage = "Task status is required.")]
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