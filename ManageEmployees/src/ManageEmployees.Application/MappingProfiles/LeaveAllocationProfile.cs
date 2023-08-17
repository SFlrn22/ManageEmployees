using AutoMapper;
using ManageEmployees.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using ManageEmployees.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using ManageEmployees.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using ManageEmployees.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using ManageEmployees.Domain;

namespace ManageEmployees.Application.MappingProfiles
{
    public class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocationDTO, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDetailsDTO>();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
        }
    }
}
