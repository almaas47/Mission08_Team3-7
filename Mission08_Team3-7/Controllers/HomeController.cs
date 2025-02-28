using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team3_7.Models;
using Microsoft.EntityFrameworkCore; // Add this for Include() and ToList()

namespace Mission08_Team3_7.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TimeManagementContext _context; // Add this for database access

    // Update the constructor to inject TimeManagementContext
    public HomeController(ILogger<HomeController> logger, TimeManagementContext context)
    {
        _logger = logger;
        _context = context; // Initialize the database context
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Update the MainGrid action to fetch tasks from the database
    public IActionResult MainGrid()
    {
        // Fetch all tasks from the database, including the Category navigation property
        var tasks = _context.Tasks
            .Include(t => t.Category) // Include the Category data
            .ToList();

        // Pass the tasks to the view
        return View(tasks);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}