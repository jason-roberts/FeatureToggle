using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspDotNetCoreExample.Models;
using AspDotNetCoreExample.ViewModels;
using ASPNetCoreExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspDotNetCoreExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly Printing _print;
        private readonly Saving _save;

        public HomeController(Printing print, Saving save)
        {
            _print = print;
            _save = save;
        }

        public IActionResult Index()
        {
            return View(new HomeIndexViewModel { Printing = _print, Saving = _save });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
