using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LmsProject.Data;
using LmsProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LmsProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }

            var role = _context.Roles.First(r => r.Id == user.RoleId).Name;

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("Role", role);

            if (role == "Teacher")
                return RedirectToAction("Index", "TeacherDashboard");
            else
                return RedirectToAction("Index", "StudentDashboard");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string fullName, string email, string password, string role)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                ViewBag.Error = "Email already registered";
                return View();
            }

            var roleEntity = _context.Roles.First(r => r.Name == role);

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = password,
                RoleId = roleEntity.Id
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Account created successfully. Please log in.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}