using Employee.Api.Dtos;
using MediatR;

namespace Employee.Api.Requests
{
    public record GetById(int id) : IRequest<Response>;
}
