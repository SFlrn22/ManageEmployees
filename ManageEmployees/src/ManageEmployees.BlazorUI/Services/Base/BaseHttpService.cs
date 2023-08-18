namespace ManageEmployees.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;
        public BaseHttpService(IClient client)
        {
            _client = client;
        }
        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException exception)
        {
            switch (exception.StatusCode)
            {
                case 400:
                    return new Response<Guid>()
                    {
                        Message = "Invalid data was submitted",
                        ValidationErrors = exception.Response,
                        Success = false
                    };
                case 404:
                    return new Response<Guid>()
                    {
                        Message = "The record was not found",
                        Success = false
                    };
                default:
                    return new Response<Guid>()
                    {
                        Message = "Something went wrong",
                        Success = false
                    };
            }
        }
    }
}
