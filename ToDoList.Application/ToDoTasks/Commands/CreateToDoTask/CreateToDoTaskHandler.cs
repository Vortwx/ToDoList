using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTasks.Dtos;
using ToDoList.Domain.Entities;


namespace ToDoList.Application.ToDoTasks.Commands.CreateToDoTask;

public class CreateToDoTaskHandler : IRequestHandler<CreateToDoTask, ToDoTaskDto>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public CreateToDoTaskHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<ToDoTaskDto> Handle(CreateToDoTask request, CancellationToken cancellationToken)
    {
        var task = new ToDoTask(
            request.CreateToDoTaskDto.Notes,
            request.CreateToDoTaskDto.Title,
            request.CreateToDoTaskDto.DueDateTime,
            request.CreateToDoTaskDto.IsDone
        );

        try
        {
            var parentList = await _toDoTaskListRepository.GetTaskListByIdAsync(request.CreateToDoTaskDto.ParentListId);
            parentList.AddTask(task); // Entities-specific method
            await _toDoTaskListRepository.UpdateTaskListAsync(parentList);
        }
        catch (Exception ex)
        {
            throw new KeyNotFoundException($"Task not found within ToDoTaskList '{request.CreateToDoTaskDto.ParentListId}'. {ex.Message}", ex);
        }
        
        return _mapper.Map<ToDoTaskDto>(task); // Output DTO always use ToDoTaskDto
    }
}

