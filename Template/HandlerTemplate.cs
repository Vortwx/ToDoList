// @ is name
// ~ is crud

using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.@s.Dtos;
using ToDoList.Domain.Entities;


namespace ToDoList.Application.@s.Commands.~@;

public class ~@Handler : IRequestHandler<~@, @Dto>
{
    private readonly IToDoTaskRepository _toDoTaskRepository;
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public ~@Handler(I@Repository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<@Dto> Handle(~@ request, CancellationToken cancellationToken)
    {
        
    }
}