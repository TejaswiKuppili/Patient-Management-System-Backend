
//using PatientManagementSystem.Common.DTOs;
//using PatientManagementSystem.Data.Entities;
//using PatientManagementSystem.Repository.Interfaces;
//using PatientManagementSystem.Services.Interfaces;

//namespace PatientManagementSystem.Services
//{
//    /// <summary>
//    /// Provides methods to manage user profiles.
//    /// </summary>
//    public class ProfileService : IProfileService
//    {
//        private readonly IProfileRepository profileRepository;

//        public ProfileService(IProfileRepository profileRepository)
//        {
//            this.profileRepository = profileRepository;
//        }

//        /// <summary>
//        /// Retrieves a user profile by ApplicationUserId.
//        /// </summary>
//        public async Task<ProfileDto?> GetProfileByUserIdAsync(int userId)
//        {
//            Profile? profile = await profileRepository.GetByUserIdAsync(userId);
//            if (profile == null)
//            {
//                return null;
//            }

//            return new ProfileDto
//            {
//                Id=profile.Id,
//                City=profile.City,
//                CityState=profile.CityState,
//                Country=profile.Country,

//                FirstName=profile.ApplicationUser.Username,
//                ApplicationUserId = profile.ApplicationUserId,
//                DateOfBirth = profile.DateOfBirth,
//                Gender = profile.Gender,
//                PhoneNumber = profile.PhoneNumber,
//                Bio = profile.Bio,
//                Address = profile.Address,
//                ProfilePicture = profile.ProfilePicture 
//            };
//        }

//        /// <summary>
//        /// Creates a new user profile from the provided DTO.
//        /// </summary>
//        public async Task CreateProfileAsync(ProfileDto profileDto)
//        {
//            byte[]? pictureBytes = null;

//            if (profileDto.ProfilePicture != null) // separate property for upload
//            {
//                using MemoryStream memoryStream = new();

//                pictureBytes = memoryStream.ToArray();
//            }

//            Profile profile = new Profile
//            {
//                ApplicationUserId = profileDto.ApplicationUserId,
//                DateOfBirth = profileDto.DateOfBirth,
//                Gender = profileDto.Gender,
//                PhoneNumber = profileDto.PhoneNumber,
//                Bio = profileDto.Bio,
//                Address = profileDto.Address,
//                ProfilePicture = pictureBytes
//            };

//            await profileRepository.AddAsync(profile);
//        }

//        /// <summary>
//        /// Updates an existing profile.
//        /// </summary>
//        public async Task<bool> UpdateProfileAsync( ProfileDto profileDto)
//        {
//            Profile? existingProfile = await profileRepository.GetByUserIdAsync(profileDto.ApplicationUserId);
//            if (existingProfile == null)
//            {
//                return false;
//            }

//            existingProfile.DateOfBirth = profileDto.DateOfBirth;
//            existingProfile.Gender = profileDto.Gender;
//            existingProfile.PhoneNumber = profileDto.PhoneNumber;
//            existingProfile.Bio = profileDto.Bio;
//            existingProfile.Address = profileDto.Address;

//            if (profileDto.ProfilePicture != null)
//            {
//                using MemoryStream memoryStream = new();

//                existingProfile.ProfilePicture = memoryStream.ToArray();
//            }

//            await profileRepository.UpdateAsync(existingProfile);
//            return true;
//        }

//        /// <summary>
//        /// Deletes a profile by ApplicationUserId.
//        /// </summary>
//        public async Task<bool> DeleteProfileAsync(int userId)
//        {
//            Profile? existingProfile = await profileRepository.GetByUserIdAsync(userId);
//            if (existingProfile == null)
//            {
//                return false;
//            }

//            await profileRepository.DeleteAsync(existingProfile);
//            return true;
//        }
//    }
//}


using Microsoft.Extensions.Logging;
using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Common.Helpers;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystem.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;
        private readonly ILogger<ProfileService> logger;

        public ProfileService(IProfileRepository profileRepository, ILogger<ProfileService> logger)
        {
            this.profileRepository = profileRepository;
            this.logger = logger;
        }

        public async Task<ApiResponse<ProfileDto?>> GetProfileByUserIdAsync(int userId)
        {
            try
            {
                var profile = await profileRepository.GetByUserIdAsync(userId);
                if (profile == null)
                {
                    return ApiResponseHelper.Fail<ProfileDto?>("Profile not found.", ResponseConstants.NotFound);
                }

                var dto = new ProfileDto
                {
                    Id = profile.Id,
                    City = profile.City,
                    CityState = profile.CityState,
                    Country = profile.Country,
                    FirstName = profile.ApplicationUser?.Username,
                    ApplicationUserId = profile.ApplicationUserId,
                    DateOfBirth = profile.DateOfBirth,
                    Gender = profile.Gender,
                    PhoneNumber = profile.PhoneNumber,
                    Bio = profile.Bio,
                    Address = profile.Address,
                    ProfilePicture = profile.ProfilePicture
                };

                return ApiResponseHelper.Success(dto, "Profile fetched successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching profile for userId {userId}", userId);
                return ApiResponseHelper.Fail<ProfileDto?>($"{ResponseConstants.GenericErrorMessage}{ex.Message}", ResponseConstants.InternalServerError);
            }
        }

        public async Task<ApiResponse<ProfileDto?>> CreateProfileAsync(ProfileDto profileDto)
        {
            try
            {
                var profile = new Profile
                {
                    ApplicationUserId = profileDto.ApplicationUserId,
                    DateOfBirth = profileDto.DateOfBirth,
                    Gender = profileDto.Gender,
                    PhoneNumber = profileDto.PhoneNumber,
                    Bio = profileDto.Bio,
                    Address = profileDto.Address,
                    ProfilePicture = profileDto.ProfilePicture,
                    City = profileDto.City,
                    CityState = profileDto.CityState,
                    Country = profileDto.Country
                };

                await profileRepository.AddAsync(profile);

                profileDto.Id = profile.Id;

                return ApiResponseHelper.Success(profileDto, "Profile created successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating profile for userId {userId}", profileDto.ApplicationUserId);
                return ApiResponseHelper.Fail<ProfileDto?>($"{ResponseConstants.GenericErrorMessage}{ex.Message}", ResponseConstants.InternalServerError);
            }
        }

        public async Task<ApiResponse<ProfileDto?>> UpdateProfileAsync(ProfileDto profileDto)
        {
            try
            {
                var profile = await profileRepository.GetByUserIdAsync(profileDto.ApplicationUserId);
                if (profile == null)
                {
                    return await CreateProfileAsync(profileDto);
                }

                profile.DateOfBirth = profileDto.DateOfBirth;
                profile.Gender = profileDto.Gender;
                profile.PhoneNumber = profileDto.PhoneNumber;
                profile.Bio = profileDto.Bio;
                profile.Address = profileDto.Address;
                profile.City = profileDto.City;
                profile.CityState = profileDto.CityState;
                profile.Country = profileDto.Country;

                if (profileDto.ProfilePicture != null)
                {
                    profile.ProfilePicture = profileDto.ProfilePicture;
                }

                await profileRepository.UpdateAsync(profile);

                return ApiResponseHelper.Success(profileDto, "Profile updated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating profile for userId {userId}", profileDto.ApplicationUserId);
                return ApiResponseHelper.Fail<ProfileDto?>($"{ResponseConstants.GenericErrorMessage}{ex.Message}", ResponseConstants.InternalServerError);
            }
        }

        public async Task<ApiResponse<bool>> DeleteProfileAsync(int userId)
        {
            try
            {
                var profile = await profileRepository.GetByUserIdAsync(userId);
                if (profile == null)
                {
                    return ApiResponseHelper.Fail<bool>("Profile not found.", ResponseConstants.NotFound);
                }

                await profileRepository.DeleteAsync(profile);

                return ApiResponseHelper.Success(true, "Profile deleted successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting profile for userId {userId}", userId);
                return ApiResponseHelper.Fail<bool>($"{ResponseConstants.GenericErrorMessage}{ex.Message}", ResponseConstants.InternalServerError);
            }
        }
    }
}
