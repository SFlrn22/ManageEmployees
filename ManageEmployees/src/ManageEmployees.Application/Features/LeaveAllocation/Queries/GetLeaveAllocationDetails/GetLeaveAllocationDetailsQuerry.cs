using MediatR;

namespace ManageEmployees.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQuerry : IRequest<LeaveAllocationDetailsDTO>
    {
        public int Id { get; set; }
    }
}
