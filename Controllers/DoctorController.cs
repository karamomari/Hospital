using Hospital.Service.Interfaces;
using Hospital.ViewModel;
using Hospital.ViewModel.Doctor;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IPatientRecordRepository _recordRepo;
        private readonly IDoctorService _doctorService;


        public DoctorController(IPatientRecordRepository recordRepo, IDoctorService doctorService)
        {
            _recordRepo = recordRepo;
            _doctorService = doctorService;
        }




        public async Task<IActionResult> Dashboard()
        {
            string doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // معلومات الإحصائيات مثل عدد السجلات الخاصة بالدكتور
            var recordsCount = await _doctorService.GetRecordsForDoctorAsync(doctorId);
            var totalRecords = recordsCount.Count;

            // هنا ممكن تضيف أي معلومات إضافية حسب الحاجة
            var dashboardData = new DoctorDashboardViewModel
            {
                TotalRecords = totalRecords,
                // بإمكانك إضافة معلومات إضافية عن المرضى أو أي إحصائيات أخرى
            };

            return View(dashboardData);
        }


        // عرض سجلات المرضى
        public async Task<IActionResult> Records()
        {
            string doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var records = await _doctorService.GetRecordsForDoctorAsync(doctorId);
            return View(records);
        }

        public async Task<IActionResult> RecordsByPatientId(string PatientId)
        {
            var records = await _doctorService.GetOneRecordsForDoctorAsync(PatientId);
            return View(records);
        }


        [HttpGet]
        public IActionResult AddRecord()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddRecord(PatientRecord record)
        {
            if (!ModelState.IsValid)
                return View(record);

            record.DoctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _doctorService.AddPatientRecordAsync(record);
            if (!result)
            {
                ModelState.AddModelError("", "An error occurred while saving the record. Try again.");
                return View(record);
            }

            return RedirectToAction("Records");
        }


        [HttpGet]
        public async Task<IActionResult> EditRecord(string id)
        {
            var record = await _recordRepo.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            return View(record);
        }



        [HttpPost]
        public async Task<IActionResult> EditRecord(PatientRecord record)
        {
            if (!ModelState.IsValid)
                return View(record);

            try
            {
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

    }
}
