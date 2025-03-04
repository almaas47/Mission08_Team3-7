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
        
        // Action to display the form for adding a new task
        [HttpGet]
        public IActionResult Create()
        {
            // Pass the list of categories to the view for the dropdown
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

// Action to handle the form submission for adding a new task
        [HttpPost]
        public IActionResult Create(Mission08_Team3_7.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                // Set the task as incomplete by default
                task.Completed = false;

                // Add the task to the database
                _context.Tasks.Add(task);
                _context.SaveChanges();

                // Redirect to the MatrixGrid view
                return RedirectToAction("MatrixGrid");
            }

            // If the model state is not valid, return the view with the current task and categories
            ViewBag.Categories = _context.Categories.ToList();
            return View(task);
        }
        
        // Action to display the form for editing a task
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Find the task by ID
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound(); // Return a 404 if the task is not found
            }

            // Pass the list of categories to the view for the dropdown
            ViewBag.Categories = _context.Categories.ToList();
            return View(task);
        }

// Action to handle the form submission for editing a task
        [HttpPost]
        public IActionResult Edit(Mission08_Team3_7.Models.Task task) // Fully qualify the Task model
        {
            if (ModelState.IsValid)
            {
                // Update the task in the database
                _context.Tasks.Update(task);
                _context.SaveChanges();

                // Redirect to the MatrixGrid view
                return RedirectToAction("MatrixGrid");
            }

            // If the model state is not valid, return the view with the current task and categories
            ViewBag.Categories = _context.Categories.ToList();
            return View(task);
        }
        
        // Show completed tasks
        public IActionResult CompletedTasks()
        {
            var completedTasks = _context.Tasks
                .Include(t => t.Category)
                .Where(t => t.Completed)
                .OrderByDescending(t => t.DueDate)
                .ToList();

            return View(completedTasks);
        }
        
        [HttpPost]
        public IActionResult ToggleCompleted(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                task.Completed = !task.Completed;
                _context.SaveChanges();
            }
            return RedirectToAction(task.Completed ? "CompletedTasks" : "MatrixGrid");
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