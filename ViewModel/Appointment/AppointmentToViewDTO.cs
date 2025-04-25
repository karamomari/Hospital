namespace Hospital.ViewModel.Appointment
{
    public class AppointmentToViewDTO
    {
        public string Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string PatientFullName { get; set; }
        public string? Notes { get; set; }
    }
}
