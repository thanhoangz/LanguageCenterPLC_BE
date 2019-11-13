using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Data.EF;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILearnerService _learnerService;
        private readonly AppDbContext _context;
        public SearchController(ILearnerService learnerService, AppDbContext context)
        {
            _learnerService = learnerService;
            _context = context;
        }

        //[HttpPost]
        //[Route("search-for-learner")]
        //public object GetFullPointClasses(string id)
        //{
           

        //}

    }
}