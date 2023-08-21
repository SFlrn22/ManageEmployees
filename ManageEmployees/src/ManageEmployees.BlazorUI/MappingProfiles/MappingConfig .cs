using AutoMapper;
using ManageEmployees.BlazorUI.Models;
using ManageEmployees.BlazorUI.Models.LeaveAllocations;
using ManageEmployees.BlazorUI.Models.LeaveRequests;
using ManageEmployees.BlazorUI.Models.LeaveTypes;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.MappingProfiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<LeaveTypeDTO, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeVM, CreateLeaveTypeCommand>().ReverseMap();
            CreateMap<LeaveTypeVM, UpdateLeaveTypeCommand>().ReverseMap();
            CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();

            CreateMap<LeaveRequestDTO, LeaveRequestVM>()
               .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.RequestDate.DateTime)).ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime)).ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
               .ReverseMap();
            CreateMap<LeaveRequestDetailsDTO, LeaveRequestVM>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.RequestDate.DateTime)).ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime)).ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();
            CreateMap<CreateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();

            CreateMap<LeaveAllocationDTO, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocationDetailsDTO, LeaveAllocationVM>().ReverseMap();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();

            CreateMap<EmployeeVM, EmployeeAuth>().ReverseMap();
        }
    }
}
