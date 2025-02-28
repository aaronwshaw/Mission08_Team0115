using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0115.Models;

namespace Mission08_Team0115.Controllers
{
    public class HomeController : Controller
    {
        private ITaskRepository _repo; //repository constructor

        public HomeController(ITaskRepository temp) 
        {  
            _repo = temp;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
