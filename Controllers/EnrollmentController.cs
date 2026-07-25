using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LmsProject.Data;
using LmsProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LmsProject.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly AppDbContext _context;
        public EnrollmentController(AppDbContext context) => _context = context;

        // GET: /Enrollment
        public async Task<IActionResult> Index()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();

            return View(enrollments);
        }

        // GET: /Enrollment/Create
        public IActionResult Create()
        {
            ViewBag.Students = _context.Users.Where(u => u.RoleId == 1).ToList(); // 1 = Student
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        // POST: /Enrollment/Create
        [HttpPost]
        public async Task<IActionResult> Create(int studentId, int courseId)
        {
            bool alreadyEnrolled = _context.Enrollments
                .Any(e => e.StudentId == studentId && e.CourseId == courseId);

            if (alreadyEnrolled)
            {
                ViewBag.Error = "This student is already enrolled in this course.";
                ViewBag.Students = _context.Users.Where(u => u.RoleId == 1).ToList();
                ViewBag.Courses = _context.Courses.ToList();
                return View();
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}