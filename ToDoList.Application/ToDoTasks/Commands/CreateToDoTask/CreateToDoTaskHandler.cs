using MediatR;
using AutoMapper;
using ToDoList.Application.ToDoTasks.Dtos;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.ToDoTasks.Commands.CreateToDoTask;

public class CreateToDoTaskHandler : IRequestHandler<CreateToDoTask, ToDoTaskDto>
{
    private readonly IToDoTaskRepository _toDoTaskRepository;
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public CreateToDoTaskHandler(IToDoTaskRepository toDoTaskRepository, IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskRepository = toDoTaskRepository;
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<ToDoTaskDto> Handle(CreateToDoTask request, CancellationToken cancellationToken)
    {
        var task = new ToDoTask(request.Title, request.Notes, request.DueDateTime, request.IsDone);
        
        try
        {
            var parentList = await _toDoTaskListRepository.GetTaskListByIdAsync(request.ParentListId);
        }
        catch (Exception ex)
        {
            throw new KeyNotFoundException("Parent list not found", ex);
        }
        
        await _toDoTaskRepository.CreateTaskAsync(task);
        parentList.AddTask(task); // Entities-specific method
        await _toDoTaskListRepository.UpdateTaskListAsync(parentList);
        return _mapper.Map<ToDoTaskDto>(task); // Output DTO always use ToDoTaskDto
    }
}