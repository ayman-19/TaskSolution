using Employee.Api.Data;
using Employee.Api.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.Api.Handler
{
    public class GetById : IRequestHandler<Requests.GetById, Response>
    {
        private readonly EmployeeDbContext _context;

        public GetById(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(
            Requests.GetById request,
            CancellationToken cancellationToken
        )
        {
            var validator = new Validation.GetById(_context);
            var result = validator.Validate(request);
            if (result.Errors.Count > 0)
                return new Response(0, "", "", 0, string.Join(",", result.Errors));
            var employee = await _context
                .Employees.AsNoTracking()
                .FirstAsync(e => e.EmployeeId == request.id);
            var response = new Response(
                employee.EmployeeId,
                employee.Name,
                employee.Department,
                employee.Salary,
                "OK"
            );
            return response;
        }
    }
}
