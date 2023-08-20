using ManageEmployees.Domain.Common;

namespace ManageEmployees.Domain
{
    public class LeaveAllocation : BaseEntity
    {
        public int NumberOfDays { get; set; }
        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
        public Employee? Employee { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
    }
}
