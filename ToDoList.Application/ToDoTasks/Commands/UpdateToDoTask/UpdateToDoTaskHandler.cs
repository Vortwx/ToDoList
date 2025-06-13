// ToDoTask is name
// Update is crud

using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTasks.Dtos;
using ToDoList.Domain.Entities;


namespace ToDoList.Application.ToDoTasks.Commands.UpdateToDoTask;

public class UpdateToDoTaskHandler : IRequestHandler<UpdateToDoTask, ToDoTaskDto>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public UpdateToDoTaskHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<ToDoTaskDto> Handle(UpdateToDoTask request, CancellationToken cancellationToken)
    {
        var parentList = await _toDoTaskListRepository.GetTaskListByIdAsync(request.UpdateToDoTaskDto.ParentListId);
        
        if (parentList == null)
        {
            throw new KeyNotFoundException($"ToDoTaskList with Id '{request.UpdateToDoTaskDto.ParentListId}' not found.");
        }

        try
        {
            parentList.UpdateTask(
                request.UpdateToDoTaskDto.Id,
                request.UpdateToDoTaskDto.Title,
                request.UpdateToDoTaskDto.Notes,
                request.UpdateToDoTaskDto.DueDateTime,
                request.UpdateToDoTaskDto.IsDone
            );
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException($"Task with Id '{taskId}' not found within ToDoTaskList '{parentListId}'.", ex);
        }

        var task = await _toDoTaskListRepository.UpdateTaskListAsync(parentList);
        return _mapper.Map<ToDoTaskDto>(task);
    }
}