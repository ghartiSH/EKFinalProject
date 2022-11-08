using FluentValidation;

namespace ServiceLayer.ViewModels.Validators
{
    public class EmployeeValidator: AbstractValidator<EmployeeViewModel>
    {
        private readonly ApplicationDbContext _context;
        public EmployeeValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name must not be empty");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address must not be empty");

            RuleFor(x => x.Salary)
                .NotEmpty()
                .WithMessage("Salary must not be empty");

            RuleFor(x => x.EmployeeCode)
                .NotEmpty()
                .WithMessage("Employee Code must not be empty");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email must not be empty")
                .EmailAddress()
                .WithMessage("Email must be valid")
                .Must(UniqueEmail)
                .WithMessage("Email must be unique");
            
        }

        public bool UniqueEmail(string Email)
        {
            return _context.People.FirstOrDefault(x => x.Email == Email) == null;
        }

    }
}
