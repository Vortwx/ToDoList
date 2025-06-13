using ToDoList.Domain.Entities;

namespace ToDoList.Application.ToDoTaskLists.Dtos;

public class UpdateToDoTaskListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ToDoTask> Tasks { get; set; }

    public UpdateToDoTaskListDto(Guid id, string name, List<ToDoTask> tasks)
    {
        Id = id;
        Name = name;
        Tasks = tasks;
    }
}

