using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailsQuery : IRequest<LeaveRequestDetailsDTO>
    {
        public int Id { get; set; }
    }
}
