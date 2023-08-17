using MediatR;

namespace ManageEmployees.Application.Features.Employee.Queries.GetEmployeesDetails
{
    public record GetEmployeeDetailsQuery(int Id) : IRequest<EmployeeDetailsDTO>;
}
