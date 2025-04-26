namespace Hospital.ViewModel.Reception
{
    public class ReceptionDashboardViewModel
    {
        public int TotalAppointments { get; set; }
        public int TotalPatients { get; set; }
        public List<AppointmentToViewDTO> UpcomingAppointments { get; set; }
    }
}
