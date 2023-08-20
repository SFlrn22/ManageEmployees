using AutoMapper;
using ManageEmployees.Application.Contracts;
using ManageEmployees.Application.Contracts.Identity;
using ManageEmployees.Application.Exceptions;
using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IUserService userService,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _userService = userService;
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid LeaveRequest", validationResult);

            var employeeId = _userService.UserId;

            var allocation = await _leaveAllocationRepository.GetUserAllocationsAsync(employeeId,
                request.LeaveTypeId);

            if (allocation is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure
                    (nameof(request.LeaveTypeId),
                    "You do not have any allocations for this leave type"));
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            int daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;

            if (daysRequested > allocation.NumberOfDays)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure
                    (nameof(request.EndDate),
                    "You do not have enough days for this request"));
            }

            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            leaveRequest.RequestingEmployeeId = employeeId;
            leaveRequest.RequestDate = DateTime.Now;
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            return Unit.Value;
        }
    }
}
