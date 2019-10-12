using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

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
            //CourseViewModel courseViewModel = new CourseViewModel();
            //courseViewModel.Id = 11;
            //courseViewModel.Name = "QQQQQQQQQQQ";
            //courseViewModel.NumberOfSession = 1;
            //courseViewModel.TraingTime = 4;
            //courseViewModel.Price = 2;
            //courseViewModel.Status = Status.Active;
            //courseViewModel.Content = "";
            //courseViewModel.DateCreated = DateTime.Now;
            //courseViewModel.DateModified = DateTime.Now;


            //_courseService.AddAsync(courseViewModel);
            //_courseService.SaveChanges();

            //_courseService.DeleteAsync(7);

            //_courseService.UpdateAsync(courseViewModel);

            List<CourseViewModel> courseViewModels = _courseService.GetAll();

            ViewBag.Name = courseViewModels[1].Name;

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
