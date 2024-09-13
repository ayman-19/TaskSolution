using Employee.Api.Dtos;
using MediatR;

namespace Employee.Api.Requests
{
    public record Delete(int id) : IRequest<Response>;
}
