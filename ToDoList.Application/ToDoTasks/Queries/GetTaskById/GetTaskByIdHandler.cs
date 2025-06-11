using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTasks.Dtos; 

namespace ToDoList.Application.ToDoTasks.Queries.GetTaskById;

public class GetTaskByIdHandler : IRequestHandler<GetTaskById, ToDoTaskDto>
{
    private readonly IToDoTaskRepository _toDoTaskRepository;
    private readonly IMapper _mapper;

    public GetTaskByIdHandler(IToDoTaskRepository toDoTaskRepository, IMapper mapper)
    {
        _toDoTaskRepository = toDoTaskRepository;
        _mapper = mapper;
    }

    public async Task<ToDoTaskDto> Handle(GetTaskById request, CancellationToken cancellationToken)
    {
        var task = await _toDoTaskRepository.GetTaskByIdAsync(request.Id);
        return _mapper.Map<ToDoTaskDto>(task); // Output DTO always use ToDoTaskDto
    }
}