using ManageEmployees.Domain.Common;

namespace ManageEmployees.Domain
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
