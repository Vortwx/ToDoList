using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.ToDoTasks.Dtos;

// Normally deletion will reuse original Dto
// However under aggregation with ToDoTaskList, deletion requires a lookup to parentListId
public class DeleteToDoTaskDto
{
    [Required(ErrorMessage = "Task ID is required.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Parent List ID is required.")]
    public Guid ParentListId { get; set; } // FK in external identifier to make sure ToDoTask is dependent on a ToDoTaskList.

    public DeleteToDoTaskDto(Guid id, Guid parentListId)
    {
        Id = id;
        ParentListId = parentListId;
    }
}