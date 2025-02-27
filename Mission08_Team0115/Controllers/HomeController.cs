using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0115.Models;

namespace Mission08_Team0115.Controllers
{
    public class HomeController : Controller
    {

        //This is very Temporary
        private AddEditTask.AddEditTaskContext _context;

        public HomeController(AddEditTask.AddEditTaskContext temp) // constructor
        {
            _context = temp;
        }




        //The Queries will definitely maybe be changed to make it work with the database


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Quadrant()
        {
            var tasks = _context.Tasks
            .Include(x => x.Category)
            .OrderBy(x => x.CategoryName).ToList();
            return View(tasks);
        }


        // Takes you to the AddEditTask page to add a new Task
        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Tasks = _context.Tasks
                .ToList();

            return View("AddEditTask", new Task());
        }


        // Adds the new Task - redirects to confirmation
        [HttpPost]
        public IActionResult AddTask(Task response)
        {
            if (ModelState.IsValid)
            {
                _context.Task.Add(response);
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

        //Populates the AddEditTask view with the data from the Task you want to edit
        [HttpGet]
        public IActionResult Edit(int TaskID)
        {
            var task = _context.Tasks
                .Single(x => x.TaskId == TaskID);

            ViewBag.Categories = _context.Categories.
                OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddEditTask", task);

        }

        //Submit the editted Task and redirects to Quadrant
        [HttpPost]
        public IActionResult Edit(Task updatedTask)
        {
            _context.Tasks.Update(updatedTask);
            _context.SaveChanges();

            return RedirectToAction("Quadrant");
        }







        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
