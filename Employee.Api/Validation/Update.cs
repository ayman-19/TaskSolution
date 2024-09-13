using Employee.Api.Data;
using FluentValidation;

namespace Employee.Api.Validation
{
    public class Update : AbstractValidator<Requests.Update>
    {
        private readonly EmployeeDbContext _context;

        public Update(EmployeeDbContext context)
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

            RuleFor(p => p.salary)
                .NotEmpty()
                .WithMessage("Salary Not Empty")
                .NotNull()
                .WithMessage("Salary Not Null")
                .NotEqual(0)
                .WithMessage("Salary Not Equal Zero")
                .GreaterThan(0)
                .WithMessage("Salary Not less than Zero");

            RuleFor(p => p.department)
                .NotEmpty()
                .WithMessage("Department Not Empty")
                .NotNull()
                .WithMessage("Department Not Null")
                .MaximumLength(100)
                .WithMessage("Department Lenth Must less or equal 100");

            RuleFor(p => p.name)
                .NotEmpty()
                .WithMessage("Name Not Empty")
                .NotNull()
                .WithMessage("Name Not Null")
                .MaximumLength(100)
                .WithMessage("Name Lenth Must less or equal 100");
        }
    }
}
