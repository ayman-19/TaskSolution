using Employee.Api.Dtos;
using MediatR;

namespace Employee.Api.Requests
{
    public record Add(string name, string department, decimal salary) : IRequest<Response>;
}
