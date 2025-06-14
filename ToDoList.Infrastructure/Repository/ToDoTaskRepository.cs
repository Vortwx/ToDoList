using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Repository;

// Not used in current implementation
public class ToDoTaskRepository : IToDoTaskRepository
{
    private readonly ApplicationDbContext _context;

    public ToDoTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ToDoTask?> GetTaskByIdAsync(Guid id)
    {
        return await _context.ToDoTaskCollection.FindAsync(id);
    }

    public async Task CreateTaskAsync(ToDoTask task)
    {
        _context.ToDoTaskCollection.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(ToDoTask task)
    {
        _context.ToDoTaskCollection.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(ToDoTask task)
    {
        _context.ToDoTaskCollection.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ToDoTask>> GetTasksByDueDateTimeAsync(DateTime dueDateTime)
    {
        return await _context.ToDoTaskCollection
            .Where(t => t.DueDateTime.HasValue && t.DueDateTime.Value.Date == dueDateTime.Date)
            .ToListAsync();
    }
    

}