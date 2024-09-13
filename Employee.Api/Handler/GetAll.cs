using Employee.Api.Data;
using Employee.Api.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.Api.Handler
{
    public class GetAll : IRequestHandler<Requests.GetAll, List<Response>>
    {
        private readonly EmployeeDbContext _context;

        public GetAll(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Response>> Handle(
            Requests.GetAll request,
            CancellationToken cancellationToken
        )
        {
            var employees = await _context
                .Employees.AsNoTracking()
                .Select(employee => new Response(
                    employee.EmployeeId,
                    employee.Name,
                    employee.Department,
                    employee.Salary,
                    "OK"
                ))
                .ToListAsync();
            return employees;
        }
    }
}
