using FluentValidation;
using ManageEmployees.Application.Contracts.Persistence;

namespace ManageEmployees.Application.Features.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public CreateEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(40).WithMessage("{PropertyName} should not exceed 40 characters");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(40).WithMessage("{PropertyName} should not exceed 40 characters");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(60).WithMessage("{PropertyName} should not exceed 60 characters");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress();

            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(10).WithMessage("{PropertyName} should not exceed 10 characters");

            RuleFor(c => c.EmploymentTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .IsInEnum();

            RuleFor(c => c.JobTitle)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(20).WithMessage("{PropertyName} should not exceed 20 characters");

            RuleFor(c => c.DepartmentId)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(q => q)
                .MustAsync(CheckUniqueEmployee)
                .WithMessage("Employee already exists");

            _employeeRepository = employeeRepository;
        }

        private Task<bool> CheckUniqueEmployee(CreateEmployeeCommand command, CancellationToken token)
        {
            return _employeeRepository.IsEmployeeUnique(command.FirstName, command.LastName, command.Email);
        }
    }
}
