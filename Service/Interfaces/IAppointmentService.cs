namespace Hospital.Service.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentToViewDTO>> GetAllAppointmentsAsync();
        Task AddAppointmentAsync(AppointmentToAddDTO dto);
    }
}
