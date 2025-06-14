using System.Linq;

namespace ToDoList.Domain.Entities;
public class ToDoTaskList
{
    public Guid Id { get; private set; } // Set to Guid.Empty by default

    public ToDoTaskList(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Task list name cannot be empty.", nameof(name));
        }
        Name = name;
    }

    private ToDoTaskList() {} // EF core constructor will initialise Id

    public string Name { get; set; }
    public List<ToDoTask> Tasks { get; private set; } = new List<ToDoTask>(); // private stter for collection to control updates

    public void AddTask(ToDoTask task)
    {
        Tasks.Add(task);
    }

    public void RemoveTask(Guid taskId)
    {
        var taskToRemove = GetTask(taskId);

        if (taskToRemove == null)
        {
            throw new KeyNotFoundException($"Task with Id '{taskId}' not found in this task list.");
        }

        Tasks.Remove(taskToRemove);
    }

    public ToDoTask? GetTask(Guid taskId)
    {
        return Tasks.FirstOrDefault(t => t.Id == taskId);
    }

    public void UpdateTask(Guid id, string? notes, string? title, DateTime? dueDateTime, bool? isDone)
    {
        var taskToUpdate = GetTask(id);
        taskToUpdate.update(notes, title, dueDateTime, isDone);
    }
}