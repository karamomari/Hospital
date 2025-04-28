namespace Hospital.ViewModel.Patient
{
    public class PatientDashboardViewModel
    {

        public string FullName { get; set; }
        public int UpcomingAppointmentsCount { get; set; }
        public int PastAppointmentsCount { get; set; }
        public int UniqueDoctorsVisitedCount { get; set; }
        public int MedicalRecordsCount { get; set; }

        public List<PatientAppointmentViewModel> RecentAppointments { get; set; } = new List<PatientAppointmentViewModel>();
    }
}
