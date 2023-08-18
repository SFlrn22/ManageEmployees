using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public bool Approved { get; set; }
    }
}
