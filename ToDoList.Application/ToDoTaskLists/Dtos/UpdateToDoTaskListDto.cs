using ToDoList.Domain.Entities;

namespace ToDoList.Application.ToDoTaskLists.Dtos;

public class UpdateToDoTaskListDto
{
    public string Name { get; set; }
    public List<ToDoTaskList> Tasks { get; set; }

    public UpdateToDoTaskListDto(string name, List<ToDoTaskList> tasks)
    {
        Name = name;
        Tasks = tasks;
    }
}

