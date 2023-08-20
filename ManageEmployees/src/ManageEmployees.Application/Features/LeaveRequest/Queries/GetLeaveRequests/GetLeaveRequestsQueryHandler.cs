using AutoMapper;
using ManageEmployees.Application.Contracts;
using ManageEmployees.Application.Contracts.Identity;
using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery,
        List<LeaveRequestDTO>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public GetLeaveRequestsQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<List<LeaveRequestDTO>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<Domain.LeaveRequest>();
            var requests = new List<LeaveRequestDTO>();

            if (request.IsLoggedInUser)
            {
                var userId = _userService.UserId;
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsyncByUser(userId);

                var employee = await _userService.GetEmployee(userId);
                requests = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync();
                requests = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId.ToString());
                }
            }

            return requests;
        }
    }
}
