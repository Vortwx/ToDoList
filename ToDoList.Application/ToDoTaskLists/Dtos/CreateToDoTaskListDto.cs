using ToDoList.Domain.Entities;

namespace ToDoList.Application.ToDoTaskLists.Dtos
{
    public class CreateToDoTaskListDto
    {
        public string Name { get; set; }

        public CreateToDoTaskListDto(string name)
        {
            Name = name;
        }
    }
}
