using Hospital.ViewModel.PatientRecord;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Service.Interfaces
{
    public interface IDoctorService
    {
        Task<List<PatientRecord>> GetRecordsForDoctorAsync(string userId);
        Task<List<Appointment>> CountAppointmentsByDoctorIdAsync(string userId);
        Task<PatientRecord> GetOneRecordsForDoctorAsync(string RecordId);
        Task<bool> AddPatientRecordAsync(PatientRecord record, string UserId);
        Task UpdateRecordAsync(PatientRecordToupdateDTO updatedRecord);

       Task<IEnumerable<Appointment>> MyAppointments(string userId);


    }
}
