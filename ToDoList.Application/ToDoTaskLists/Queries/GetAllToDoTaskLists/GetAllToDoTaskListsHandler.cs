using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTaskLists.Dtos; 

namespace ToDoList.Application.ToDoTaskLists.Queries.GetAllToDoTaskLists;

public class GetAllToDoTaskListsHandler : IRequestHandler<GetAllToDoTaskLists, List<ToDoTaskListDto>>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public GetAllToDoTaskListsHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<List<ToDoTaskListDto>> Handle(GetAllToDoTaskLists request, CancellationToken cancellationToken)
    {
        var taskLists = await _toDoTaskListRepository.GetAllTaskListAsync();
        return _mapper.Map<List<ToDoTaskListDto>>(taskLists);
    }
}