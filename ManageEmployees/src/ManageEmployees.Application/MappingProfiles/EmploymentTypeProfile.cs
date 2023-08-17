using AutoMapper;
using ManageEmployees.Application.DTO;
using ManageEmployees.Domain.Models;

namespace ManageEmployees.Application.MappingProfiles
{
    public class EmploymentTypeProfile : Profile
    {
        public EmploymentTypeProfile()
        {
            CreateMap<EmploymentTypeDTO, EmploymentType>().ReverseMap();
        }
    }
}
