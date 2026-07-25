using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LmsProject.Data;
using LmsProject.Models;
using System.Threading.Tasks;

namespace LmsProject.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly AppDbContext _context;

        public SubmissionController(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var studentId = int.Parse(HttpContext.Session.GetString("UserId"));

            var submissions = await _context.Submissions
                .Include(s => s.Assignment)
                .Where(s => s.StudentId == studentId)
                .ToListAsync();

            return View(submissions);
        }

       
        public IActionResult Create(int assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int assignmentId, string content)
        {
            var studentId = int.Parse(HttpContext.Session.GetString("UserId"));

            var submission = new Submission
            {
                AssignmentId = assignmentId,
                StudentId = studentId,
                Content = content
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GradeList()
        {
            var submissions = await _context.Submissions
                .Include(s => s.Assignment)
                .Include(s => s.Student)
                .ToListAsync();

            return View(submissions);
        }

        
        [HttpPost]
        public async Task<IActionResult> Grade(int id, int grade)
        {
            var submission = await _context.Submissions.FindAsync(id);
            submission.Grade = grade;

            await _context.SaveChangesAsync();

            return RedirectToAction("GradeList");
        }
    }
}