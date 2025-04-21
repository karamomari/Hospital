using System.ComponentModel.DataAnnotations;

namespace Hospital.ViewModel
{
    public class DiagnoseViewModel
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        [Required]
        public string Treatment { get; set; }

        public string? Notes { get; set; }
    }
}
