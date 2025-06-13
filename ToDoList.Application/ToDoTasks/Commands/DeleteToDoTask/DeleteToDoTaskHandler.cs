// ToDoTask is name
// Delete is crud

using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;


namespace ToDoList.Application.ToDoTasks.Commands.DeleteToDoTask;

public class DeleteToDoTaskHandler : IRequestHandler<DeleteToDoTask, Unit>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public DeleteToDoTaskHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteToDoTask request, CancellationToken cancellationToken)
    {
        var parentList = await _toDoTaskListRepository.GetTaskListByIdAsync(request.DeleteToDoTaskDto.ParentListId);
        parentList.RemoveTask(request.DeleteToDoTaskDto.Id);
        await _toDoTaskListRepository.UpdateTaskListAsync(parentList);

        return Unit.Value;
    }
}