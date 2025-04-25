using Hospital.Models;

namespace Hospital.Repositories.Interfaces
{
    public interface IPatientRecordRepository
    {

        Task<List<PatientRecord>> GetAllAsync();
        Task<PatientRecord?> GetByIdAsync(string RecordId);
        Task<PatientRecord?> GetByPatientIdAsync(string PatientId);
        Task AddAsync(PatientRecord record);
        Task SaveChangesAsync();
    }
}
