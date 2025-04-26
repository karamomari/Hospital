namespace Hospital.Service.Interfaces
{
    public interface IReceptionService
    {
        Task<int> GetTotalPatientsAsync();
        Task<int> GetTotalAppointmentsAsync();
        Task<List<AppointmentToViewDTO>> GetUpcomingAppointmentsAsync();
    }
}
