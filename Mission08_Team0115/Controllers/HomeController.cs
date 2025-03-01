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

        //Home page
        public IActionResult Index()
        {
            return View();
        }


        //Action to pull up the addedittask page for a new task
        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = _repo.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddEditTask", new Models.Task());
        }


        //Action to add the new task to the database
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



        //Load the quadrant page with only noncompleted tasks
        public IActionResult Quadrant()
        {
            var tasks = _repo.Tasks
                .Include(x => x.Category)
                .Where(t => t.Completed != true)  // Filter out completed tasks
                .ToList();

            return View(tasks);
        }


        //Action to edit the task in the addedittask view
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

        //Action to post the editted task to the database
        [HttpPost]
        public IActionResult Edit(Models.Task updatedTask)
        {
            _repo.Edit(updatedTask);  // Use repository method
            return RedirectToAction("Quadrant");
        }

        //Action to pull up the delete confirmation page for a specific task
        [HttpGet]
        public IActionResult Delete(int TaskId)
        {
            var recordToDelete = _repo.Tasks
                .Single(x => x.TaskId == TaskId);

            return View("DeleteConfirm", recordToDelete);
        }

        //Action to officially delete the task from the database
        [HttpPost]
        public IActionResult Delete(Models.Task recordToDelete)
        {
            _repo.Delete(recordToDelete);  // Use repository method
            return RedirectToAction("Quadrant");
        }


        //Mark task as completed
        [HttpGet]
        public IActionResult Complete(int taskId)
        {
            _repo.UpdateComplete(taskId);  // Call the renamed method to mark the task as completed
            return RedirectToAction("Quadrant");  // Redirect back to the quadrant page
        }

    }
}
