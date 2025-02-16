using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GymSystem.BLL.Errors
{
    public class ApiResponse
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(int? statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        public ApiResponse(int? statusCode, string? message, object? data)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
            Data = data;
        }

        private string? GetDefaultMessageForStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                200 => "Success",
                201 => "Resource created",
                204 => "Resource deleted",
                400 => "Bad Request, You Have Made",
                401 => "You are not Authorized",
                403 => "You are forbidden",
                404 => "Resource Not Found",
                405 => "Method Not Allowed",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
