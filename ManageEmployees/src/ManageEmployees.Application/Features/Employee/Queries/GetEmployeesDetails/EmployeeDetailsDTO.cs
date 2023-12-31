﻿using ManageEmployees.Application.DTO;

namespace ManageEmployees.Application.Features.Employee.Queries.GetEmployeesDetails
{
    public class EmployeeDetailsDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public EmploymentTypeDTO EmploymentType { get; set; }
        public int EmploymentTypeId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public DepartmentDTO Department { get; set; }
        public int DepartmentId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
