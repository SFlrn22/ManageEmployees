using FluentValidation;
using ManageEmployees.Application.Contracts;

namespace ManageEmployees.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} should not exceed 70 characters");

            RuleFor(c => c.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");
            RuleFor(q => q)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("LeaveType already exists");
            _leaveTypeRepository = leaveTypeRepository;
        }

        private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}
