

namespace Hospital.Repositories.Implementations
{
    public class PatientRecordRepository : IPatientRecordRepository
    {
        private readonly MvcHospitalcontext _context;

        public PatientRecordRepository(MvcHospitalcontext context)
        {
            _context = context;
        }

        public async Task<List<PatientRecord>> GetAllAsync()
        {
            return await _context.PatientRecords.Include(p=>p.Patient).ToListAsync();
        }

        public async Task<PatientRecord?> GetByIdAsync(string id)
        {
            return await _context.PatientRecords.FindAsync(id);
        }

        public async Task<PatientRecord?> GetByPatientIdAsync(string PatientId)
        {
            return await _context.PatientRecords.Include(p=>p.Patient).Where(p=>p.PatientId== PatientId).FirstOrDefaultAsync();
        }

        public async Task AddAsync(PatientRecord record)
        {
            await _context.PatientRecords.AddAsync(record);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
