using Hospital.Service.Interfaces;
using Hospital.ViewModel.Reception;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    public class ReceptionController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IReceptionService _secretaryService;
        private readonly IDoctorService _doctorService;

        public ReceptionController(IAppointmentService appointmentService, IPatientService patientService, IReceptionService secretaryService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _secretaryService = secretaryService;
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var model = new ReceptionDashboardViewModel
            {
                TotalPatients = await _secretaryService.GetTotalPatientsAsync(),
                TotalAppointments = await _secretaryService.GetTotalAppointmentsAsync(),
                UpcomingAppointments = await _secretaryService.GetUpcomingAppointmentsAsync()
            };

            return View(model);
        }


        public async Task<IActionResult> GetAllAppointments()
        {
            var list =await _appointmentService.GetAllAppointmentsAsync();
            return View(list);

        }
        // 0 referencses
        public IActionResult AddAppointment() => View();

        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentToAddDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _appointmentService.AddAppointmentAsync(model);
            return RedirectToAction("Dashboard");
        }



        [HttpGet]
        public IActionResult GetBySSN(string ssn)
        {
            var patient = _patientService.GetBySSN(ssn);
            if (patient == null)
                return NotFound();

            return Json(new
            {
                id = patient.Id,
                fullName = $"{patient.User.FirstName} {patient.User.LastName}",
                age = patient.DOB
            });
        }



        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _doctorService.GetAll(); 
            var result = doctors.Select(d => new
            {
                id = d.Id,
                fullName = $"{d.User.FirstName} {d.User.LastName}"
            });

            return Json(result);
        }

    }
}
