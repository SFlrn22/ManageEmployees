using MediatR;

namespace ManageEmployees.Application.Features.Employee.Queries.GetEmployeesDetails
{
    public class GetEmployeeDetailsQuery : IRequest<EmployeeDetailsDTO>
    {
        public int Id { get; set; }
    }
}
