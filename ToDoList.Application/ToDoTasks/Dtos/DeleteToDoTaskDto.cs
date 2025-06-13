namespace ToDoList.Application.ToDoTasks.Dtos;

// Normally deletion will reuse original Dto
// However under aggregation with ToDoTaskList, deletion requires a lookup to parentListId
public class DeleteToDoTaskDto
{
    public Guid Id { get; set; }
    public Guid ParentListId { get; set; } // FK in external identifier to make sure ToDoTask is dependent on a ToDoTaskList.

    public DeleteToDoTaskDto(Guid id, Guid parentListId)
    {
        Id = id;
        ParentListId = parentListId;
    }
}