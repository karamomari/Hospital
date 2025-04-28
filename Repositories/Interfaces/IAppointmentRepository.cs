namespace Hospital.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<int> CountAsync();
        Task<Appointment?> GetByIdAsync(string id);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(string id);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(string doctorId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId);

        Task<List<Appointment>> GetUpcomingAppointmentsAsync();

        Task<List<Appointment>> GetUpcomingAppointmentsForPatientAsync(string patientId);
        Task<List<Appointment>> GetPastAppointmentsForPatientAsync(string patientId);
        Task<List<string>> GetUniqueDoctorsVisitedAsync(string patientId);


    }
}
