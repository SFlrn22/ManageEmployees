using ManageEmployees.Domain;

namespace ManageEmployees.Application.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
        Task<List<LeaveRequest>> GetLeaveRequestWithDetailsAsync();
        Task<List<LeaveRequest>> GetLeaveRequestWithDetailsAsyncByUser(string userId);
    }
}
