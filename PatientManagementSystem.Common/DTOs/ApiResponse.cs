
namespace PatientManagementSystem.Common.DTOs
{
    /// <summary>
    /// provides a standardized format for API responses, indicating success or failure, including an optional message, holding the returned data, and providing an HTTP status code
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; } 
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; } 
    }

}
