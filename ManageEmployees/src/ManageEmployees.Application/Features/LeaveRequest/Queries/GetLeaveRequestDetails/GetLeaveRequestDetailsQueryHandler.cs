using AutoMapper;
using ManageEmployees.Application.Contracts;
using ManageEmployees.Application.Contracts.Identity;
using ManageEmployees.Application.Exceptions;
using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDTO>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public GetLeaveRequestDetailsQueryHandler(ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper, IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<LeaveRequestDetailsDTO> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveRequestDetails = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.Id);
            var data = _mapper.Map<LeaveRequestDetailsDTO>(leaveRequestDetails);

            if (leaveRequestDetails == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            data.Employee = await _userService.GetEmployee(leaveRequestDetails.RequestingEmployeeId);
            return data;
        }
    }
}
