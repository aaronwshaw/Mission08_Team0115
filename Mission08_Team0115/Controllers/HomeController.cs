using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0115.Models;

namespace Mission08_Team0115.Controllers
{
    public class HomeController : Controller
    {
        private ITaskRepository _repo;

        public HomeController(ITaskRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = _repo.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddEditTask", new Task());
        }

        [HttpPost]
        public IActionResult AddTask(Task response)
        {
            if (ModelState.IsValid)
            {
                _repo.AddTask(response);  // Use repository method
                return View("Confirmation", response);
            }
            else
            {
                ViewBag.Categories = _repo.Categories
                    .OrderBy(x => x.CategoryName)
                    .ToList();

                return View("AddEditTask", response);
            }
        }

        public IActionResult Quadrant()
        {
            var tasks = _repo.Tasks
                .Include(x => x.Category)
                .ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Edit(int taskID)
        {
            var task = _repo.Tasks
                .Single(x => x.TaskId == taskID);

            ViewBag.Categories = _repo.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddEditTask", task);
        }

        [HttpPost]
        public IActionResult Edit(Task updatedTask)
        {
            _repo.UpdateTask(updatedTask);  // Use repository method
            return RedirectToAction("Quadrant");
        }

        [HttpGet]
        public IActionResult DeleteTask(int taskID)
        {
            var recordToDelete = _repo.Tasks
                .Single(x => x.TaskId == taskID);

            return View("DeleteConfirm", recordToDelete);
        }

        [HttpPost]
        public IActionResult DeleteTask(Task recordToDelete)
        {
            _repo.DeleteTask(recordToDelete);  // Use repository method
            return RedirectToAction("Quadrant");
        }
    }
}
