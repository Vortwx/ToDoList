// ToDoTask is name
// Delete is crud

using MediatR;
using ToDoList.Application.ToDoTasks.Dtos;

namespace ToDoList.Application.ToDoTasks.Commands.DeleteToDoTask;

// DeleteToDoTaskDto only serves as Input DTO (represent input payload)
public class DeleteToDoTask : IRequest<ToDoTaskDto>
{
    public DeleteToDoTaskDto DeleteToDoTaskDto { get; }
    public DeleteToDoTask(DeleteToDoTaskDto DeleteToDoTaskDto)
    {
        DeleteToDoTaskDto = DeleteToDoTaskDto;
    }
}