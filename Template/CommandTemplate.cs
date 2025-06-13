// @ is name
// ~ is crud

using MediatR;
using ToDoList.Application.@s.Dtos;

namespace ToDoList.Application.@s.Commands.~@;

// ~@Dto only serves as Input DTO (represent input payload)
public class ~@ : IRequest<@Dto>
{
    public ~@Dto ~@Dto { get; }
    public ~@(~@Dto ~@Dto)
    {
        ~@Dto = ~@Dto;
    }
}