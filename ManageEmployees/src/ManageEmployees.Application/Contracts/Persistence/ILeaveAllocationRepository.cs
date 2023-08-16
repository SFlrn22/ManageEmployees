using ManageEmployees.Domain;

namespace ManageEmployees.Application.Contracts
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync();
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync(string userId);
        Task<bool> AllocationExistsAsync(string userid, int leaveTypeId, int period);
        Task AddAllocationsAsync(List<LeaveAllocation> allocations);
        Task<List<LeaveAllocation>> GetUserAllocationsAsync(string userId, int leaveTypeId);
    }
}
