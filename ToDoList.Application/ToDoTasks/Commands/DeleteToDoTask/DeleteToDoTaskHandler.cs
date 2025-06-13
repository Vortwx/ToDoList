// ToDoTask is name
// Delete is crud

using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTasks.Dtos;
using ToDoList.Domain.Entities;


namespace ToDoList.Application.ToDoTasks.Commands.DeleteToDoTask;

public class DeleteToDoTaskHandler : IRequestHandler<DeleteToDoTask, ToDoTaskDto>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public DeleteToDoTaskHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<ToDoTaskDto> Handle(DeleteToDoTask request, CancellationToken cancellationToken)
    {
        var parentList = await _toDoTaskListRepository.GetTaskListByIdAsync(request.ToDoTaskDto.ParentListId);
        parentList.RemoveTask(request.ToDoTaskDto.Id);
        var task = await _toDoTaskListRepository.UpdateTaskListAsync(parentList);

        return _mapper.Map<ToDoTaskDto>(task);
    }
}