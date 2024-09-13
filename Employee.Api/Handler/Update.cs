using Employee.Api.Data;
using Employee.Api.Dtos;
using MediatR;

namespace Employee.Api.Handler
{
    public class Update : IRequestHandler<Requests.Update, Response>
    {
        private readonly EmployeeDbContext _context;

        public Update(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(
            Requests.Update request,
            CancellationToken cancellationToken
        )
        {
            var validator = new Validation.Update(_context);
            var result = validator.Validate(request);
            if (result.Errors.Count > 0)
                return new Response(0, "", "", 0, string.Join(",", result.Errors));
            var employee = Entities.Employee.Create(
                request.name,
                request.department,
                request.salary
            );
            employee.EmployeeId = request.id;
            _context.Employees.Update(employee);
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
