using ManageEmployees.BlazorUI.Models.LeaveRequests;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Contracts
{
    public interface ILeaveRequestService
    {
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequests();
        Task<EmployeeLeaveRequestVM> GetUserLeaveRequests();
        Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest);
        Task<LeaveRequestVM> GetLeaveRequest(int id);
        Task DeleteLeaveRequest(int id);
        Task ApproveLeaveRequest(int id, bool approved);
    }
}
