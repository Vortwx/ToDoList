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

    public void RemoveTask(ToDoTask task)
    {
        if (task == null) return;
        Tasks.Remove(task);
    }
}