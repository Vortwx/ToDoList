using MediatR;
using ToDoList.Application.ToDoTasks.Dtos;

namespace ToDoList.Application.ToDoTasks.Queries.GetTaskById
{
    public class GetTaskById: IRequest<ToDoTaskDto>
    {
        public Guid Id { get; }

        public GetTaskById(Guid id)
        {
            Id = id;
        }
    }
}