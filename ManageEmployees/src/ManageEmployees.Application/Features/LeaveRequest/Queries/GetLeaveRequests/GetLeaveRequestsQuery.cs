using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequests
{
    public record GetLeaveRequestsQuery : IRequest<List<LeaveRequestDTO>>;
}
