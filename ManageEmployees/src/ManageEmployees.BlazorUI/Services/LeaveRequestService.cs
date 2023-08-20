using AutoMapper;
using Blazored.LocalStorage;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models.LeaveAllocations;
using ManageEmployees.BlazorUI.Models.LeaveRequests;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly IMapper _mapper;
        public LeaveRequestService(IClient client, ILocalStorageService localStorage, IMapper mapper) :
            base(client, localStorage)
        {
            _mapper = mapper;
        }

        public async Task ApproveLeaveRequest(int id, bool approved)
        {
            try
            {
                var request = new ChangeLeaveRequestApprovalCommand { Approved = approved, Id = id };
                await _client.UpdateApprovalAsync(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Response<Guid>> CancelLeaveRequest(int id)
        {
            try
            {
                var response = new Response<Guid>();
                var request = new CancelLeaveRequestCommand { Id = id };
                await _client.CancelRequestAsync(request);
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest)
        {
            try
            {
                var response = new Response<Guid>();
                CreateLeaveRequestCommand createLeaveRequest = _mapper.Map<CreateLeaveRequestCommand>(leaveRequest);

                await _client.LeaveRequestsPOSTAsync(createLeaveRequest);
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task DeleteLeaveRequest(int id)
        {
            try
            {
                await _client.LeaveRequestsDELETEAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequests()
        {
            var leaveRequests = await _client.LeaveRequestsAllAsync(isLoggedInUser: false);

            var request = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(r => r.Approved == true),
                RejectedRequests = leaveRequests.Count(r => r.Approved == false),
                PendingRequests = leaveRequests.Count(r => r.Approved == null),
                LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };

            return request;
        }

        public async Task<LeaveRequestVM> GetLeaveRequest(int id)
        {
            var leaveRequest = await _client.LeaveRequestsGETAsync(id);
            return _mapper.Map<LeaveRequestVM>(leaveRequest);
        }

        public async Task<EmployeeLeaveRequestVM> GetUserLeaveRequests()
        {
            var leaveRequests = await _client.LeaveRequestsAllAsync(isLoggedInUser: true);
            var allocations = await _client.LeaveAllocationAllAsync(isLoggedInUser: true);
            var model = new EmployeeLeaveRequestVM
            {
                LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations),
                LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };

            return model;
        }
    }
}
