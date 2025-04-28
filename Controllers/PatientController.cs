using AutoMapper;
using Hospital.Models;
using Hospital.Service.Interfaces;
using Hospital.ViewModel.Patient;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    public class PatientController : Controller
    {
      
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientService _patientService;
       
        public PatientController(IPatientRepository patientRepository,IPatientService patientService)
        {
       
            _patientRepository = patientRepository;
            _patientService = patientService;
        }


        private async Task<string?> GetCurrentPatientIdAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return null;

            var patient = await _patientRepository.GetByUserIdAsync(userId);

            if (patient == null)
                return null;

            return patient.Id;
        }

        public async Task<IActionResult> Dashboard()
        {
            var patientId = await GetCurrentPatientIdAsync();
            if (patientId == null)
                return NotFound();

            var model = await _patientService.Dashboard(patientId);
            model.FullName = User.Identity.Name ?? "Patient";
            return View(model);
        }



        public async Task<IActionResult> MyAppointments()
        {
            var patientId = await GetCurrentPatientIdAsync();
            if (patientId == null)
                return NotFound();

            var model = await _patientService.MyAppointments(patientId);
            return View(model);
        }



        public async Task<IActionResult> MyMedicalRecords()
        {
            var patientId = await GetCurrentPatientIdAsync();
            if (patientId == null)
                return NotFound();


            var model = await  _patientService.MyMedicalRecords(patientId);

            return View(model);
        }





    }
}
