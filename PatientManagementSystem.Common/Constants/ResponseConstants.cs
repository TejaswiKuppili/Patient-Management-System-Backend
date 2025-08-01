using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public const int NotFound = (int)HttpStatusCode.NotFound;

        /// <summary>
        /// Represents the HTTP 500 Internal Server Error status code.
        /// </summary>
        public const int InternalServerError = (int)HttpStatusCode.InternalServerError;

        /// <summary>
        /// Represents the HTTP 200 Success status code.
        /// </summary>
        public const int Success = (int)HttpStatusCode.OK;

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
        public const string GenericErrorMessage = "An error occurred ";
        public const string SqlErrorMessage = "We’re experiencing some issues. Please try again in a moment !";
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
        public const int BadRequest = (int)HttpStatusCode.BadRequest;

        /// <summary>
        /// Bad Request: UnAuthorized status code
        /// </summary>
        public const int UnAuthorized = (int)HttpStatusCode.Unauthorized;


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

        /// <summary>
        /// Message indicating that patient details were fetched successfully.
        /// </summary>
        public const string PatientsFetchedMessage = "Patient details fetched successfully!!";

        /// <summary>
        /// Message indicating that a patient was added successfully.
        /// </summary>
        public const string PatientAddedMessage = "Patient added successfully";

        /// <summary>
        /// Message indicating that fetching patient details failed.
        /// </summary>
        public const string PatientsFetchFailedMessage= "Failed to fetch patient details";

        /// <summary>
        /// Message indicating that adding patient details failed.
        /// </summary>
        public const string PatientAddFailedMessage="Failed to add patient details";

        /// <summary>
        /// Message Indicating that adding vital details failed
        /// </summary>
        public const string VitalFailedMessage = "Failed to add Vital Data";

        /// <summary>
        /// Message indicating that Vital data has been added successfully
        /// </summary>
        public const string VitalSuccessMessage = "Added vital data successfully";

        /// <summary>
        /// Message  indicating that fetching vital details successfully.
        /// </summary>
        public const string VitalFetchedSuccessMessage = "Vitals fetched successfully";

        /// <summary>
        /// Message indicating that fetched appointment details successfully.
        /// </summary>
        public const string AppointmentsFetchedMessage = "Appointments retrieved successfully.";

        /// <summary>
        /// Message indicating that fetching appointment details failed.
        /// </summary>
        public const string AppointmentsFetchFailedMessage = "Failed to retrieve appointments.";

        /// <summary>
        /// Message indicating that an appointment was added successfully.
        /// </summary>
        public const string AppointmentAddedMessage = "Appointment added successfully.";

        /// <summary>
        /// Message indicating that adding an appointment failed.
        /// </summary>
        public const string AppointmentAddFailedMessage = "Failed to add appointment.";

        /// <summary>
        /// Message indicating that an appointment was updated successfully.
        /// </summary>
        public const string AppointmentUpdatedMessage = "Appointment updated successfully.";

        /// <summary>
        /// Message indicating that updating an appointment failed.
        /// </summary>
        public const string AppointmentUpdateFailedMessage = "Failed to update appointment.";

        /// <summary>
        /// Message indicating that an appointment was deleted successfully.
        /// </summary>
        public const string AppointmentDeletedMessage = "Appointment deleted successfully.";

        /// <summary>
        /// Message indicating that deleting an appointment failed.
        /// </summary>
        public const string AppointmentDeleteFailedMessage = "Failed to delete appointment.";

        ///<summary>
        ///Message indicating that Profile has been created succesfully.
        /// </summary>
        public const string ProfileCreatedSuccessMessage = "Profile created successfully.";

       
        /// <summary>Returned when profile is updated successfully.</summary>
        public const string ProfileUpdatedSuccessMessage = "Profile updated successfully.";

        /// <summary>Returned when profile is deleted successfully.</summary>
        public const string ProfileDeletedSuccessMessage = "Profile deleted successfully.";

        /// <summary>Returned when profile is fetched successfully.</summary>
        public const string ProfileFetchedSuccessMessage = "Profile retrieved successfully.";

        /// <summary>Returned when profile operation fails (generic).</summary>
        public const string ProfileOperationFailedMessage = "Profile operation failed.";

        /// <summary>Returned when a requested profile is not found in the system.</summary>
        public const string ProfileNotFoundMessage = "Profile not found.";
        public const string EmailExists = "This user already exists";

    }
}