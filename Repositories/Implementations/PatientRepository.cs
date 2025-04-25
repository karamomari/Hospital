using Microsoft.EntityFrameworkCore;

namespace Hospital.Repositories.Implementations
{
    public class PatientRepository:IPatientRepository
    {
        private readonly MvcHospitalcontext _context;
        public PatientRepository(MvcHospitalcontext context)
        {
            _context = context;
        }
        public Patient? GetBySSN(string ssn)
        {
            return _context.Patients.Include(p=>p.User).Where(p => p.SSN == ssn).FirstOrDefault();
        }

    }
}
