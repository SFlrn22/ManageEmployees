using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models.LeaveRequests;
using ManageEmployees.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace ManageEmployees.BlazorUI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject]
        ILeaveTypeService _leaveTypeService { get; set; }
        [Inject]
        ILeaveRequestService _leaveRequestService { get; set; }
        [Inject]
        NavigationManager _navigation { get; set; }
        LeaveRequestVM Request { get; set; } = new LeaveRequestVM();
        List<LeaveTypeVM> leaveTypes { get; set; } = new List<LeaveTypeVM>();

        protected override async Task OnInitializedAsync()
        {
            leaveTypes = await _leaveTypeService.GetLeaveTypes();
        }

        private async Task HandleValidSubmit()
        {
            await _leaveRequestService.CreateLeaveRequest(Request);
            _navigation.NavigateTo("/leaverequests/");
        }
    }
}