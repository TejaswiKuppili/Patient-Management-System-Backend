//using PatientManagementSystem.Repository.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;

//namespace PatientManagementSystem.Services
//{
//    public class DoctorService : IDoctorService
//    {
//        private readonly IDoctorRepository DoctorRepository;

//        public DoctorService(IDoctorRepository doctorRepository)
//        {
//            DoctorRepository = doctorRepository;
//        }

//        public async Task<IEnumerable<Doctor>> GetDoctorsAsync()
//        {
//            try
//            {
//                return await DoctorRepository.GetAllAsync();
//            }
//            catch (Exception ex)
//            {
//                // Log exception
//                Console.WriteLine($"Error in GetDoctorsAsync: {ex.Message}");
//                return new List<Doctor>();
//            }
//        }

//        public async Task<Doctor> GetDoctorByIdAsync(int id)
//        {
//            try
//            {
//                return await DoctorRepository.GetByIdAsync(id);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error in GetDoctorByIdAsync: {ex.Message}");
//                return null;
//            }
//        }

//        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
//        {
//            try
//            {
//                await DoctorRepository.AddAsync(doctor);
//                return doctor;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error in CreateDoctorAsync: {ex.Message}");
//                return null;
//            }
//        }

//        public async Task<bool> UpdateDoctorAsync(int id, Doctor doctor)
//        {
//            try
//            {
//                if (id != doctor.Id || !await DoctorRepository.ExistsAsync(id))
//                {
//                    return false;
//                }

//                await DoctorRepository.UpdateAsync(doctor);
//                return true;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error in UpdateDoctorAsync: {ex.Message}");
//                return false;
//            }
//        }

//        public async Task<bool> DeleteDoctorAsync(int id)
//        {
//            try
//            {
//                Doctor doctor = await DoctorRepository.GetByIdAsync(id);
//                if (doctor == null)
//                {
//                    return false;
//                }

//                await DoctorRepository.DeleteAsync(doctor);
//                return true;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error in DeleteDoctorAsync: {ex.Message}");
//                return false;
//            }
//        }
//    }
//}
