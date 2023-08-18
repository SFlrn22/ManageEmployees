﻿using FluentValidation;

namespace ManageEmployees.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandValidator :
        AbstractValidator<ChangeLeaveRequestApprovalCommand>
    {
        public ChangeLeaveRequestApprovalCommandValidator()
        {
            RuleFor(l => l.Approved)
                .NotNull()
                .WithMessage("Approval status cannot be null");
        }
    }
}
