using MediatR;
using ToDoList.Application.ToDoTaskLists.Dtos;

namespace ToDoList.Application.ToDoTaskLists.Queries.GetToDoTaskListById
{
    public class GetToDoTaskListById: IRequest<ToDoTaskListDto>
    {
        public Guid Id { get; }

        public GetToDoTaskListById(Guid id)
        {
            Id = id;
        }
    }
}