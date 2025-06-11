using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Mappings;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(MappingProfile).Assembly)
    );

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ToDoListDb")
    );

// Register your Repositories
builder.Services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
builder.Services.AddScoped<IToDoTaskListRepository, ToDoTaskListRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();
app.MapGet("/", () => "Hello from ToDoList API!");

app.Run();
