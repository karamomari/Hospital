namespace Hospital.ViewModel.Appointment
{
    public class AppointmentToReturnDTO
    {
        public string Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; }

        public string? Notes { get; set; }

        public string PatientFullName { get; set; }

        public string DoctorFullName { get; set; }
    }
}
