using MediatR;

namespace ManageEmployees.Application.Features.LeaveType.Queries.GetLeaveTypesDetails
{
    public record GetLeaveTypeDetailsQuery(int id) : IRequest<LeaveTypeDetailsDTO>;
}
