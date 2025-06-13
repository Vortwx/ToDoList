using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces
{
    public interface IToDoTaskListRepository
    {
        Task<ToDoTaskList?> GetTaskListByIdAsync(Guid id);
        Task<IEnumerable<ToDoTaskList>> GetAllTaskListAsync();
        Task CreateTaskListAsync(ToDoTaskList taskList);
        Task UpdateTaskListAsync(ToDoTaskList taskList);
        Task DeleteTaskListAsync(ToDoTaskList taskList);
        Task<IEnumerable<ToDoTask>> GetTasksByDueDateTimeAsync(DateTime dueDateTime);


    }
}