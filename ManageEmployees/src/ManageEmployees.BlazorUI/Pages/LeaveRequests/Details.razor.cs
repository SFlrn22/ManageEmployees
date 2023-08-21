using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace ManageEmployees.BlazorUI.Pages.LeaveRequests
{
    public partial class Details
    {
        [Inject] ILeaveRequestService _leaveRequestService { get; set; }
        [Inject] NavigationManager _navigation { get; set; }
        [Parameter] public int id { get; set; }

        string ClassName = string.Empty;
        string HeadingText = string.Empty;

        public LeaveRequestVM Request { get; set; } = new LeaveRequestVM();

        protected override async Task OnParametersSetAsync()
        {
            Request = await _leaveRequestService.GetLeaveRequest(id);
        }

        protected override async Task OnInitializedAsync()
        {
            if (Request.Approved is true)
            {
                ClassName = "success";
                HeadingText = "Approved";
            }
            else if (Request.Approved is false)
            {
                ClassName = "danger";
                HeadingText = "Rejected";
            }
            else
            {
                ClassName = "warning";
                HeadingText = "Pending Approval";
            }
        }
        public async Task ChangeApproval(bool approvalStatus)
        {
            await _leaveRequestService.ApproveLeaveRequest(id, approvalStatus);
            _navigation.NavigateTo("/leaverequests/");
        }
    }
}