using ToDoList.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.ToDoTaskLists.Dtos;

public class UpdateToDoTaskListDto
{
    [Required(ErrorMessage = "List ID is required.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "List name is required.")]
    public string Name { get; set; }

    public UpdateToDoTaskListDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

