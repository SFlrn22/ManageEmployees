using ManageEmployees.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequests
{
    public class LeaveRequestDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveTypeDTO LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime ReqeustDate { get; set; }
    }
}
