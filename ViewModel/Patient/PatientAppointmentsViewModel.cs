namespace Hospital.ViewModel.Patient
{
    public class PatientAppointmentsViewModel
    {
        public List<AppointmentToViewDTO> UpcomingAppointments { get; set; } = new List<AppointmentToViewDTO>();
        public List<AppointmentToViewDTO> PastAppointments { get; set; } = new List<AppointmentToViewDTO>();
    }
}
