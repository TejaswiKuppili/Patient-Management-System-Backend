using Microsoft.Extensions.Logging;
using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Common.Enums;
using PatientManagementSystem.Common.Helpers;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                Profile? profile = await profileRepository.GetByUserIdAsync(userId);
                if (profile == null)
                {
                    return ApiResponseHelper.Fail<ProfileDto?>(ResponseConstants.ProfileNotFoundMessage, ResponseConstants.NotFound);
                }

                string[] nameParts = profile.ApplicationUser.Username.Split(' ');

                var dto = new ProfileDto
                {
                    Id = profile.Id,
                    City = profile.City,
                    State = profile.CityState,
                    Country = profile.Country,
                    FirstName = nameParts.FirstOrDefault() ?? "",
                    LastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : "",
                    ApplicationUserId = profile.ApplicationUserId,
                    DateOfBirth = profile.DateOfBirth,
                    Gender = profile.Gender,
                    PhoneNumber = profile.PhoneNumber,
                    Bio = profile.Bio,
                    Address = profile.Address,
                    ProfilePicture = profile.ProfilePicture,
                    Email = profile.ApplicationUser.Email,
                    Date = profile.DateOfBirth?.ToString("yyyy-MM-dd")
                };


                dto.GenderOptions = Enum.GetValues(typeof(Gender))
                    .Cast<Gender>()
                    .ToDictionary(g => (int)g, g => g.ToString());

                return ApiResponseHelper.Success(dto, ResponseConstants.ProfileFetchedSuccessMessage);
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
                Profile profile = new Profile
                {
                    ApplicationUserId = profileDto.ApplicationUserId,
                    DateOfBirth = profileDto.DateOfBirth,
                    Gender = profileDto.Gender,
                    PhoneNumber = profileDto.PhoneNumber,
                    Bio = profileDto.Bio,
                    Address = profileDto.Address,
                    ProfilePicture = profileDto.ProfilePicture,
                    City = profileDto.City,
                    CityState = profileDto.State,
                    Country = profileDto.Country
                };

                await profileRepository.AddAsync(profile);

                profileDto.Id = profile.Id;

                return ApiResponseHelper.Success(profileDto, ResponseConstants.ProfileCreatedSuccessMessage);
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
                Profile? profile = await profileRepository.GetByUserIdAsync(profileDto.ApplicationUserId);
                if (profile == null)
                {
                    return await CreateProfileAsync(profileDto);
                }
                profile.ApplicationUser.Username = profileDto.FirstName + " " + profileDto.LastName;
                profile.DateOfBirth = profileDto.DateOfBirth;
                profile.Gender = profileDto.Gender;
                profile.PhoneNumber = profileDto.PhoneNumber;
                profile.Bio = profileDto.Bio;
                profile.Address = profileDto.Address;
                profile.City = profileDto.City;
                profile.CityState = profileDto.State;
                profile.Country = profileDto.Country;
                
                if (profileDto.ProfilePicture != null)
                {
                    profile.ProfilePicture = profileDto.ProfilePicture;
                }

                await profileRepository.UpdateAsync(profile);

                return ApiResponseHelper.Success(profileDto, ResponseConstants.ProfileUpdatedSuccessMessage);
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
                Profile? profile = await profileRepository.GetByUserIdAsync(userId);
                if (profile == null)
                {
                    return ApiResponseHelper.Fail<bool>(ResponseConstants.ProfileNotFoundMessage, ResponseConstants.NotFound);
                }

                await profileRepository.DeleteAsync(profile);

                return ApiResponseHelper.Success(true, ResponseConstants.ProfileDeletedSuccessMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting profile for userId {userId}", userId);
                return ApiResponseHelper.Fail<bool>($"{ResponseConstants.GenericErrorMessage}{ex.Message}", ResponseConstants.InternalServerError);
            }
        }
    }
}
