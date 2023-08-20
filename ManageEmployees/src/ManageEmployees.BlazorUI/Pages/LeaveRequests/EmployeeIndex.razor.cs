using global::Microsoft.AspNetCore.Components;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models.LeaveRequests;
using Microsoft.JSInterop;

namespace ManageEmployees.BlazorUI.Pages.LeaveRequests
{
    public partial class EmployeeIndex
    {
        [Inject] ILeaveRequestService _leaveRequestService { get; set; }
        [Inject] IJSRuntime js { get; set; }
        public EmployeeLeaveRequestVM Request { get; set; } = new();
        public string Message { get; set; } = string.Empty;

        protected async override Task OnInitializedAsync()
        {

            Request = await _leaveRequestService.GetUserLeaveRequests();
        }

        async Task CancelRequestAsync(int id)
        {
            var confirm = await js.InvokeAsync<bool>("confirm", "Do you want to cancel this request?");
            if (confirm)
            {
                var response = await _leaveRequestService.CancelLeaveRequest(id);
                if (response.Success)
                {
                    StateHasChanged();
                }
                else
                {
                    Message = response.Message;
                }
            }
        }
    }
}