namespace Hospital.Models
{
    public class PatientRecord
    {
        public string Id { get; set; }

        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        public string? Notes { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
