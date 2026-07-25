using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LmsProject.Data;
using LmsProject.Models;
using System.Threading.Tasks;

namespace LmsProject.Controllers
{
    public class MaterialController : Controller
    {
        private readonly AppDbContext _context;

        public MaterialController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var materials = await _context.Materials
                .Include(m => m.Course)
                .ToListAsync();

            return View(materials);
        }

        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string fileUrl, int courseId)
        {
            var material = new Material
            {
                Title = title,
                FileUrl = fileUrl,
                CourseId = courseId
            };

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}