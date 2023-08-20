using ManageEmployees.Application.Contracts.Identity;
using ManageEmployees.Application.Models.Identity;
using ManageEmployees.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ManageEmployees.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public string UserId
        {
            get => _contextAccessor.HttpContext?.User?
                .FindFirstValue("uid");
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
