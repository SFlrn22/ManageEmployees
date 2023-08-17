using MediatR;

namespace ManageEmployees.Application.Features.Employee.Queries.GetAllEmployees
{
    public record GetEmployeesQuery : IRequest<List<EmployeeDTO>>;
}
