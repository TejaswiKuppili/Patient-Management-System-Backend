using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.DTOs;

namespace PatientManagementSystem.Common.Helpers
{
    /// <summary>
    /// Provides static helper methods for creating standardized API responses.
    /// </summary>
    public static class ApiResponseHelper
    {
        /// <summary>
        /// Creates a successful API response with the provided data.
        /// </summary>
        /// <typeparam name="T">The type of the data being returned.</typeparam>
        /// <param name="data">The data to include in the response.</param>
        /// <param name="statusCode">The HTTP status code for the response (defaults to 200 Success).</param>
        /// <returns>A new ApiResponse instance indicating success.</returns>
        public static ApiResponse<T> Success<T>(T data, string message)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                StatusCode = ResponseConstants.Success,
                Message= message
            };
        }
        ///// <summary>
        ///// Creates a successful API response with message
        ///// </summary>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public static ApiResponse<string> Success(string message)
        //{
        //    return new ApiResponse<string>
        //    {
        //        Success = true,
        //        StatusCode = ResponseConstants.Success,
        //        Message = message
        //    };
        //}

        /// <summary>
        /// Creates a failed API response with a specified message and status code.
        /// </summary>
        /// <typeparam name="T">The type of the data that would have been returned (can be default).</typeparam>
        /// <param name="message">The error message to include in the response.</param>
        /// <param name="statusCode">The HTTP status code for the response (e.g., 400, 404, 500).</param>
        /// <returns>A new ApiResponse instance indicating failure.</returns>
        public static ApiResponse<T> Fail<T>(string message, int statusCode)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                StatusCode = statusCode
            };
        }


    }
}