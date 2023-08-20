using AutoMapper;
using ManageEmployees.Application.Contracts;
using ManageEmployees.Application.Exceptions;
using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler :
        IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangeLeaveRequestApprovalCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid approval change", validationResult);

            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            leaveRequest.Approved = request.Approved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            if (request.Approved)
            {
                var daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                var allocation = await _leaveAllocationRepository.GetUserAllocationsAsync(leaveRequest.RequestingEmployeeId,
                    leaveRequest.LeaveTypeId);
                allocation.NumberOfDays -= daysRequested;

                await _leaveAllocationRepository.UpdateAsync(allocation);
            }

            return Unit.Value;
        }
    }
}
