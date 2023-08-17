using ManageEmployees.Domain;

namespace ManageEmployees.Application.Contracts.Persistence
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> GetEmployeeWithDetailsAsync(int id);
        Task<List<Employee>> GetEmployeesWithDetailsAsync();
        Task<List<Employee>> GetEmployeesWithDetailsByDepartmentAsync(int departmentId);
        Task<bool> IsEmployeeUnique(string firstName, string lastName, string email);
    }
}
