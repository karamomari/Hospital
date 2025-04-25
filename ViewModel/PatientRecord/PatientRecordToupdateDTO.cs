namespace Hospital.ViewModel.PatientRecord
{
    public class PatientRecordToupdateDTO
    {
        public string Id { get; set; }
        public string PatientId { get; set; }


        public string? Diagnosis { get; set; }

        public string Treatment { get; set; }

        public string? Notes { get; set; }
    }
}
