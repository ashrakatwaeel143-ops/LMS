using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LmsProject.Data;
using LmsProject.Models;
using System.Threading.Tasks;

namespace LmsProject.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly AppDbContext _context;

        public AssignmentController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var assignments = await _context.Assignments
                .Include(a => a.Course)
                .ToListAsync();

            return View(assignments);
        }

        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, 
            string description,
            System.DateTime deadline, 
            int courseId)
        {
            var assignment = new Assignment
            {
                Title = title,
                Description = description,
                Deadline = deadline,
                CourseId = courseId
            };

            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}