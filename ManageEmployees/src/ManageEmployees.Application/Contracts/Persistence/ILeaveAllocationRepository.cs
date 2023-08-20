using ManageEmployees.Domain;

namespace ManageEmployees.Application.Contracts
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync();
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsyncByUser(string userId);
        Task<bool> AllocationExistsAsync(string userid, int leaveTypeId, int period);
        Task AddAllocationsAsync(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId);
    }
}
