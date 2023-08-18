using AutoMapper;
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
        }
    }
}
