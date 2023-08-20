using Blazored.LocalStorage;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        public LeaveAllocationService(IClient client, ILocalStorageService localStorage) :
            base(client, localStorage)
        {

        }

        public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<Guid>();
                CreateLeaveAllocationCommand createLeaveAllocation = new() { LeaveTypeId = leaveTypeId };
                await _client.LeaveAllocationPOSTAsync(createLeaveAllocation);
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
