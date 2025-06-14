using MediatR;
using ToDoList.Application.ToDoTasks.Dtos;

namespace ToDoList.Application.ToDoTasks.Queries.GetToDoTaskByDueDateTime
{
    public class GetToDoTaskByDueDateTime: IRequest<List<ToDoTaskDto>>
    {
        public DateTime DueDateTime { get; set; }

        public GetToDoTaskByDueDateTime(DateTime dueDateTime)
        {
            DueDateTime = dueDateTime;
        }
    }
}