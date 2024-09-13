using Employee.Api.Dtos;
using MediatR;

namespace Employee.Api.Requests
{
    public record GetAll() : IRequest<List<Response>>;
}
