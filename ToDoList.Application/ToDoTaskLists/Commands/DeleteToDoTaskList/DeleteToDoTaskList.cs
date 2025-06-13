// ToDoTaskList is name
// Delete is crud

using MediatR;
using ToDoList.Application.ToDoTaskLists.Dtos;

namespace ToDoList.Application.ToDoTaskLists.Commands.DeleteToDoTaskList;

// DeleteToDoTaskListDto only serves as Input DTO (represent input payload)
public class DeleteToDoTaskList : IRequest<ToDoTaskListDto>
{
    public ToDoTaskListDto ToDoTaskListDto { get; }
    public DeleteToDoTaskList(ToDoTaskListDto ToDoTaskListDto)
    {
        ToDoTaskListDto = ToDoTaskListDto;
    }
}