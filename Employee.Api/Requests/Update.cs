using Employee.Api.Dtos;
using MediatR;

namespace Employee.Api.Requests
{
    public record Update(int id, string name, string department, decimal salary)
        : IRequest<Response>;
}
