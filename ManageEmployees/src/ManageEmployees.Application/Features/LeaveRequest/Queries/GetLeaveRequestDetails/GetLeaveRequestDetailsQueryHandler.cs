using AutoMapper;
using ManageEmployees.Application.Contracts;
using MediatR;

namespace ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDTO>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        public GetLeaveRequestDetailsQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<LeaveRequestDetailsDTO> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveRequestDetails = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.Id);
            var data = _mapper.Map<LeaveRequestDetailsDTO>(leaveRequestDetails);
            return data;
        }
    }
}
