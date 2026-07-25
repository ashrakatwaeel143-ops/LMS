using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LmsProject.Data;
using LmsProject.Models;
using System.Threading.Tasks;

namespace LmsProject.Controllers
{
    public class LessonController : Controller
    {
        private readonly AppDbContext _context;
        public LessonController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var lessons = await _context.Lessons
                .Include(l => l.Course)
                .ToListAsync();

            return View(lessons);
        }

        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string content, int courseId)
        {
            var lesson = new Lesson
            {
                Title = title,
                Content = content,
                CourseId = courseId
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}