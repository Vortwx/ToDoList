// ToDoTask is name
// Update is crud

using MediatR;
using ToDoList.Application.ToDoTasks.Dtos;

namespace ToDoList.Application.ToDoTasks.Commands.UpdateToDoTask;

// UpdateToDoTaskDto only serves as Input DTO (represent input payload)
public class UpdateToDoTask : IRequest<Unit>
{
    public UpdateToDoTaskDto UpdateToDoTaskDto { get; }
    public UpdateToDoTask(UpdateToDoTaskDto updateToDoTaskDto)
    {
        UpdateToDoTaskDto = updateToDoTaskDto;
    }
}