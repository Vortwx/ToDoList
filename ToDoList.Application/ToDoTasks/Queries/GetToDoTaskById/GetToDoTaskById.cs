using MediatR;
using ToDoList.Application.ToDoTasks.Dtos;

namespace ToDoList.Application.ToDoTasks.Queries.GetToDoTaskById
{
    public class GetToDoTaskById: IRequest<ToDoTaskDto>
    {
        public Guid Id { get; }
        public Guid ParentListId { get; }

        public GetToDoTaskById(Guid id, Guid parentListId)
        {
            Id = id;
            ParentListId = parentListId;
        }
    }
}