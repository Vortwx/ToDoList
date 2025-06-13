// ToDoTask is name
// Get is crud

using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTasks.Dtos;
using ToDoList.Domain.Entities;


namespace ToDoList.Application.ToDoTasks.Commands.GetToDoTaskByDueDateTime;

public class GetToDoTaskByDueDateTimeHandler : IRequestHandler<GetToDoTaskByDueDateTime, ToDoTaskDto>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public GetToDoTaskByDueDateTimeHandler(IToDoTaskRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<List<ToDoTaskDto>> Handle(GetToDoTaskByDueDateTime request, CancellationToken cancellationToken)
    {
        var tasks = await _toDoTaskListRepository.GetTasksByDueDateTimeAsync(request.DueDateTime);
        return _mapper.Map<List<ToDoTaskDto>>(tasks);
    }
}