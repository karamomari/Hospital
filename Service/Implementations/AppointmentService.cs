using AutoMapper;
using Hospital.Service.Interfaces;

namespace Hospital.Service.Implementations
{
    public class AppointmentService:IAppointmentService
    {
        private readonly MvcHospitalcontext _context;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(MvcHospitalcontext context, IMapper mapper,IAppointmentRepository appointmentRepository)
        {
            _context = context;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<AppointmentToViewDTO>> GetAllAppointmentsAsync()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient).ThenInclude(a=>a.User)
                .Include(a => a.Doctor).ThenInclude(a=>a.User)
                .ToListAsync();

            return _mapper.Map<List<AppointmentToViewDTO>>(appointments);
        }

        public async Task AddAppointmentAsync(AppointmentToAddDTO dto)
        {
            var appointment = _mapper.Map<Appointment>(dto);
            await _appointmentRepository.AddAsync(appointment);
        }
    }
}
