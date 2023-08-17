using ManageEmployees.Domain;

namespace ManageEmployees.Application.Contracts
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync();
        Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsyncByUser(int userId);
        Task<bool> AllocationExistsAsync(int userid, int leaveTypeId, int period);
        Task AddAllocationsAsync(List<LeaveAllocation> allocations);
        Task<List<LeaveAllocation>> GetUserAllocationsAsync(int userId, int leaveTypeId);
    }
}
