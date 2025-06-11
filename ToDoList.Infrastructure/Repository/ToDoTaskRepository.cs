using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Repository;

public class ToDoTaskRepository : IToDoTaskRepository
{
    private readonly ApplicationDbContext _context;

    public ToDoTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ToDoTask> GetTaskByIdAsync(Guid id)
    {
        return await _context.ToDoTasks.FindAsync(id);
    }

    public async Task CreateTaskAsync(ToDoTask task)
    {
        _context.ToDoTasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(ToDoTask task)
    {
        _context.ToDoTasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(ToDoTask task)
    {
        _context.ToDoTasks.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ToDoTask>> GetTasksByDueDateTimeAsync(DateTime dueDateTime)
    {
        return await _context.ToDoTasks
            .Where(t => t.DueDateTime.Date == dueDateTime.Date)
            .ToListAsync();
    }
    

}