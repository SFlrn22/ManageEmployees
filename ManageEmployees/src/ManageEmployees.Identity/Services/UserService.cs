using ManageEmployees.Application.Contracts.Identity;
using ManageEmployees.Application.Models.Identity;
using ManageEmployees.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace ManageEmployees.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<EmployeeAuth> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            return new EmployeeAuth
            {
                Id = employee.Id,
                Email = employee.Email,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };
        }

        public async Task<List<EmployeeAuth>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(e => new EmployeeAuth
            {
                Id = e.Id,
                Email = e.Email,
                Firstname = e.FirstName,
                Lastname = e.LastName
            }).ToList();
        }
    }
}
