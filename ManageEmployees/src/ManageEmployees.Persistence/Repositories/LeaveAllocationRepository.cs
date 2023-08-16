using ManageEmployees.Application.Contracts;
using ManageEmployees.Domain;
using ManageEmployees.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(DBContext context) : base(context)
        {
        }

        public async Task AddAllocationsAsync(List<LeaveAllocation> allocations)
        {
            await _context.AddRangeAsync(allocations);
        }

        public async Task<bool> AllocationExistsAsync(string userid, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userid
                    && q.LeaveTypeId == leaveTypeId
                    && q.Period == period);
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id)
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);
            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync()
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync(string userId)
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Where(q => q.EmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetUserAllocationsAsync(string userId, int leaveTypeId)
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Where(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }
    }
}
