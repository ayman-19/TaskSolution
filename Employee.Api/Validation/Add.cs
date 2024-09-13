using FluentValidation;

namespace Employee.Api.Validation
{
    public class Add : AbstractValidator<Requests.Add>
    {
        public Add()
        {
            SetValidation();
        }

        private void SetValidation()
        {
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
