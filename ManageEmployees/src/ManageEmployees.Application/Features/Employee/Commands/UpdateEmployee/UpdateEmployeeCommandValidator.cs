using FluentValidation;
using ManageEmployees.Application.Contracts.Persistence;
using ManageEmployees.Domain.Enums;

namespace ManageEmployees.Application.Features.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(u => u.Id)
                .NotNull()
                .MustAsync(EmployeeMustExist);

            RuleFor(u => u.FirstName)
                .MaximumLength(40).WithMessage("{PropertyName} should not exceed 40 characters");

            RuleFor(u => u.LastName)
                .MaximumLength(40).WithMessage("{PropertyName} should not exceed 40 characters");

            RuleFor(u => u.City)
                .MaximumLength(60).WithMessage("{PropertyName} should not exceed 60 characters");

            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("{PropertyName} is invalid");

            RuleFor(u => u.PhoneNumber)
                .MaximumLength(10).WithMessage("{PropertyName} should not exceed 10 characters");

            RuleFor(u => u.EmploymentTypeId)
                .Must(CheckIfInEnum).WithMessage("{PropertyName} is invalid");

            RuleFor(u => u.JobTitle)
                .MaximumLength(20).WithMessage("{PropertyName} should not exceed 20 characters");

            RuleFor(u => u)
                .MustAsync(CheckUniqueEmployee).WithMessage("Employee already exists");

            _employeeRepository = employeeRepository;
        }
        private bool CheckIfInEnum(int value)
        {
            return Enum.IsDefined(typeof(EmploymentTypesEnum), value);
        }
        private async Task<bool> CheckUniqueEmployee(UpdateEmployeeCommand command, CancellationToken token)
        {
            return await _employeeRepository.IsEmployeeUnique(command.FirstName, command.LastName, command.Email);
        }
        private async Task<bool> EmployeeMustExist(int id, CancellationToken token)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee != null;
        }
    }
}
