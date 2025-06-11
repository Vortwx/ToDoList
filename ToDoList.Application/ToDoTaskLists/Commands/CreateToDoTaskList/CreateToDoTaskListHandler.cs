using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTaskLists.Dtos;
using ToDoList.Domain.Entities;


namespace ToDoList.Application.ToDoTaskLists.Commands.CreateToDoTaskList;

public class CreateToDoTaskListHandler : IRequestHandler<CreateToDoTaskList, ToDoTaskListDto>
{
    private readonly IToDoTaskRepository _toDoTaskRepository;
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public CreateToDoTaskListHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    public async Task<ToDoTaskListDto> Handle(CreateToDoTaskList request, CancellationToken cancellationToken)
    {
        var taskList = new ToDoTaskList(
            request.CreateToDoTaskListDto.Name
        );

        await _toDoTaskListRepository.CreateTaskListAsync(taskList);

        return _mapper.Map<ToDoTaskListDto>(taskList); // Output DTO always use ToDoTaskDto
    }
}