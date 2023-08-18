using Newtonsoft.Json;
using System.Text;

namespace ManageEmployees.IntegrationTests.Helpers
{
    internal class HttpHelper
    {
        public static StringContent GetJsonHttpContent(object items)
        {
            return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
        }

        internal static class Urls
        {
            public const string EMPLOYEES = "/api/Employee";
            public const string LEAVETYPES = "/api/LeaveTypes";
            public const string LEAVEREQUESTS = "/api/LeaveRequests";
            public const string LEAVEALLOCATIONS = "/api/LeaveAllocation";
            public const string CANCELLEAVEREQUEST = "/api/LeaveRequests/CancelRequest";
            public const string UPDATELEAVEREQUEST = "/api/LeaveRequests/UpdateApproval";
        }
    }
}
