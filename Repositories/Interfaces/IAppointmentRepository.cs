namespace Hospital.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(string id);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(string id);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(string doctorId);

    }
}
