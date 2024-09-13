using Employee.Api.Data;
using Employee.Api.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.Api.Handler
{
    public class Delete : IRequestHandler<Requests.Delete, Response>
    {
        private readonly EmployeeDbContext _context;

        public Delete(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(
            Requests.Delete request,
            CancellationToken cancellationToken
        )
        {
            var validator = new Validation.Delete(_context);
            var result = validator.Validate(request);
            if (result.Errors.Count > 0)
                return new Response(0, "", "", 0, string.Join(",", result.Errors));
            var employee = await _context
                .Employees.AsNoTracking()
                .FirstAsync(e => e.EmployeeId == request.id);
            await _context.Employees.Where(e => e.EmployeeId == request.id).ExecuteDeleteAsync();
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
