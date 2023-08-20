using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsQuery : IRequest<List<LeaveRequestDTO>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
