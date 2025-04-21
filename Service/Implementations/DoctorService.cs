using Hospital.Controllers;
using Hospital.Service.Interfaces;

namespace Hospital.Service.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IPatientRecordRepository _recordRepo;
        private readonly ILogger<AccountController> _logger;
        public DoctorService(IPatientRecordRepository recordRepo, ILogger<AccountController> logger)
        {
            _recordRepo = recordRepo;
            _logger = logger;
        }

        public async Task<List<PatientRecord>> GetRecordsForDoctorAsync(string doctorId)
        {
            var records = await _recordRepo.GetAllAsync();
            return records.Where(r => r.DoctorId == doctorId).ToList();
        }

        public async Task<PatientRecord> GetOneRecordsForDoctorAsync(string patientId)
        {
            var Record= await _recordRepo.GetByPatientIdAsync(patientId);
            return Record;
        }


        public async Task<bool> AddPatientRecordAsync(PatientRecord record)
        {
            try
            {
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

        public async Task UpdateRecordAsync(PatientRecord updatedRecord)
        {
            var existingRecord = await _recordRepo.GetByIdAsync(updatedRecord.Id);

            if (existingRecord == null)
                throw new Exception("Record not found.");

            bool isModified = false;

            if (!string.IsNullOrEmpty(existingRecord.Diagnosis) && existingRecord.Diagnosis != updatedRecord.Diagnosis)
            {
                existingRecord.Diagnosis = updatedRecord.Diagnosis;
                isModified = true;
            }

            // فحص الـ null أو القيمة الفارغة 
            if (existingRecord.Treatment != updatedRecord.Treatment)
            {
                existingRecord.Treatment = updatedRecord.Treatment;
                isModified = true;
            }

            // فحص الـ null أو القيمة الفارغة
            if (!string.IsNullOrEmpty(existingRecord.Notes) && existingRecord.Notes != updatedRecord.Notes)
            {
                existingRecord.Notes = updatedRecord.Notes;
                isModified = true;
            }

            if (isModified)
            {
                existingRecord.RecordDate = DateTime.UtcNow;
                await _recordRepo.SaveChangesAsync();
            }
        }


    }
}
