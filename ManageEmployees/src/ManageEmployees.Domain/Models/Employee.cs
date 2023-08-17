using ManageEmployees.Domain.Common;
using ManageEmployees.Domain.Enums;
using ManageEmployees.Domain.Models;

namespace ManageEmployees.Domain
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public EmploymentType EmploymentType { get; set; }
        public EmploymentTypesEnum EmploymentTypeId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
