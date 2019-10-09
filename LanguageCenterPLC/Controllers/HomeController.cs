using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LanguageCenterPLC.Models;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Infrastructure.Enums;

namespace LanguageCenterPLC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;

        public HomeController(ILogger<HomeController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        public IActionResult XXX()
        {
            CourseViewModel courseViewModel = new CourseViewModel();
            courseViewModel.Id = 2;
            courseViewModel.Name = "caxxxxxcx";
            courseViewModel.NumberOfSession = 1;
            courseViewModel.TraingTime = 4;
            courseViewModel.Price = 2;
            courseViewModel.Status = Status.Active;
            courseViewModel.Content = "";
            courseViewModel.DateCreated = DateTime.Now;
            courseViewModel.DateModified = DateTime.Now;
            //_courseService.AddSync(courseViewModel);
            //_courseService.Delete(1);

            _courseService.UpdateSync(courseViewModel);
            _courseService.SaveChanges();

            ViewBag.Name = "qqqq";

            return View();
        }

        public IActionResult Index()
        {
            return View();
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
