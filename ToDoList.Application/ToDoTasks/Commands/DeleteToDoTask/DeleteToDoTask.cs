// ToDoTask is name
// Delete is crud

using MediatR;
using ToDoList.Application.ToDoTasks.Dtos;

namespace ToDoList.Application.ToDoTasks.Commands.DeleteToDoTask;

// DeleteToDoTaskDto only serves as Input DTO (represent input payload)
public class DeleteToDoTask : IRequest<Unit>
{
    public DeleteToDoTaskDto DeleteToDoTaskDto { get; }
    public DeleteToDoTask(DeleteToDoTaskDto deleteToDoTaskDto)
    {
        DeleteToDoTaskDto = deleteToDoTaskDto;
    }
}