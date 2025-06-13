// ToDoTaskList is name
// Update is crud

using MediatR;
using AutoMapper;
using ToDoList.Application.Interfaces;
using ToDoList.Application.ToDoTaskLists.Dtos;
using ToDoList.Domain.Entities;


namespace ToDoList.Application.ToDoTaskLists.Commands.UpdateToDoTaskList;

public class UpdateToDoTaskListHandler : IRequestHandler<UpdateToDoTaskList, ToDoTaskListDto>
{
    private readonly IToDoTaskListRepository _toDoTaskListRepository;
    private readonly IMapper _mapper;

    public UpdateToDoTaskListHandler(IToDoTaskListRepository toDoTaskListRepository, IMapper mapper)
    {
        _toDoTaskListRepository = toDoTaskListRepository;
        _mapper = mapper;
    }

    // An Upsert pattern of task is implemented here
    // Make sure the tasks is synchronous to the payload and its representative task list
    public async Task<ToDoTaskListDto> Handle(UpdateToDoTaskList request, CancellationToken cancellationToken)
    {
        var taskList = await _toDoTaskListRepository.GetTaskListByIdAsync(request.UpdateToDoTaskListDto.Id);
        if (taskList == null)
        {
            throw new KeyNotFoundException($"ToDoTaskList with Id {request.Id} not found.");
        }
        taskList.Name = request.UpdateToDoTaskListDto.Name;
        // do left outer join where left is the payload.tasks
        // before left outer join make sure that the non-matched right side is removed
        
        var taskIdsPayload = request.UpdateToDoTaskListDto.Tasks
            .Select(x => x.Id)
            .ToHashSet();

        var removedTasks = taskList.Tasks
            .Where(x => !taskIdsPayload.Contains(x.Id))
            .ToList();

        foreach (var removedTask in removedTasks)
        {
            taskList.Tasks.RemoveTask(removedTask);
        }

        foreach (var payloadTask in request.UpdateToDoTaskListDto.Tasks)
        {
            var task = taskList.Tasks.FirstOrDefault(x => x.Id == payloadTask.Id);
            if (task == null)
            {
                task = new ToDoTask(payloadTask.Notes, payloadTask.Title, payloadTask.DueDateTime, payloadTask.IsDone);
                taskList.AddTask(task);
            }
            else
            {
                task.Notes = payloadTask.Notes;
                task.Title = payloadTask.Title;
                task.DueDateTime = payloadTask.DueDateTime;
                task.IsDone = payloadTask.IsDone;
            }
        }
        
        await _toDoTaskListRepository.UpdateTaskListAsync(taskList); // will save inside this operation
        return _mapper.Map<ToDoTaskListDto>(taskList);
    }
}