namespace Hospital.Service.Interfaces
{
    public interface IPatientService
    {
        Patient? GetBySSN(string ssn);
        Task<PatientDashboardViewModel> Dashboard(string patientId);
        Task<PatientAppointmentsViewModel> MyAppointments(string patientId);
        Task<List<PatientToViewDTO>> SearchPatientsAsync(string keyword);
        Task<PatientMedicalRecordsPViewModel> MyMedicalRecords(string patientId);

        Task<List<AppointmentToViewDTO>> GetMyAppointmentsAsync(string patientId);
        Task<bool> BookAppointmentAsync(AppointmentToAddDTO appointmentDto);
        Task<List<PatientToViewDTO>> GetMyMedicalRecordsAsync(string patientId);
    }
}
