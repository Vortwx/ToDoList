using MediatR;
using ToDoList.Application.ToDoTaskLists.Dtos;

namespace ToDoList.Application.ToDoTaskLists.Commands.CreateToDoTaskList;

// CreateToDoTaskListDto only serves as Input DTO (represent input payload)
public class CreateToDoTaskList : IRequest<ToDoTaskListDto>
{
    public CreateToDoTaskListDto CreateToDoTaskListDto { get; }
    public CreateToDoTaskList(CreateToDoTaskListDto createToDoTaskListDto)
    {
        CreateToDoTaskListDto = createToDoTaskListDto;
    }
}