using AutoMapper;
using ManageEmployees.Application.Contracts;
using MediatR;

namespace ManageEmployees.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationListQuerryHandler : IRequestHandler<GetLeaveAllocationListQuerry, List<LeaveAllocationDTO>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        public GetLeaveAllocationListQuerryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }
        public async Task<List<LeaveAllocationDTO>> Handle(GetLeaveAllocationListQuerry request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationWithDetailsAsync();
            var allocations = _mapper.Map<List<LeaveAllocationDTO>>(leaveAllocations);
            return allocations;
        }
    }
}
