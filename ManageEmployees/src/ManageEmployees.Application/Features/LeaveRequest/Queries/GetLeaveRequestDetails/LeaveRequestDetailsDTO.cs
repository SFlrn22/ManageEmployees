using ManageEmployees.Application.Features.Employee.Queries.GetAllEmployees;
using ManageEmployees.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class LeaveRequestDetailsDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EmployeeDTO? Employee { get; set; }
        public int RequestingEmployeeId { get; set; }
        public LeaveTypeDTO? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime ReqeustDate { get; set; }
        public string? RequestComments { get; set; }
        public DateTime? DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }

    }
}
