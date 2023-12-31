using Blazored.Toast.Services;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace ManageEmployees.BlazorUI.Pages.LeaveTypes
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILeaveTypeService _leaveTypeService { get; set; }
        [Inject]
        public ILeaveAllocationService _leaveAllocationService { get; set; }
        [Inject]
        IToastService _toastService { get; set; }
        public List<LeaveTypeVM> LeaveTypes { get; private set; }
        public string Message { get; set; } = string.Empty;
        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create/");
        }
        protected void AllocateLeaveType(int id)
        {
            _leaveAllocationService.CreateLeaveAllocations(id);
        }
        protected void EditLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
        }
        protected void DetailsLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/details/{id}");
        }
        protected async Task DeleteLeaveType(int id)
        {
            var response = await _leaveTypeService.DeleteLeaveType(id);
            if (response.Success)
            {
                _toastService.ShowSuccess("Leave Type deleted");
                await OnInitializedAsync();
            }
            else
            {
                Message = response.Message;
            }
        }
        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await _leaveTypeService.GetLeaveTypes();
        }
    }
}