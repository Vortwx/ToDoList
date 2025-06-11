using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.ToDoTaskLists.Dtos;
using ToDoList.Application.ToDoTaskLists.Commands.CreateToDoTaskList;
using System.Net;
using MediatR;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoTaskListController: ControllerBase
{
    private readonly IMediator _mediator;
    public ToDoTaskListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost] 
    [ProducesResponseType(typeof(ToDoTaskListDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ToDoTaskListDto>> CreateToDoTaskList(CreateToDoTaskListDto createToDoTaskListDto)
    {
        return await _mediator.Send(new CreateToDoTaskList(createToDoTaskListDto));
    }
}