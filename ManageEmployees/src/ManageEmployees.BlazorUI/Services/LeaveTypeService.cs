using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        public LeaveTypeService(IClient client) : base(client)
        {

        }
    }
}
