﻿namespace ManageEmployees.Application.Features.Employee.Queries.GetAllEmployees
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
    }
}
