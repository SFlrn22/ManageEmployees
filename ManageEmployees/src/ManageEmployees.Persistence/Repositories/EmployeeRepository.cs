using ManageEmployees.Application.Contracts.Persistence;
using ManageEmployees.Domain;
using ManageEmployees.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DBContext context) : base(context)
        {

        }
        public async Task<bool> IsEmployeeUnique(string firstName, string lastName, string email)
        {
            return await _context.Employees.AnyAsync(e => e.FirstName == firstName &&
                e.LastName == lastName && e.Email == email) == false;
        }

        public Task<List<Employee>> GetEmployeesWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetEmployeesWithDetailsByDepartmentAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
