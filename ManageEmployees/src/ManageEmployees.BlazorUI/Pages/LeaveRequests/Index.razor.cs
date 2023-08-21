using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace ManageEmployees.BlazorUI.Pages.LeaveRequests
{
    public partial class Index
    {
        [Inject] ILeaveRequestService _leaveRequestService { get; set; }
        [Inject] NavigationManager _navigation { get; set; }
        public AdminLeaveRequestViewVM Request { get; set; } = new();
        protected async override Task OnInitializedAsync()
        {
            Request = await _leaveRequestService.GetAdminLeaveRequests();
        }
        void GoToDetails(int id)
        {
            _navigation.NavigateTo($"/leaverequests/details/{id}");
        }
    }
}