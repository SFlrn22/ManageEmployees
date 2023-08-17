using MediatR;

namespace ManageEmployees.Application.Features.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int EmploymentTypeId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
