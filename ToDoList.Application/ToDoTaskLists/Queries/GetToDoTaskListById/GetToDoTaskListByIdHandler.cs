using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTaskLists.Dtos; 

namespace ToDoList.Application.ToDoTaskLists.Queries.GetToDoTaskListById;

public class GetToDoTaskListByIdHandler : IRequestHandler<GetToDoTaskListById, ToDoTaskListDto>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public GetToDoTaskListByIdHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<ToDoTaskListDto> Handle(GetToDoTaskListById request, CancellationToken cancellationToken)
    {
        var task = await _toDoTaskListRepository.GetTaskListByIdAsync(request.Id);
        return _mapper.Map<ToDoTaskListDto>(task); // Output DTO always use ToDoTaskListDto
    }
}