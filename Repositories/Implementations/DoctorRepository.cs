
namespace Hospital.Repositories.Implementations
{
    public class DoctorRepository:IDoctorRepository
    {
        private readonly MvcHospitalcontext _context;

        public DoctorRepository(MvcHospitalcontext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
              return await _context.Doctors.Include(d=>d.User).ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(string id)
        {
            return await _context.Doctors.Where(d => d.Id == id).FirstAsync();
        }
        public async Task<Doctor?> GetByUserIdAsync(string id)
        {
            return await _context.Doctors.Where(d => d.UserId == id).FirstAsync();
        }
        public async Task<Doctor?> GetByUserIdWithDetalisAsync(string id)
        {
            return await _context.Doctors.Include(d=>d.User).Where(d => d.UserId == id).FirstAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
