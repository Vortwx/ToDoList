// ToDoTaskList is name
// Update is crud

using MediatR;
using ToDoList.Application.ToDoTaskLists.Dtos;

namespace ToDoList.Application.ToDoTaskLists.Commands.UpdateToDoTaskList;

// UpdateToDoTaskListDto only serves as Input DTO (represent input payload)
public class UpdateToDoTaskList : IRequest<ToDoTaskListDto>
{
    public UpdateToDoTaskListDto UpdateToDoTaskListDto { get; }
    public UpdateToDoTaskList(UpdateToDoTaskListDto UpdateToDoTaskListDto)
    {
        UpdateToDoTaskListDto = UpdateToDoTaskListDto;
    }
}