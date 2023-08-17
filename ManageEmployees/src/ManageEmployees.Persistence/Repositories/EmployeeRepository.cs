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

        public async Task<List<Employee>> GetEmployeesWithDetailsAsync()
        {
            var employees = await _context.Employees
                .Include(e => e.EmploymentType)
                .Include(e => e.Department)
                .ToListAsync();
            return employees;
        }

        public async Task<List<Employee>> GetEmployeesWithDetailsByDepartmentAsync(int departmentId)
        {
            var employees = await _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .Include(e => e.EmploymentType)
                .Include(e => e.Department)
                .ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeWithDetailsAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.EmploymentType)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
            return employee;
        }
    }
}
