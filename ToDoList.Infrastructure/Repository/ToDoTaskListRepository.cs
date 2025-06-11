using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Repository;

public class ToDoTaskListRepository : IToDoTaskListRepository
{
    private readonly ApplicationDbContext _context;
    public ToDoTaskListRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ToDoTaskList> GetTaskListByIdAsync(Guid id)
    {
        return await _context.ToDoTaskLists.FindAsync(id);
    }

    public async Task CreateTaskListAsync(ToDoTaskList taskList)
    {
        _context.ToDoTaskLists.Add(taskList);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskListAsync(ToDoTaskList taskList)
    {
        _context.ToDoTaskLists.Update(taskList);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskListAsync(ToDoTaskList taskList)
    {
        _context.ToDoTaskLists.Remove(taskList);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ToDoTaskList>> GetAllTaskListsAsync()
    {
        return await _context.ToDoTaskLists.ToListAsync();
    }
}