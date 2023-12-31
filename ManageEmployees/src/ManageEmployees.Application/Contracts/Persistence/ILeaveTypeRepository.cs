﻿using ManageEmployees.Domain;

namespace ManageEmployees.Application.Contracts
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        Task<bool> IsLeaveTypeUnique(string name);
    }
}
