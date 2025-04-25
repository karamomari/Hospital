using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hospital.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        MvcHospitalcontext c;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, MvcHospitalcontext c)
        {
            _logger = logger;
            _userManager = userManager;
            this.c = c;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }







        //public async Task<ActionResult> Register()
        //{
        //    var user = new ApplicationUser
        //    {
        //        UserName = "dssas",
        //        Email = "ssss@gmail.com",
        //        FirstName = "aa",
        //        LastName = "ss",
        //        AccountCreatedAt = DateTime.UtcNow,
        //        IsActive = true
        //    };

        //    var result = await _userManager.CreateAsync(user, "Bombom");

        //    if (result.Succeeded)
        //    {
        //        _userManager.AddToRoleAsync(user, "Patient");
        //        _logger.LogInformation($"New user registered: {user.Email} at {DateTime.UtcNow}");
        //        var p = new Patient();
        //        p.Id = Guid.NewGuid().ToString();
        //        p.UserId = user.Id;
        //        p.InsuranceNo = "ds";
        //        p.DOB = new DateTime(1995, 7, 10);
        //        c.Patients.Add(p);
        //        c.SaveChanges();
        //        ممكن تبعت كود تفعيل هون بدال تسجيل الدخول
        //        return Content("Login");
        //    }
        //    return Content("no Login");

        //}





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
