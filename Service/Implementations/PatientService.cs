using Hospital.Service.Interfaces;

namespace Hospital.Service.Implementations
{
    public class PatientService:IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {

            _patientRepository = patientRepository;

        }
        public Patient? GetBySSN(string ssn)
        {
            return _patientRepository.GetBySSN(ssn);
        }

    }
}
