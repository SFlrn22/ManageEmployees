using ManageEmployees.Application.Contracts;
using ManageEmployees.Domain;
using ManageEmployees.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(DBContext context) : base(context)
        {
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);
            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestWithDetailsAsync()
        {
            var leaveRequests = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestWithDetailsAsyncByUser(string userId)
        {
            var leaveRequests = await _context.LeaveRequests
                .Where(q => q.RequestingEmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }
    }
}
