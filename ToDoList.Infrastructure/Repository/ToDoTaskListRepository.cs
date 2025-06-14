using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Repository;

public class ToDoTaskListRepository : IToDoTaskListRepository
{
    private readonly ApplicationDbContext _context;
    public ToDoTaskListRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ToDoTaskList?> GetTaskListByIdAsync(Guid id)
    {
        return await _context.ToDoTaskListCollection
                         .Include(tl => tl.Tasks)
                         .FirstOrDefaultAsync(tl => tl.Id == id);
    }

    public async Task CreateTaskListAsync(ToDoTaskList taskList)
    {
        _context.ToDoTaskListCollection.Add(taskList);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskListAsync(ToDoTaskList taskList)
    {
        _context.ToDoTaskListCollection.Update(taskList);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskListAsync(ToDoTaskList taskList)
    {
        _context.ToDoTaskListCollection.Remove(taskList);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ToDoTaskList>> GetAllTaskListAsync()
    {
        return await _context.ToDoTaskListCollection
                .Include(tl => tl.Tasks)
                .ToListAsync(); 
    }

    // ToDoTask is aggregated into ToDoTaskList hence this function will be migrated here
    public async Task<IEnumerable<ToDoTask>> GetTasksByDueDateTimeAsync(DateTime dueDateTime)
    {
        return await _context.ToDoTaskCollection
            .Where(t => t.DueDateTime.HasValue && t.DueDateTime.Value.Date == dueDateTime.Date)
            .ToListAsync();
    }
}