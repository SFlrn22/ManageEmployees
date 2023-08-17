using AutoMapper;
using ManageEmployees.Application.Features.Employee.Commands.CreateEmployee;
using ManageEmployees.Application.Features.Employee.Commands.UpdateEmployee;
using ManageEmployees.Application.Features.Employee.Queries.GetAllEmployees;
using ManageEmployees.Application.Features.Employee.Queries.GetEmployeesDetails;
using ManageEmployees.Domain;

namespace ManageEmployees.Application.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
            CreateMap<Employee, EmployeeDetailsDTO>();
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<UpdateEmployeeCommand, Employee>();
        }
    }
}
