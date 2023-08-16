namespace ManageEmployees.Application.Features.LeaveType.Queries.GetLeaveTypesDetails
{
    public class LeaveTypeDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
