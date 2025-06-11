using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.ToDoTasks.Dtos;
using ToDoList.Application.ToDoTasks.Commands.CreateToDoTask;
using System.Net;
using MediatR;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoTaskController: ControllerBase
{
    private readonly IMediator _mediator;
    public ToDoTaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpGet("{id}")] 
    //[ProducesResponseType(typeof(ToDoTaskDto), (int)HttpStatusCode.OK)]
    //public async Task<ActionResult<ToDoTaskDto>> GetTaskById(Guid id)
    //{
    //    return await _mediator.Send(new GetTaskById(id));
    //}

    //[HttpGet] 
    //[ProducesResponseType(typeof(IEnumerable<ToDoTaskDto>), (int)HttpStatusCode.OK)]
    //public async Task<ActionResult<IEnumerable<ToDoTaskDto>>> GetTasksByDueDateTime(DateTime dueDateTime)
    //{
    //    return await _mediator.Send(new GetTasksByDueDateTime(dueDateTime));
    //}

    [HttpPost] 
    [ProducesResponseType(typeof(ToDoTaskDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ToDoTaskDto>> CreateToDoTask(CreateToDoTaskDto createToDoTaskDto)
    {
        return await _mediator.Send(new CreateToDoTask(createToDoTaskDto));
    }

    //[HttpPut] 
    //[ProducesResponseType(typeof(ToDoTaskDto), (int)HttpStatusCode.OK)]
    //public async Task<ActionResult<ToDoTaskDto>> UpdateToDoTask(UpdateToDoTaskDto updateToDoTaskDto)
    //{
    //    return await _mediator.Send(new UpdateToDoTask(updateToDoTaskDto));
    //}

    //[HttpDelete("{id}")] 
    //[ProducesResponseType(typeof(ToDoTaskDto), (int)HttpStatusCode.OK)]
    //public async Task<ActionResult<ToDoTaskDto>> DeleteToDoTask(Guid id)
    //{
    //    return await _mediator.Send(new DeleteToDoTask(id));
    //}
}