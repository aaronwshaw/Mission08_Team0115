using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0115.Models;
using Task = Mission08_Team0115.Models.Task;

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

            return View("AddEditTask", new Models.Task());
        }

        [HttpPost]
        public IActionResult AddTask(Models.Task response)
        {
            if (ModelState.IsValid)
            {
                _repo.AddTask(response);  // Use repository method
                return View("ConfirmAddEdit", response);
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
        public IActionResult Edit(int TaskID)
        {
            var task = _repo.Tasks
                .Single(x => x.TaskId == TaskID);

            ViewBag.Categories = _repo.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddEditTask", task);
        }

        [HttpPost]
        public IActionResult Edit(Models.Task updatedTask)
        {
            _repo.Edit(updatedTask);  // Use repository method
            return RedirectToAction("Quadrant");
        }

        [HttpGet]
        public IActionResult Delete(int TaskId)
        {
            var recordToDelete = _repo.Tasks
                .Single(x => x.TaskId == TaskId);

            return View("DeleteConfirm", recordToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Models.Task recordToDelete)
        {
            _repo.Delete(recordToDelete);  // Use repository method
            return RedirectToAction("Quadrant");
        }
    }
}
