using Employee.Api.Data;
using FluentValidation;

namespace Employee.Api.Validation
{
    public class Delete : AbstractValidator<Requests.Delete>
    {
        private readonly EmployeeDbContext _context;

        public Delete(EmployeeDbContext context)
        {
            SetValidation();
            _context = context;
        }

        private void SetValidation()
        {
            RuleFor(p => p.id)
                .Must((id) => _context.Employees.Any(e => e.EmployeeId == id))
                .WithMessage("Employee Not Found")
                .NotEmpty()
                .WithMessage("Id Not Empty")
                .NotNull()
                .WithMessage("Id Not Null");
        }
    }
}
