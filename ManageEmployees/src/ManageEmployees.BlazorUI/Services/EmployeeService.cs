using Blazored.LocalStorage;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Services
{
    public class EmployeeService : BaseHttpService, IEmployeeService
    {
        public EmployeeService(IClient client, ILocalStorageService localStorage) :
            base(client, localStorage)
        {

        }
    }
}
