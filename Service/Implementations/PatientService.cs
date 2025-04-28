using AutoMapper;
using Hospital.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Hospital.Service.Implementations
{
    public class PatientService:IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly MvcHospitalcontext _context;
        private readonly IPatientRecordRepository _patientRecordRepository;
        private readonly IMapper _mapper;


        public PatientService(IPatientRepository patientRepository, IMapper mapper, MvcHospitalcontext context, 
            IAppointmentRepository appointmentRepository, IPatientRecordRepository patientRecordRepository)
        {

            _patientRepository = patientRepository;
            _context = context;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _patientRecordRepository = patientRecordRepository;

        }


 
        public async Task<PatientDashboardViewModel> Dashboard(string patientId)
        {
            var upcomingAppointments = await _appointmentRepository.GetUpcomingAppointmentsForPatientAsync(patientId);
            var pastAppointments = await _appointmentRepository.GetPastAppointmentsForPatientAsync(patientId);
            var doctorsVisited = await _appointmentRepository.GetUniqueDoctorsVisitedAsync(patientId);
            var medicalRecordsCount = await _patientRecordRepository.GetMedicalRecordsCountAsync(patientId);

            var recentAppointments = pastAppointments
                .OrderByDescending(a => a.AppointmentDate)
                .Take(5)
                .Select(a => new PatientAppointmentViewModel
                {
                    AppointmentId = a.Id,
                    DoctorName = a.Doctor.Email ?? "Unknown",
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status
                }).ToList();

            var model = new PatientDashboardViewModel
            {
                UpcomingAppointmentsCount = upcomingAppointments.Count,
                PastAppointmentsCount = pastAppointments.Count,
                UniqueDoctorsVisitedCount = doctorsVisited.Count,
                MedicalRecordsCount = medicalRecordsCount,
                RecentAppointments = recentAppointments
            };
            return model;

        }
      
        
        public Patient? GetBySSN(string ssn)
        {
            return _patientRepository.GetBySSN(ssn);
        }

        public async Task<PatientAppointmentsViewModel> MyAppointments(string patientId)
        {
            var upcomingAppointments = await _appointmentRepository.GetUpcomingAppointmentsForPatientAsync(patientId);
            var pastAppointments = await _appointmentRepository.GetPastAppointmentsForPatientAsync(patientId);

            var upcomingAppointmentsDTO = _mapper.Map<List<AppointmentToViewDTO>>(upcomingAppointments);
            var pastAppointmentsDTO = _mapper.Map<List<AppointmentToViewDTO>>(pastAppointments);


            var model = new PatientAppointmentsViewModel
            {
                UpcomingAppointments = upcomingAppointmentsDTO,
                PastAppointments = pastAppointmentsDTO
            };

            return model;
        }


        public async Task<List<PatientToViewDTO>> SearchPatientsAsync(string keyword)
        {
            var query = _context.Patients
                .Include(p => p.User)
                .Where(p => p.User.FirstName.Contains(keyword) || p.User.LastName.Contains(keyword) || p.SSN.Contains(keyword));

            var results = await query.ToListAsync();
            return _mapper.Map<List<PatientToViewDTO>>(results);
        }

        public async Task<List<AppointmentToViewDTO>> GetMyAppointmentsAsync(string patientId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByPatientIdAsync(patientId);
            return _mapper.Map<List<AppointmentToViewDTO>>(appointments);
        }

        public async Task<bool> BookAppointmentAsync(AppointmentToAddDTO appointmentDto)
        {
            try
            {
                var appointment = _mapper.Map<Appointment>(appointmentDto);
                await _appointmentRepository.AddAsync(appointment);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    
        public async Task<List<PatientToViewDTO>> GetMyMedicalRecordsAsync(string patientId)
        {
            var records = await _patientRecordRepository.GetByPatientIdAsync(patientId);
            return _mapper.Map<List<PatientToViewDTO>>(records);
        }


        public async Task<PatientMedicalRecordsPViewModel> MyMedicalRecords(string patientId)
        {
            var medicalRecords = await _patientRecordRepository.GetListByPatientIdAsync(patientId);

            var model = new PatientMedicalRecordsPViewModel
            {
                MedicalRecords = medicalRecords.Select(record => new PatientRecordViewModel
                {
                    RecordId = record.Id,
                    RecordDate = record.RecordDate,
                    Diagnosis = record.Diagnosis,
                    Treatment = record.Treatment
                }).ToList()
            };

            return model;
        }


    }
}
