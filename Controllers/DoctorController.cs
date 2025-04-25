using AutoMapper;
using Hospital.Service.Implementations;
using Hospital.Service.Interfaces;
using Hospital.ViewModel;
using Hospital.ViewModel.Appointment;
using Hospital.ViewModel.Doctor;
using Hospital.ViewModel.Patient;
using Hospital.ViewModel.PatientRecord;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IPatientRecordRepository _recordRepo;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        MvcHospitalcontext context;
        private  readonly IAppointmentRepository _appointmentRepo;
        private readonly IMapper _mapper;

        public DoctorController(IPatientRecordRepository recordRepo, IDoctorService doctorService,MvcHospitalcontext context,
            IPatientService patientService, IMapper mapper, IAppointmentRepository appointmentRepo)
        {
            _recordRepo = recordRepo;
            _doctorService = doctorService;
            this.context = context;
            _patientService = patientService;
            _mapper = mapper;
            _appointmentRepo = appointmentRepo;
        }




        public async Task<IActionResult> Dashboard()
        {
            string userId= User.FindFirstValue(ClaimTypes.NameIdentifier);


            // معلومات الإحصائيات مثل عدد السجلات الخاصة بالدكتور
            var recordsCount = await _doctorService.GetRecordsForDoctorAsync(userId);
            var totalRecords = recordsCount.Count;
            var AppointmentCount = await _doctorService.CountAppointmentsByDoctorIdAsync(userId);
            var totalAppointment = AppointmentCount.Count;

            // هنا ممكن تضيف أي معلومات إضافية حسب الحاجة
            var dashboardData = new DoctorDashboardViewModel
            {
                TotalRecords = totalRecords,
                TotalAppointments=totalAppointment
            };

            return View(dashboardData);
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


        // عرض سجلات المرضى
        public async Task<IActionResult> Records()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var records = await _doctorService.GetRecordsForDoctorAsync(userId);
            return View(records);
        }

        public async Task<IActionResult> RecordsByPatientId(string id)
        {

            var records = await _doctorService.GetOneRecordsForDoctorAsync(id);
            return View(records);
        }


        [HttpGet]
        public IActionResult AddRecord()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddRecord(PatientRecordToAddDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var record = _mapper.Map<PatientRecord>(dto); 

            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _doctorService.AddPatientRecordAsync(record,UserId);
            if (!result)
            {
                ModelState.AddModelError("", "An error occurred while saving the record. Try again.");
                return View(dto);
            }

            return RedirectToAction("Records");
        }

        [HttpGet]
        public async Task<IActionResult> EditRecord(string id)
        {
            Console.WriteLine("ID: " + id);
            var record = await _recordRepo.GetByIdAsync(id);
            if (record == null)
                return NotFound();
            var dto = _mapper.Map<PatientRecordToupdateDTO>(record);
            return View(dto);
        }



        [HttpPost]
        public async Task<IActionResult> EditRecord(PatientRecordToupdateDTO record)
        {
            try
            {

                if (!ModelState.IsValid)
                    return View(record);
                await _doctorService.UpdateRecordAsync(record);
                TempData["Success"] = "Record updated successfully.";
                return RedirectToAction("Records");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(record);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyAppointments()
        {
            try 
            { 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var appointments = await _doctorService.MyAppointments(userId);

            var appointmentDTOs = _mapper.Map<IEnumerable<AppointmentToViewDTO>>(appointments);

            return View(appointmentDTOs);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }


    }
}
