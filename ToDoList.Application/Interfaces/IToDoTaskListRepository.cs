namespace ToDoList.Application.Interfaces
{
    public interface IToDoTaskRepository : IGenericRepository<ToDoTask>
    {
        Task<ToDoTaskList?> GetTaskListByIdAsync(Guid id);
        Task<IEnumerable<ToDoTaskList>> GetAllTaskListAsync();
        Task AddTaskListAsync(ToDoTaskList taskList);
        Task UpdateTaskListAsync(ToDoTaskList taskList);
        Task DeleteTaskListByIdAsync(Guid id);

    }
}