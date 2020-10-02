namespace API.Errors
{
    public class APIResponse
    {
        public APIResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode) => statusCode switch
        {
            400 => "You've made a bad request",
            401 => "You're not authorized to access this section",
            404 => "Resource not found",
            500 => "Server error. Check the stacktrace",
            _ => null
        };
    }
}