using AutoMapper;
using ManageEmployees.Application.DTO;
using ManageEmployees.Domain;

namespace ManageEmployees.Application.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentDTO, Department>().ReverseMap();
        }
    }
}
