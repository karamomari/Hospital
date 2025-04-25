using AutoMapper;
using Hospital.Controllers;
using Hospital.Service.Interfaces;
using Hospital.ViewModel.PatientRecord;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Service.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IPatientRecordRepository _recordRepo;
        private readonly ILogger<AccountController> _logger;
        private readonly IDoctorRepository doctorRepository;
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IMapper _mapper;

        public DoctorService(IPatientRecordRepository recordRepo, ILogger<AccountController> logger, IDoctorRepository doctorRepository, 
            IMapper mapper, IAppointmentRepository appointmentRepo)
        {
            _recordRepo = recordRepo;
            _logger = logger;
            this.doctorRepository = doctorRepository;
            _mapper = mapper;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<List<PatientRecord>> GetRecordsForDoctorAsync(string userId)
        {
            var records = await _recordRepo.GetAllAsync();
            var doctor =await doctorRepository.GetByUserIdAsync(userId);
            return records.Where(r => r.DoctorId == doctor.Id).ToList();
        }

        public async Task<PatientRecord> GetOneRecordsForDoctorAsync(string RecordId)
        {
            var Record= await _recordRepo.GetByIdAsync(RecordId);
            return Record;
        }

        public async Task<List<Appointment>> CountAppointmentsByDoctorIdAsync(string userId)
        {
            var Appointments = await _appointmentRepo.GetAllAsync();
            var doctor = await doctorRepository.GetByUserIdAsync(userId);
            return Appointments.Where(r => r.DoctorId == doctor.Id).ToList();

        }

        public async Task<bool> AddPatientRecordAsync(PatientRecord record,string UserId)
        {
            try
            {
                var doctor= await doctorRepository.GetByUserIdAsync(UserId);
                record.DoctorId = doctor.Id;
                record.Id = Guid.NewGuid().ToString();
                record.RecordDate = DateTime.UtcNow;

                await _recordRepo.AddAsync(record);
                await _recordRepo.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
              _logger.LogError(ex, "Error while adding patient record : " + ex.Message);

                return false;
            }
        }

        public async Task UpdateRecordAsync(PatientRecordToupdateDTO updatedRecord)
        {
            var existingRecord = await _recordRepo.GetByIdAsync(updatedRecord.Id);

            if (existingRecord == null)
                throw new Exception("Record not found.");

            _mapper.Map(updatedRecord, existingRecord);
            existingRecord.RecordDate = DateTime.UtcNow;
            await _recordRepo.SaveChangesAsync();

        }


        public async Task<IEnumerable<Appointment>> MyAppointments(string userId)
        {
            var doctor=await doctorRepository.GetByUserIdAsync(userId);
            var appointments = await _appointmentRepo.GetAppointmentsByDoctorIdAsync(doctor.Id);
            return appointments;
        }

    }
}
