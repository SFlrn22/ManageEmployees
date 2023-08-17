using MediatR;

namespace ManageEmployees.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationListQuerry : IRequest<List<LeaveAllocationDTO>>
    {
    }
}
