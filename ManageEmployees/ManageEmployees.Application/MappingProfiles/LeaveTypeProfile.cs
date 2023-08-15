using AutoMapper;
using ManageEmployees.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using ManageEmployees.Application.Features.LeaveType.Queries.GetLeaveTypesDetails;
using ManageEmployees.Domain;

namespace ManageEmployees.Application.MappingProfiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDTO, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDTO>();
        }
    }
}
