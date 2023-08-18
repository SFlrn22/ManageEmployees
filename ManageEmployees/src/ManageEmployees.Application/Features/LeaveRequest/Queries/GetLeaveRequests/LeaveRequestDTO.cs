﻿using ManageEmployees.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequests
{
    public class LeaveRequestDTO
    {
        public int RequestingEmployeeId { get; set; }
        public LeaveTypeDTO LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Approved { get; set; }
    }
}