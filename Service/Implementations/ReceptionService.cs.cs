using AutoMapper;
using Hospital.Service.Interfaces;

namespace Hospital.Service.Implementations
{
    public class ReceptionService:IReceptionService
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IPatientRepository _patientRepo;
        private readonly IMapper _mapper;

        public ReceptionService(IAppointmentRepository appointmentRepo, IPatientRepository patientRepo, IMapper mapper)
        {
            _appointmentRepo = appointmentRepo;
            _patientRepo = patientRepo;
            _mapper = mapper;
        }

        public async Task<int> GetTotalPatientsAsync()
        {
            return await _patientRepo.CountAsync();
        }

        public async Task<int> GetTotalAppointmentsAsync()
        {
            return await _appointmentRepo.CountAsync();
        }

        public async Task<List<AppointmentToViewDTO>> GetUpcomingAppointmentsAsync()
        {
            var appointments = await _appointmentRepo.GetUpcomingAppointmentsAsync();
            return _mapper.Map<List<AppointmentToViewDTO>>(appointments);
        }
    }
}
