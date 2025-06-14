using ToDoList.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.ToDoTaskLists.Dtos
{
    public class CreateToDoTaskListDto
    {
        [Required(ErrorMessage = "List name is required.")]
        public string Name { get; set; }

        public CreateToDoTaskListDto(string name)
        {
            Name = name;
        }
    }
}
