using Microsoft.AspNetCore.Mvc;
using Mission08_Team3_7.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mission08_Team3_7.Controllers
{
    public class HomeController : Controller
    {
        private readonly TimeManagementContext _context;

        // Inject the database context
        public HomeController(TimeManagementContext context)
        {
            _context = context;
        }

        // Action for the main grid view
        public IActionResult MatrixGrid()
        {
            // Fetch tasks from the database, including the Category navigation property
            var tasks = _context.Tasks
                .Include(t => t.Category) // Include the Category data
                .Where(t => !t.Completed) // Only show incomplete tasks
                .ToList();

            // Pass the tasks to the view
            return View(tasks);
        }

        // Action to mark a task as completed
        [HttpPost]
        public IActionResult MarkCompleted(int id)
        {
            // Find the task by ID
            var task = _context.Tasks.Find(id);

            if (task != null)
            {
                // Mark the task as completed
                task.Completed = true;
                _context.SaveChanges();
            }

            // Redirect back to the main grid
            return RedirectToAction("MatrixGrid");
        }

        // Action to delete a task
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Find the task by ID
            var task = _context.Tasks.Find(id);

            if (task != null)
            {
                // Remove the task
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }

            // Redirect back to the main grid
            return RedirectToAction("MatrixGrid");
        }

        // Default action for the home page
        public IActionResult Index()
        {
            return View();
        }

        // Privacy page (default template)
        public IActionResult Privacy()
        {
            return View();
        }
    }
}