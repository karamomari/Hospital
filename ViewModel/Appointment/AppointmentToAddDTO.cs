namespace Hospital.ViewModel.Appointment
{
    public class AppointmentToAddDTO
    {

        [Required]
        public DateTime AppointmentDate { get; set; }

        public string? Notes { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        public string DoctorId { get; set; }
    }
}
