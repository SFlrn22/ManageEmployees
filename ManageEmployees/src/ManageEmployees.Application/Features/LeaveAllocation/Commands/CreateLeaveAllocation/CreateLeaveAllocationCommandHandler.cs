using ManageEmployees.Application.Contracts;
using ManageEmployees.Application.Contracts.Identity;
using ManageEmployees.Application.Exceptions;
using MediatR;

namespace ManageEmployees.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler :
        IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;
        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IUserService userService)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _userService = userService;
        }
        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid LeaveAllocation Request", validationResult);

            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
            var employees = await _userService.GetEmployees();
            var period = DateTime.Now.Year;
            var allocationsToAdd = new List<Domain.LeaveAllocation>();


            foreach (var employee in employees)
            {
                var allocationExists = await _leaveAllocationRepository.AllocationExistsAsync(employee.Id, leaveType.Id, period);

                if (allocationExists is false)
                {
                    allocationsToAdd.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = employee.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period
                    });
                }
            }

            if (allocationsToAdd.Any())
            {
                await _leaveAllocationRepository.AddAllocationsAsync(allocationsToAdd);
            }

            return Unit.Value;
        }
    }
}
