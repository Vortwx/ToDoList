using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDoList.Application.ToDoTasks.Commands.CreateToDoTask;


// Import your DTOs, Commands, and Queries
using ToDoList.Application.ToDoTasks.Commands.DeleteToDoTask;
using ToDoList.Application.ToDoTasks.Commands.UpdateToDoTask;
using ToDoList.Application.ToDoTasks.Dtos;
using ToDoList.Application.ToDoTasks.Queries.GetToDoTaskByDueDateTime;
using ToDoList.Application.ToDoTasks.Queries.GetToDoTaskById; // For ToDoTaskDto, CreateToDoTaskDto, UpdateToDoTaskDto, DeleteToDoTaskDto

namespace ToDoList.Api.Controllers;

/// <summary>
/// Controller for managing individual ToDo Tasks.
/// Tasks are always associated with a ToDo Task List (aggregate root).
/// </summary>
[ApiController]
[Route("api/tasks")] // Flat route, ParentListId will be in body/query
[Produces("application/json")]
public class ToDoTaskController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ToDoTaskController"/> class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator for sending commands and queries.</param>
    public ToDoTaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new ToDo Task within a specified ToDo Task List.
    /// </summary>
    /// <param name="createToDoTaskDto">The data for creating the new ToDo Task, including its parent list ID.</param>
    /// <returns>The newly created ToDoTaskDto object.</returns>
    /// <response code="201">Returns the newly created ToDo Task.</response>
    /// <response code="400">If the input data is invalid.</response>
    /// <response code="404">If the specified parent ToDo Task List is not found.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ToDoTaskDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ToDoTaskDto>> CreateToDoTask([FromBody] CreateToDoTaskDto createToDoTaskDto)
    {
        // FromBody is used to parse incoming JSON
        // Validate input
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _mediator.Send(new CreateToDoTask(createToDoTaskDto));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); 
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while creating the ToDo Task.");
        }
    }

    /// <summary>
    /// Retrieves a specific ToDo Task by its ID and its parent ToDo Task List ID.
    /// </summary>
    /// <param name="id">The unique identifier of the ToDo Task.</param>
    /// <param name="parentListId">The unique identifier of the parent ToDo Task List.</param>
    /// <returns>A ToDoTaskDto object.</returns>
    /// <response code="200">Returns the requested ToDo Task.</response>
    /// <response code="404">If the ToDo Task or its parent ToDo Task List is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ToDoTaskDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ToDoTaskDto>> GetToDoTaskById(Guid id, [FromQuery] Guid parentListId)
    {
        try
        {
            var result = await _mediator.Send(new GetToDoTaskById(id, parentListId));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred while retrieving the ToDo Task. {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing ToDo Task within a specified ToDo Task List.
    /// </summary>
    /// <param name="id">The unique identifier of the ToDo Task to update (from route).</param>
    /// <param name="updateToDoTaskDto">The updated data for the ToDo Task, including its parent list ID and own ID.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">The ToDo Task was successfully updated.</response>
    /// <response code="400">If the input data is invalid or IDs mismatch.</response>
    /// <response code="404">If the ToDo Task or its parent ToDo Task List is not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> UpdateToDoTask(Guid id, [FromBody] UpdateToDoTaskDto updateToDoTaskDto)
    {
        // Validate that the ID in the route matches the ID in the DTO payload
        if (id != updateToDoTaskDto.Id)
        {
            return BadRequest($"ID in route '{id}' does not match Task ID in payload '{updateToDoTaskDto.Id}'.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _mediator.Send(new UpdateToDoTask(updateToDoTaskDto));
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating the ToDo Task.");
        }
    }

    /// <summary>
    /// Deletes a specific ToDo Task from its parent ToDo Task List.
    /// </summary>
    /// <param name="id">The unique identifier of the ToDo Task to delete (from route).</param>
    /// <param name="deleteToDoTaskDto">The data for deleting the ToDo Task, including its parent list ID.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">The ToDo Task was successfully deleted.</response>
    /// <response code="400">If the input data is invalid or IDs mismatch.</response>
    /// <response code="404">If the ToDo Task or its parent ToDo Task List is not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> DeleteToDoTask(Guid id, [FromBody] DeleteToDoTaskDto deleteToDoTaskDto)
    {
        // Validate that the ID in the route matches the ID in the DTO payload
        if (id != deleteToDoTaskDto.Id)
        {
            return BadRequest($"ID in route '{id}' does not match Task ID in payload '{deleteToDoTaskDto.Id}'.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _mediator.Send(new DeleteToDoTask(deleteToDoTaskDto));
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the ToDo Task.");
        }
    }

    /// <summary>
    /// Retrieves all ToDo Tasks across all lists that have a specific due date and time.
    /// </summary>
    /// <param name="dueDateTime">The specific due date and time to filter tasks by (ISO 8601 format).</param>
    /// <returns>A list of ToDoTaskDto objects.</returns>
    /// <response code="200">Returns the list of ToDo Tasks matching the due date.</response>
    [HttpGet("byduedate")]
    [ProducesResponseType(typeof(List<ToDoTaskDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<ToDoTaskDto>>> GetToDoTasksByDueDateTime([FromQuery] DateTime dueDateTime, [FromQuery] Guid parentListId)
    {
        var result = await _mediator.Send(new GetToDoTaskByDueDateTime(dueDateTime, parentListId));
        return Ok(result);
    }
}
