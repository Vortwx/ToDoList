﻿using AutoMapper; 
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Mappings;
using ToDoList.Application.ToDoTaskLists.Commands.CreateToDoTaskList;
using ToDoList.Application.ToDoTaskLists.Dtos;
using ToDoList.Application.ToDoTaskLists.Queries.GetToDoTaskListById;
using ToDoList.Application.ToDoTasks.Commands.CreateToDoTask;
using ToDoList.Application.ToDoTasks.Dtos;
using ToDoList.Application.ToDoTasks.Queries.GetToDoTaskByDueDateTime;
using ToDoList.Application.ToDoTasks.Queries.GetToDoTaskById;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Repository; 
using Xunit; 

namespace ToDoList.UnitTests;

public class ToDoListTests
{
    private ApplicationDbContext _context;
    private IToDoTaskListRepository _toDoTaskListRepository;
    private IMapper _mapper;
    private IRequestHandler<CreateToDoTaskList, ToDoTaskListDto> _createToDoTaskListHandler;
    private IRequestHandler<CreateToDoTask, ToDoTaskDto> _createToDoTaskHandler; // Handler for creating tasks
    private GetToDoTaskListByIdHandler _getToDoTaskListByIdHandler;
    private GetToDoTaskByIdHandler _getToDoTaskByIdHandler;
    private GetToDoTaskByDueDateTimeHandler _getToDoTasksByDueDateTimeHandler; 

    public ToDoListTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated(); 

        _toDoTaskListRepository = new ToDoTaskListRepository(_context);

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

        _createToDoTaskListHandler = new CreateToDoTaskListHandler(_toDoTaskListRepository, _mapper);
        _createToDoTaskHandler = new CreateToDoTaskHandler(_toDoTaskListRepository, _mapper);
        _getToDoTaskListByIdHandler = new GetToDoTaskListByIdHandler(_toDoTaskListRepository, _mapper);
        _getToDoTaskByIdHandler = new GetToDoTaskByIdHandler(_toDoTaskListRepository, _mapper);
        _getToDoTasksByDueDateTimeHandler = new GetToDoTaskByDueDateTimeHandler(_toDoTaskListRepository, _mapper); // Instantiate the new query handler
    }

    // --- Test Method for Creating a ToDoTaskList ---
    [Fact]
    public async Task CreateToDoTaskList_ShouldAddTaskListToInMemoryDatabase()
    {
        var createToDoTaskListDto = new CreateToDoTaskListDto("My New Test List");
        var command = new CreateToDoTaskList(createToDoTaskListDto);


        var resultToDoTaskListDto = await _createToDoTaskListHandler.Handle(command, CancellationToken.None);


        Assert.NotNull(resultToDoTaskListDto);
        Assert.NotEqual(Guid.Empty, resultToDoTaskListDto.Id);
        Assert.Equal("My New Test List", resultToDoTaskListDto.Name);


        var retrievedToDoTaskList = await _context.ToDoTaskListCollection
                                        .FirstOrDefaultAsync(tl => tl.Id == resultToDoTaskListDto.Id);

        Assert.NotNull(retrievedToDoTaskList);
        Assert.Equal("My New Test List", retrievedToDoTaskList.Name);
        Assert.Empty(retrievedToDoTaskList.Tasks); // Should be empty initially
    }

    // --- Test Method for Adding a ToDoTask to a ToDoTaskList ---
    [Fact]
    public async Task AddTask_ShouldAddTasksToInMemoryDatabase()
    {
        var createParentToDoTaskListDto = new CreateToDoTaskListDto("List for Tasks");
        var createParentListCommand = new CreateToDoTaskList(createParentToDoTaskListDto);
        var createdParentListDto = await _createToDoTaskListHandler.Handle(createParentListCommand, CancellationToken.None);

        var createToDoTaskDto = new CreateToDoTaskDto
        (
            "New Task",
            "Some notes for the task",
            DateTime.Now.AddDays(7),
            false,
            createdParentListDto.Id
        );
        var createToDoTaskCommand = new CreateToDoTask(createToDoTaskDto);

        var createdToDoTaskDto = await _createToDoTaskHandler.Handle(createToDoTaskCommand, CancellationToken.None);

        Assert.NotNull(createdToDoTaskDto);
        Assert.NotEqual(Guid.Empty, createdToDoTaskDto.Id);
        Assert.Equal("New Task", createdToDoTaskDto.Title);

        var retrievedToDoTaskListWithTask = await _context.ToDoTaskListCollection
                                                    .Include(tl => tl.Tasks) // Include tasks to verify
                                                    .FirstOrDefaultAsync(tl => tl.Id == createdParentListDto.Id);

        Assert.NotNull(retrievedToDoTaskListWithTask);
        Assert.Single(retrievedToDoTaskListWithTask.Tasks); // Expect one task
        Assert.Equal("New Task", retrievedToDoTaskListWithTask.Tasks.First().Title);
        Assert.Equal(createdToDoTaskDto.Id, retrievedToDoTaskListWithTask.Tasks.First().Id);

        // --- Verify Queries are Working Properly ---

        // 1. Query the ToDoTaskList by ID (expecting it to now contain the new task)
        var queryListById = new GetToDoTaskListById(createdParentListDto.Id);
        var queriedListDto = await _getToDoTaskListByIdHandler.Handle(queryListById, CancellationToken.None);

        Assert.NotNull(queriedListDto);
        Assert.Equal(createdParentListDto.Id, queriedListDto.Id);
        Assert.Equal("List for Tasks", queriedListDto.Name);
        Assert.Single(queriedListDto.Tasks); // Assert that the list now contains one task

        // 2. Query the ToDoTask by its ID within its parent list
        var queryTaskById = new GetToDoTaskById(createdToDoTaskDto.Id, createdParentListDto.Id);
        var queriedTaskDto = await _getToDoTaskByIdHandler.Handle(queryTaskById, CancellationToken.None);

        Assert.NotNull(queriedTaskDto);
        Assert.Equal(createdToDoTaskDto.Id, queriedTaskDto.Id);
        Assert.Equal("New Task", queriedTaskDto.Title);
        Assert.Equal("Some notes for the task", queriedTaskDto.Notes);
        Assert.False(queriedTaskDto.IsDone);
    }

    // --- Examine the correctness of GetToDoTaskByDueDateTime which collects all eligible ToDoTask across all ToDoTaskList ---
    [Fact]
    public async Task GetTasksByDueDateTime_ShouldReturnAllTasksOnSameDate()
    {
        var commonDueDate = DateTime.Today.AddDays(1);

        // --- Create ToDoTaskList One and ToDoTask One ---
        var listOneDto = new CreateToDoTaskListDto("Daily Chores");
        var listOneCommand = new CreateToDoTaskList(listOneDto);
        var createdListOneDto = await _createToDoTaskListHandler.Handle(listOneCommand, CancellationToken.None);

        var taskOneDto = new CreateToDoTaskDto
        (
            "Morning Routine",
            "Drink coffee",
            commonDueDate.AddHours(9),
            false,
            createdListOneDto.Id
        );
        var taskOneCommand = new CreateToDoTask(taskOneDto);
        var createdTaskOneDto = await _createToDoTaskHandler.Handle(taskOneCommand, CancellationToken.None);

        // --- Create ToDoTaskList Two and ToDoTask Two ---
        var listTwoDto = new CreateToDoTaskListDto("Homework");
        var listTwoCommand = new CreateToDoTaskList(listTwoDto);
        var createdListTwoDto = await _createToDoTaskListHandler.Handle(listTwoCommand, CancellationToken.None);

        var taskTwoDto = new CreateToDoTaskDto
        (
            "Software Design Report",
            "Email to lecturer",
            commonDueDate.AddHours(17),
            false,
            createdListTwoDto.Id
        );
        var taskTwoCommand = new CreateToDoTask(taskTwoDto);
        var createdTaskTwoDto = await _createToDoTaskHandler.Handle(taskTwoCommand, CancellationToken.None);


        var query = new GetToDoTaskByDueDateTime(commonDueDate);
        var resultTasks = await _getToDoTasksByDueDateTimeHandler.Handle(query, CancellationToken.None);

        Assert.NotNull(resultTasks);
        Assert.Equal(2, resultTasks.Count); 

        // Both created task IDs are in the result
        Assert.Contains(resultTasks, t => t.Id == createdTaskOneDto.Id);
        Assert.Contains(resultTasks, t => t.Id == createdTaskTwoDto.Id);

        // Check that both tasks have the same due date as queried
        var retrievedTaskOne = resultTasks.FirstOrDefault(t => t.Id == createdTaskOneDto.Id);
        Assert.NotNull(retrievedTaskOne);
        Assert.Equal(commonDueDate.Date, retrievedTaskOne.DueDateTime.Date); 

        var retrievedTaskTwo = resultTasks.FirstOrDefault(t => t.Id == createdTaskTwoDto.Id);
        Assert.NotNull(retrievedTaskTwo);
        Assert.Equal(commonDueDate.Date, retrievedTaskTwo.DueDateTime.Date); 
    }

}