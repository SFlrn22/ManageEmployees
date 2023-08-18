using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        public LeaveAllocationService(IClient client) : base(client)
        {

        }
    }
}
