using AutoMapper;
using ManageEmployees.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using ManageEmployees.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using ManageEmployees.Domain;

namespace ManageEmployees.Application.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequestDTO, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequestDetailsDTO, LeaveRequest>().ReverseMap();
            CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
        }
    }
}
