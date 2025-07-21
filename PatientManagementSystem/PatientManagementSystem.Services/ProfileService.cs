
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystem.Services
{
    /// <summary>
    /// Provides methods to manage user profiles.
    /// </summary>
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        /// <summary>
        /// Retrieves a user profile by ApplicationUserId.
        /// </summary>
        public async Task<ProfileDto?> GetProfileByUserIdAsync(int userId)
        {
            Profile? profile = await profileRepository.GetByUserIdAsync(userId);
            if (profile == null)
            {
                return null;
            }

            return new ProfileDto
            {
                Id=profile.Id,
                City=profile.City,
                CityState=profile.CityState,
                Country=profile.Country,

                FirstName=profile.ApplicationUser.Username,
                ApplicationUserId = profile.ApplicationUserId,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                PhoneNumber = profile.PhoneNumber,
                Bio = profile.Bio,
                Address = profile.Address,
                ProfilePicture = profile.ProfilePicture 
            };
        }

        /// <summary>
        /// Creates a new user profile from the provided DTO.
        /// </summary>
        public async Task CreateProfileAsync(ProfileDto profileDto)
        {
            byte[]? pictureBytes = null;

            if (profileDto.ProfilePicture != null) // separate property for upload
            {
                using MemoryStream memoryStream = new();
                
                pictureBytes = memoryStream.ToArray();
            }

            Profile profile = new Profile
            {
                ApplicationUserId = profileDto.ApplicationUserId,
                DateOfBirth = profileDto.DateOfBirth,
                Gender = profileDto.Gender,
                PhoneNumber = profileDto.PhoneNumber,
                Bio = profileDto.Bio,
                Address = profileDto.Address,
                ProfilePicture = pictureBytes
            };

            await profileRepository.AddAsync(profile);
        }

        /// <summary>
        /// Updates an existing profile.
        /// </summary>
        public async Task<bool> UpdateProfileAsync( ProfileDto profileDto)
        {
            Profile? existingProfile = await profileRepository.GetByUserIdAsync(profileDto.ApplicationUserId);
            if (existingProfile == null)
            {
                return false;
            }

            existingProfile.DateOfBirth = profileDto.DateOfBirth;
            existingProfile.Gender = profileDto.Gender;
            existingProfile.PhoneNumber = profileDto.PhoneNumber;
            existingProfile.Bio = profileDto.Bio;
            existingProfile.Address = profileDto.Address;

            if (profileDto.ProfilePicture != null)
            {
                using MemoryStream memoryStream = new();
                
                existingProfile.ProfilePicture = memoryStream.ToArray();
            }

            await profileRepository.UpdateAsync(existingProfile);
            return true;
        }

        /// <summary>
        /// Deletes a profile by ApplicationUserId.
        /// </summary>
        public async Task<bool> DeleteProfileAsync(int userId)
        {
            Profile? existingProfile = await profileRepository.GetByUserIdAsync(userId);
            if (existingProfile == null)
            {
                return false;
            }

            await profileRepository.DeleteAsync(existingProfile);
            return true;
        }
    }
}
