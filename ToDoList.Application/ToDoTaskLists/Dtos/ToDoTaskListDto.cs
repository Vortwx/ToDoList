using ToDoList.Domain.Entities;

namespace ToDoList.Application.ToDoTaskLists.Dtos;

public class ToDoTaskListDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<ToDoTask> Tasks { get; set; }

    public ToDoTaskListDto(Guid id, string name, List<ToDoTask> tasks)
    {
        Id = id;
        Name = name;
        Tasks = tasks;
    }
}