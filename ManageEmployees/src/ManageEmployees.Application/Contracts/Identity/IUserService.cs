using ManageEmployees.Application.Models.Identity;

namespace ManageEmployees.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<EmployeeAuth> GetEmployee(string userId);
        Task<List<EmployeeAuth>> GetEmployees();
    }
}
