using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Mappings;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Repository;
using ToDoList.Domain.Entities;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
);

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(MappingProfile).Assembly)
    );

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ToDoListDb"),
    ServiceLifetime.Scoped
    );

// Register your Repositories
builder.Services.AddScoped<IToDoTaskListRepository, ToDoTaskListRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Add CORS for different origins (.NET & Typescript)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

// Data Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>(); 
        context.Database.EnsureCreated(); 

        if (!context.ToDoTaskListCollection.Any())
        {
            var todayList = new ToDoTaskList("Today");

            var loremIpsumTask = new ToDoTask(
                notes: "Lorem ipsum dolor sit amet.",
                title: "Example Task",
                dueDateTime: DateTime.Today.AddHours(17), // Due 5 PM today
                isDone: false
            );

            todayList.AddTask(loremIpsumTask); 

            context.ToDoTaskListCollection.Add(todayList);
            context.ToDoTaskCollection.Add(loremIpsumTask);

            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the in-memory database.");
    }
}

app.UseCors();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
