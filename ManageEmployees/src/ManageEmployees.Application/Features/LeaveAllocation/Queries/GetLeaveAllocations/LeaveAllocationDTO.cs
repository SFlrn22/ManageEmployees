using ManageEmployees.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace ManageEmployees.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class LeaveAllocationDTO
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveTypeDTO LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
