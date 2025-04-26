namespace Hospital.Repositories.Implementations
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly MvcHospitalcontext _context;

        public AppointmentRepository(MvcHospitalcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(string id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Appointment appointment)
        {
            appointment.Id = Guid.NewGuid().ToString();
            appointment.Status = "Scheduled";
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Appointments.CountAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(string doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient).ThenInclude(u=>u.User)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }


        public async Task<List<Appointment>> GetUpcomingAppointmentsAsync()
        {
            return await _context.Appointments
                //.Where(a => a.AppointmentDate > DateTime.Now && a.Status == "Scheduled")
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }




    }


}
