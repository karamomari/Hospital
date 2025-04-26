using AutoMapper;
using Hospital.Service.Interfaces;

namespace Hospital.Service.Implementations
{
    public class PatientService:IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly MvcHospitalcontext _context;
        private readonly IMapper _mapper;
        public PatientService(IPatientRepository patientRepository, IMapper mapper, MvcHospitalcontext context)
        {

            _patientRepository = patientRepository;
            _context = context;
            _mapper = mapper;

        }
        public Patient? GetBySSN(string ssn)
        {
            return _patientRepository.GetBySSN(ssn);
        }

        public async Task<List<PatientToViewDTO>> SearchPatientsAsync(string keyword)
        {
            var query = _context.Patients
                .Include(p => p.User)
                .Where(p => p.User.FirstName.Contains(keyword) || p.User.LastName.Contains(keyword) || p.SSN.Contains(keyword));

            var results = await query.ToListAsync();
            return _mapper.Map<List<PatientToViewDTO>>(results);
        }

    }
}
