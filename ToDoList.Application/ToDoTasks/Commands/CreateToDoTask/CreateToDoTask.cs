using MediatR;
using ToDoList.Application.ToDoTasks.Dtos;

namespace ToDoList.Application.ToDoTasks.Commands.CreateToDoTask;

// CreateToDoTaskDto only serves as Input DTO (represent input payload)
public class CreateToDoTask: IRequest<ToDoTaskDto>
{
    public CreateToDoTaskDto CreateToDoTaskDto { get; }
    public CreateToDoTask(CreateToDoTaskDto createToDoTaskDto)
    {
        CreateToDoTaskDto = createToDoTaskDto;
    }
}



