
namespace AngularApp5.Server.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GenerateMessageBasedOnStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GenerateMessageBasedOnStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request has been made",
                401 => "You are not authorized",
                404 => "Not found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
