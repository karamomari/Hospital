
namespace Hospital.Models
{
    public class Appointment
    {
        public string Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } // مثلاً: Scheduled, Cancelled, Completed

        public string? Notes { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
