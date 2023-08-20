using ManageEmployees.Application.Contracts;
using ManageEmployees.Application.Exceptions;
using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            leaveRequest.Cancelled = true;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);
            if (leaveRequest.Approved is true)
            {
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                var allocation = await _leaveAllocationRepository.GetUserAllocationsAsync(
                    leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                allocation.NumberOfDays += daysRequested;

                await _leaveAllocationRepository.UpdateAsync(allocation);
            }

            return Unit.Value;
        }
    }
}
