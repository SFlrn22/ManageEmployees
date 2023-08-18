﻿using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        public LeaveRequestService(IClient client) : base(client)
        {

        }
    }
}