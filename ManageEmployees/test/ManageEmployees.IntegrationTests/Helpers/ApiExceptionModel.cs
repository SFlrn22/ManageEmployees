namespace ManageEmployees.IntegrationTests.Helpers
{
    public class ApiExceptionModel
    {
        public Dictionary<string, string[]> Errors { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
    }
}
