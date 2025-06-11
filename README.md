# ToDoList

### Introduction
Designed based on Clean Architecture
Initial setup
```
dotnet new sln -n ToDoList
dotnet new webapi -n ToDoList.Api (Presentation Layer)
dotnet new classlib -n ToDoList.Application (Application Layer)
dotnet new classlib -n ToDoList.Domain (Domain Layer)
dotnet new classlib -n ToDoList.Infrastructure (Infrastructure Layer)
```
Setup project
```
dotnet sln add ToDoList.Api/ToDoList.Api.csproj
dotnet sln add ToDoList.Application/ToDoList.Application.csproj
dotnet sln add ToDoList.Domain/ToDoList.Domain.csproj
dotnet sln add ToDoList.Infrastructure/ToDoList.Infrastructure.csproj
```
Setup dependencies

Infra can access to Application & Domain
```
dotnet add ToDoList.Infrastructure/ToDoList.Infrastructure.csproj reference ToDoList.Application/ToDoList.Application.csproj
dotnet add ToDoList.Infrastructure/ToDoList.Infrastructure.csproj reference ToDoList.Domain/ToDoList.Domain.csproj

```
Application can access to Domain
```
dotnet add ToDoList.Application/ToDoList.Application.csproj reference ToDoList.Domain/ToDoList.Domain.csproj
```
Presentation can access to Application and Infrastructure (Conceptually just Application, but the implementation of data storage is Dependency Injected into Infrastructure hence the necessity)
```
dotnet add ToDoList.Api/ToDoList.Api.csproj reference ToDoList.Application/ToDoList.Application.csproj
dotnet add ToDoList.Api/ToDoList.Api.csproj reference ToDoList.Infrastructure/ToDoList.Infrastructure.csproj
```

Add DbContext and configure it in Program.cs under Presentation Layer

```
dotnet dev-certs https --trust
```
Command that is useful when browser complains about the unsafe localhost page.

The API structure is quite different from the traditional Service-Controller architecture

1. IRepository & DTO: Define contracts of what to do (like CRUD) and what Dto is used to communicate (Application)
2. Repository: Abstract away details of data storage and retrieval, Know how to store but don't know why (Infrastructure, theoretically it should be in )
3. Service: Define business logic, implement IRepository, technical details of how data is stored/retrieved is abstracted away (Application)
4. Controller: Entry point for external API request (Presentation) 