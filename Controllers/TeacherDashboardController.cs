using Microsoft.AspNetCore.Mvc;

namespace LmsProject.Controllers
{
    public class TeacherDashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Teacher")
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}