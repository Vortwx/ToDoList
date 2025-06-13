using MediatR;

namespace ToDoList.Application.ToDoTaskLists.Queries.GetAllToDoTaskLists;

public class GetAllToDoTaskLists: IRequest<List<ToDoTaskListDto>>
{
    // No properties needed here 
}