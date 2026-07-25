using Microsoft.AspNetCore.Mvc;

namespace LmsProject.Controllers
{
    public class StudentDashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Student")
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}