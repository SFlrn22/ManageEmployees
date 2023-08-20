using Blazored.LocalStorage;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models.LeaveRequests;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        public LeaveRequestService(IClient client, ILocalStorageService localStorage) :
            base(client, localStorage)
        {

        }

        public Task ApproveLeaveRequest(int id, bool approved)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLeaveRequest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AdminLeaveRequestViewVM> GetAdminLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequestVM> GetLeaveRequest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeLeaveRequestVM> GetUserLeaveRequests()
        {
            throw new NotImplementedException();
        }
    }
}
