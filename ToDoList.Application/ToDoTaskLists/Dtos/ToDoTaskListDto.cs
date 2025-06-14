using ToDoList.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.ToDoTaskLists.Dtos;

public class ToDoTaskListDto
{
    [Required(ErrorMessage = "List ID is required.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "List name is required.")]
    public string Name { get; set; }

    public List<ToDoTask> Tasks { get; set; }

    public ToDoTaskListDto(Guid id, string name, List<ToDoTask> tasks)
    {
        Id = id;
        Name = name;
        Tasks = tasks;
    }
}