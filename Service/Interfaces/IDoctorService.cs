namespace Hospital.Service.Interfaces
{
    public interface IDoctorService
    {
        Task<List<PatientRecord>> GetRecordsForDoctorAsync(string doctorId);
        Task<PatientRecord> GetOneRecordsForDoctorAsync(string patientId);
        Task<bool> AddPatientRecordAsync(PatientRecord record);
        Task UpdateRecordAsync(PatientRecord updatedRecord);

    }
}
