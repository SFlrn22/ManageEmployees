using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId);
    }
}
