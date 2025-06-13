using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTasks.Dtos; 

namespace ToDoList.Application.ToDoTasks.Queries.GetToDoTaskById;

public class GetToDoTaskByIdHandler : IRequestHandler<GetToDoTaskById, ToDoTaskDto>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public GetToDoTaskByIdHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<ToDoTaskDto> Handle(GetToDoTaskById request, CancellationToken cancellationToken)
    {
        var parentList = await _toDoTaskListRepository.GetTaskListByIdAsync(request.ParentListId);
        if (parentList == null)
        {
            throw new KeyNotFoundException($"ToDoTaskList with Id '{request.ParentListId}' not found.");
        }

        var task = parentList.GetTask(request.Id);

        if (task == null)
        {
            throw new KeyNotFoundException($"ToDoTask with Id '{request.Id}' not found within ToDoTaskList '{request.ParentListId}'.");
        }

        return _mapper.Map<ToDoTaskDto>(task); // Output DTO always use ToDoTaskDto
    }
}