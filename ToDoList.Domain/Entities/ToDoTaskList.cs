using System.Linq;

namespace ToDoList.Domain.Entities;
public class ToDoTaskList
{
    public Guid Id { get; private set; }

    public ToDoTaskList(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Tasks = new List<ToDoTask>(); // empty when created
    }

    private ToDoTaskList() // EF Core requires a parameterless constructor
    {
        Id = Guid.NewGuid();
    }

    public string Name { get; set; }
    public List<ToDoTask> Tasks { get; private set; } // private stter for collection to control updates

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

    public ToDoTask GetTask(Guid taskId)
    {
        return Tasks.FirstOrDefault(t => t.Id == taskId);
    }

    public void UpdateTask(ToDoTask task)
    {
        var taskToUpdate = GetTask(task.Id);
        taskToUpdate.Notes = task.Notes;
        taskToUpdate.Title = task.Title;
        taskToUpdate.DueDateTime = task.DueDateTime;
        taskToUpdate.IsDone = task.IsDone;
    }
}