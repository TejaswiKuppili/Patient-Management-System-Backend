namespace PatientManagementSystem.Common.Constants
{
    /// <summary>
    /// Defines constant integer values for common HTTP status codes and string messages for API responses.
    /// </summary>
    public static class ResponseConstants
    {
        /// <summary>
        /// Represents the HTTP 404 Not Found status code.
        /// </summary>
        public const int NotFound = 404;

        /// <summary>
        /// Represents the HTTP 500 Internal Server Error status code.
        /// </summary>
        public const int InternalServerError = 500;

        /// <summary>
        /// Represents the HTTP 200 Success status code.
        /// </summary>
        public const int Success = 200;

        /// <summary>
        /// Message indicating that User account has been created 
        /// </summary>
        public const string CreatedUserMessage = "Account successfully created.";

        /// <summary>
        /// Message indicating that User Role has been updated
        /// </summary>
        public const string RoleUpdatedMessage = "User role updated successfully.";

        /// <summary>
        /// Message indicating that user details were fetched successfully.
        /// </summary>
        public const string UserFetchedMessage = "User details fetched successfully";

        /// <summary>
        /// Message indicating that no users were found.
        /// </summary>
        public const string NoUsersFoundMessage = "No users found.";

        /// <summary>
        /// A generic error message prefix.
        /// </summary>
        public const string GenericErrorMessage = "An error occurred: ";

        /// <summary>
        /// Bad Request: Message for inavlid request body
        /// </summary>
        public const string InvalidRequestBody = "Invalid request body.";

        /// <summary>
        /// Bad Request: Message for missing user details
        /// </summary>
        public const string MissingUserDetails = "Missing user details.";
        
        /// <summary>
        /// Bad Request
        /// </summary>
        public const int BadRequest = 400;

        /// <summary>
        /// Bad Request: UnAuthorized status code
        /// </summary>
        public const int UnAuthorized = 401;


        /// <summary>
        /// Message indicating a successful login attempt.
        /// </summary>
        public const string LoginSuccess = "Login successful.";

        /// <summary>
        /// Message indicating that provided login credentials (email or password) are incorrect.
        /// </summary>
        public const string InvalidCredentials = "Invalid email or password.";

        /// <summary>
        /// Message indicating that the user does not have the necessary authentication or authorization to access a resource.
        /// </summary>
        public const string UnauthorizedAccess = "Unauthorized access.";
    }
}