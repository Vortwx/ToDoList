using ToDoList.Application.ToDoTasks.Dtos;

namespace ToDoList.Application.ToDoTasks.Commands.CreateToDoTask;

// CreateToDoTaskDto only serves as Input DTO (represent input payload)
public class CreateToDoTask(CreateToDoTaskDto createToDoTaskDto) : IRequest<ToDoTaskDto>;



