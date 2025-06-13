using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// Import your DTOs, Commands, and Queries
using ToDoList.Application.ToDoTaskLists.Commands.CreateToDoTaskList;
using ToDoList.Application.ToDoTaskLists.Commands.DeleteToDoTaskList;
using ToDoList.Application.ToDoTaskLists.Commands.UpdateToDoTaskList;
using ToDoList.Application.ToDoTaskLists.Dtos;
using ToDoList.Application.ToDoTaskLists.Queries.GetAllToDoTaskLists;
using ToDoList.Application.ToDoTaskLists.Queries.GetToDoTaskListById;

namespace ToDoList.Api.Controllers;

/// <summary>
/// Controller for managing ToDo Task Lists, which serve as aggregate roots for ToDo Tasks.
/// </summary>
[ApiController]
[Route("api/todolists")]
[Produces("application/json")] // Specify the default content type for responses
public class ToDoTaskListController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ToDoTaskListController"/> class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator for sending commands and queries.</param>
    public ToDoTaskListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves all ToDo Task Lists.
    /// </summary>
    /// <returns>A list of ToDoTaskListDto objects.</returns>
    /// <response code="200">Returns the list of ToDo Task Lists.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ToDoTaskListDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<ToDoTaskListDto>>> GetAllToDoTaskLists()
    {
        var result = await _mediator.Send(new GetAllToDoTaskLists());
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific ToDo Task List by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the ToDo Task List.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A ToDoTaskListDto object.</returns>
    /// <response code="200">Returns the requested ToDo Task List.</response>
    /// <response code="404">If the ToDo Task List with the specified ID is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ToDoTaskListDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ToDoTaskListDto>> GetToDoTaskListById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetToDoTaskListById(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"ToDo Task List with ID '{id}' not found.");
        }
    }

    /// <summary>
    /// Creates a new ToDo Task List.
    /// </summary>
    /// <param name="createDto">The data for creating the new ToDo Task List.</param>
    /// <returns>The newly created ToDoTaskListDto object.</returns>
    /// <response code="200">Returns the newly created ToDo Task List.</response>
    /// <response code="400">If the input data is invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ToDoTaskListDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ToDoTaskListDto>> CreateToDoTaskList([FromBody] CreateToDoTaskListDto createDto)
    {
        // Add basic validation for the DTO
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _mediator.Send(new CreateToDoTaskList(createDto));
            return Ok(result);
        }
        catch (ArgumentException ex) // Catches validation errors from domain (e.g., empty name)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex) // Catch any other unexpected errors
        {
            // Log the exception (using a logger if available)
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while creating the ToDo Task List.");
        }
    }

    /// <summary>
    /// Updates an existing ToDo Task List, including its associated ToDo Tasks.
    /// </summary>
    /// <param name="id">The unique identifier of the ToDo Task List to update.</param>
    /// <param name="updateToDoTaskListDto">The updated data for the ToDo Task List and its tasks.</param>
    /// <returns>The updated ToDoTaskListDto object.</returns>
    /// <response code="200">Returns the updated ToDo Task List.</response>
    /// <response code="400">If the input data is invalid or IDs mismatch.</response>
    /// <response code="404">If the ToDo Task List with the specified ID is not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ToDoTaskListDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ToDoTaskListDto>> UpdateToDoTaskList(Guid id, [FromBody] UpdateToDoTaskListDto updateToDoTaskListDto)
    {
        // Validate that the ID in the route matches the ID in the DTO payload
        if (id != updateToDoTaskListDto.Id)
        {
            return BadRequest($"ID in route '{id}' does not match ID in payload '{updateToDoTaskListDto.Id}'.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _mediator.Send(new UpdateToDoTaskList(updateToDoTaskListDto));
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
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating the ToDo Task List.");
        }
    }

    /// <summary>
    /// Deletes a ToDo Task List by its ID.
    /// This will also cascade delete all associated ToDo Tasks.
    /// </summary>
    /// <param name="id">The unique identifier of the ToDo Task List to delete.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">The ToDo Task List was successfully deleted.</response>
    /// <response code="404">If the ToDo Task List with the specified ID is not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> DeleteToDoTaskList(Guid id, [FromBody] ToDoTaskListDto toDoTaskListDto)
    {
        if (id != toDoTaskListDto.Id)
        {
            return BadRequest($"ID in route '{id}' does not match ID in payload '{toDoTaskListDto.Id}'.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _mediator.Send(new DeleteToDoTaskList(toDoTaskListDto));
            return NoContent(); 
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"ToDo Task List with ID '{id}' not found.");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the ToDo Task List.");
        }
    }
}
