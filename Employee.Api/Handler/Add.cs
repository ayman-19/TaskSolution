using Employee.Api.Data;
using Employee.Api.Dtos;
using MediatR;

namespace Employee.Api.Handler
{
    public class Add : IRequestHandler<Requests.Add, Response>
    {
        private readonly EmployeeDbContext _context;

        public Add(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(
            Requests.Add request,
            CancellationToken cancellationToken
        )
        {
            var validator = new Validation.Add();
            var result = validator.Validate(request);
            if (result.Errors.Count > 0)
                return new Response(0, "", "", 0, string.Join(",", result.Errors));
            var employee = Entities.Employee.Create(
                request.name,
                request.department,
                request.salary
            );
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return new Response(
                employee.EmployeeId,
                employee.Name,
                employee.Department,
                employee.Salary,
                "OK"
            );
        }
    }
}
