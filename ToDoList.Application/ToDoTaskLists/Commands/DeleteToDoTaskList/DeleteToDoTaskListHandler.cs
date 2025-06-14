// ToDoTaskList is name
// Delete is crud

using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTaskLists.Dtos;
using ToDoList.Domain.Entities;


namespace ToDoList.Application.ToDoTaskLists.Commands.DeleteToDoTaskList;

public class DeleteToDoTaskListHandler : IRequestHandler<DeleteToDoTaskList, ToDoTaskListDto>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public DeleteToDoTaskListHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<ToDoTaskListDto> Handle(DeleteToDoTaskList request, CancellationToken cancellationToken)
    {
        var taskList = await _toDoTaskListRepository.GetTaskListByIdAsync(request.Id);
        if (taskList == null)
        {
            throw new KeyNotFoundException($"Task list with Id '{request.Id}' not found.");
        }
        await _toDoTaskListRepository.DeleteTaskListAsync(taskList);
        return _mapper.Map<ToDoTaskListDto>(taskList);
    }
}