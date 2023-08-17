using AutoMapper;
using ManageEmployees.Application.Contracts;
using MediatR;

namespace ManageEmployees.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQuerryHandler : IRequestHandler<GetLeaveAllocationDetailsQuerry, LeaveAllocationDetailsDTO>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        public GetLeaveAllocationDetailsQuerryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }
        public async Task<LeaveAllocationDetailsDTO> Handle(GetLeaveAllocationDetailsQuerry request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetailsAsync(request.Id);
            var data = _mapper.Map<LeaveAllocationDetailsDTO>(leaveAllocation);
            return data;
        }
    }
}
