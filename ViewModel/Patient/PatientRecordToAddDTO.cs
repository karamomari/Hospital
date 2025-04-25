namespace Hospital.ViewModel.Patient
{
    public class PatientRecordToAddDTO
    {
        [Required]
        public string PatientId { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        [Required]
        public string Treatment { get; set; }

        public string Notes { get; set; }
    }
}
