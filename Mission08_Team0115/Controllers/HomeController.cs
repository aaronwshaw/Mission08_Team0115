using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0115.Models;

namespace Mission08_Team0115.Controllers
{
    public class HomeController : Controller
    {
        private TaskDb.TaskDbContext _context;

        public HomeController(TaskDb.TaskDbContext temp) // Constructor
        {
            _context = temp;
        }

        // Actions for the different page views
        public IActionResult Index()
        {
            return View();
        }

        // Takes you to the AddTask page
        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddEditTask", new Task());
        }

        // Adds the new task - redirects to confirmation
        [HttpPost]
        public IActionResult AddTask(Task response)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(response);
                _context.SaveChanges();

                return View("Confirmation", response);
            }
            else
            {
                ViewBag.Categories = _context.Categories
                    .OrderBy(x => x.CategoryName)
                    .ToList();

                return View("AddEditTask", response);
            }
        }

        // View for the Quadrant Page
        public IActionResult Quadrant()
        {
            var tasks = _context.Tasks
                .Include(x => x.Category) // Assuming the navigation property is singular
                .ToList();

            return View(tasks);
        }

        // Populates the AddEditTask view with the data from the task you want to edit
        [HttpGet]
        public IActionResult Edit(int taskID)
        {
            var task = _context.Tasks
                .Single(x => x.TaskId == taskID);

            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddEditTask", task);
        }

        // Submit the edited task and redirects to Quadrant page
        [HttpPost]
        public IActionResult Edit(Task updatedTask)
        {
            _context.Tasks.Update(updatedTask);
            _context.SaveChanges();

            return RedirectToAction("Quadrant");
        }

        // Finds the task you want to delete
        [HttpGet]
        public IActionResult DeleteTask(int taskID)
        {
            var recordToDelete = _context.Tasks
                .Single(x => x.TaskId == taskID);

            return View("DeleteConfirm", recordToDelete);
        }

        // Deletes the task
        [HttpPost]
        public IActionResult DeleteTask(Task recordToDelete)
        {
            _context.Tasks.Remove(recordToDelete);
            _context.SaveChanges();

            return RedirectToAction("Quadrant");
        }
    }
}
