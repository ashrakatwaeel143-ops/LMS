using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LmsProject.Data;
using LmsProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LmsProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context) => _context = context;

        
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Teacher)
                .ToListAsync();

            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Teachers = _context.Users.Where(u => u.RoleId == 2).ToList(); // 2 = Teacher
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string description, int categoryId, int teacherId)
        {
            var course = new Course
            {
                Title = title,
                Description = description,
                CategoryId = categoryId,
                TeacherId = teacherId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}