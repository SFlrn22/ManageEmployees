using FluentValidation;
using ManageEmployees.Application.Contracts;

namespace ManageEmployees.Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(l => l.StartDate)
                .LessThan(c => c.EndDate)
                .WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(l => l.EndDate)
                .GreaterThan(c => c.StartDate)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(l => l.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist");

            _leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
