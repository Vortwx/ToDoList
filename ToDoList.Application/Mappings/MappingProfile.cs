using AutoMapper;
using ToDoList.Application.ToDoTasks.Dtos;
using ToDoList.Application.ToDoTaskLists.Dtos;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoTask, ToDoTaskDto>();

            // CreateToDoTaskDto is directly used to create a ToDoTask entity:
            CreateMap<CreateToDoTaskDto, ToDoTask>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id

            CreateMap<UpdateToDoTaskDto, ToDoTask>();

            CreateMap<ToDoTaskList, ToDoTaskListDto>();
            
            CreateMap<CreateToDoTaskListDto, ToDoTaskList>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}