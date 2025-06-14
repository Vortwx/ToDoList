# ToDoList

### Project Setup
```
dotnet run --project ToDoList.Api
```
**Need to run before running frontend to-do-list-ui project**
Test cases can be runned with 
```
dotnet test
```

### File structure setup
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

Database seeding with 1 default list and 1 task created is done upon the initialisation of backend server 

### Design Rationale
Schema:
- I design the two entities (ToDoTask, ToDoTaskList) in an aggregated pattern.
- ToDoTask is managed by ToDoTaskList and cannot exists outside of the ToDoTaskList.
- This implies a many-to-one relationship from ToDoTask -> ToDoTaskList.
- All ToDoTask is managed via ToDoTaskList hence only ToDoTaskListRepository is used in this case (ToDoTaskRepository is created for the possible change in schema, i.e. ToDoTask no longer depends on ToDoTaskList)
- Although these 2 entities are aggregated, due to the features of Entity Framework I was able to collect ToDoTask across all ToDoTaskList without manually iterates through all of the ToDoTaskList (Done by maintaing a DBSet of ToDoTask)


API Structure:
1. IRepository & DTO: Define contracts of what to do (like CRUD) and what Dto is used to communicate (Application)
2. Repository: Abstract away details of data storage and retrieval, Know how to store but don't know why (Infrastructure, theoretically it should be in )
3. Service: Define business logic, implement IRepository, technical details of how data is stored/retrieved is abstracted away (Application)
4. Controller: Entry point for external API request (Presentation) 

I choose to maintain different DTO for input(Create, Update, in special case-Delete) instead of using one general DTO even when the contents are mostly the same.
This is motivated by 
1. ISP in SOLID principle (Each class should have /contract that is customised to them)
2. Extendability (Future addition can directly works by adding on current work)
3. Easier validation from DTO's Annotation

In the Controller design, I ask user for id repetitively in a implicit way (by explicitly requires an id for ToDoTask and implicitly requires an id in respective Dto). I thought that this could help double confirm the correct id since taskId and its parentList id both use Guid which can easily be confusing. However, later when I intergrating .NET backend with React frontend this problem is found impossible to happen as I will create the Dto and the id at the same time, which makes it redundant. As it doesn't affect functionality I will keep it as-is.

### Issues encountered
1. Encounter a problem where the EF tracker misclassified the state of item leading to an error: Attempted to update or delete an entity that does not exist in the store.
Resolved by not specifying Guid at start and let EF core do the work.
2. Lazy evaluation of EF core requires Include() to show child properties
3. CORS issue when communicating with Frontend
4. Configuration of each entities is delegated to DTOs instead to enable the 'Required create, Optional update' pattern to allow patching

### Tests
This unittest covers basic test as most of the part can be covered in Frontend Interaction
1. Test ToDoTaskList creation
2. Test ToDoTask creation and synchronous update of its parent ToDoTaskList
3. Test GetToDoTaskByDueDateTime
    - due to Frontend doesn't include this option
    - and it is an important function to showcase how Entity Framework works

