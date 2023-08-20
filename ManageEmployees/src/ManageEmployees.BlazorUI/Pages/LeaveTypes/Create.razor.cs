using Blazored.Toast.Services;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace ManageEmployees.BlazorUI.Pages.LeaveTypes
{
    public partial class Create
    {
        [Inject]
        NavigationManager _navManager { get; set; }
        [Inject]
        ILeaveTypeService _client { get; set; }
        [Inject]
        IToastService _toastService { get; set; }
        public string Message { get; private set; }

        LeaveTypeVM leaveType = new LeaveTypeVM();
        async Task CreateLeaveType()
        {
            var response = await _client.CreateLeaveType(leaveType);
            if (response.Success)
            {
                _toastService.ShowSuccess("Leave Type created");
                _navManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}