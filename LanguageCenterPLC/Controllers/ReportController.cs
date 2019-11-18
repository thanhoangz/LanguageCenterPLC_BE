using DinkToPdf;
using DinkToPdf.Contracts;
using IronPdf;
using LanguageCenterPLC.Application.ReportGenerate;
using LanguageCenterPLC.Application.ViewModels.Report;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {


        private readonly AppDbContext _context;
        private readonly IConverter _converter;



        public ReportController(AppDbContext context, IConverter converter)
        {
            _context = context;
            _converter = converter;
        }


       
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }



        [HttpGet]
        [Route("get-learner-in-class/{id}")]
        public void GetFileReportClass(string id)
        {
            var reports = (from l in _context.Learners
                           join st in _context.StudyProcesses on l.Id equals st.LearnerId
                           where (st.Status == Status.Active && st.LanguageClassId == id)
                           select new
                           {
                               Index = 1,
                               FullName = l.FirstName + " " + l.LastName,
                               YearOfBirth = l.Birthday.Year.ToString(),
                               Gender = l.Sex == true ? "Name" : "Nữ",
                               l.Phone,
                               ClassName = _context.LanguageClasses.Where(x => x.Id == id).Single().Name,
                               Note = ""
                           }).OrderBy(x => x.FullName).ToList();

            int i = 1;
            var results = new List<ReportViewModel>();
            foreach (var item in reports)
            {
                ReportViewModel reportViewModel = new ReportViewModel()
                {
                    Index = i,
                    FullName = item.FullName,
                    ClassName = item.ClassName,
                    Gender = item.Gender,
                    Note = "",
                    Phone = item.Phone,
                    YearOfBirth = item.YearOfBirth

                };
                results.Add(reportViewModel);
                i++;
            }


            HtmlToPdf Renderer = new HtmlToPdf();

            Renderer.RenderHtmlAsPdf(ReportControl.GetHTMLString(results)).SaveAs("baocaolop.pdf");

            var PDF = Renderer.RenderHtmlAsPdf(ReportControl.GetHTMLString(results), @"C:\Users\Than Hoang\source\repos\LanguageCenterPLC_BE\LanguageCenterPLC\wwwroot\css\");
            PDF.SaveAs("html-with-assets.pdf");

        }



        [HttpGet]
        [Route("get-test/{id}")]
        public async Task<IActionResult> GetFileReportClassx(string id)
        {
            var reports = (from l in _context.Learners
                           join st in _context.StudyProcesses on l.Id equals st.LearnerId
                           where (st.Status == Status.Active && st.LanguageClassId == id)
                           select new
                           {
                               Index = 1,
                               FullName = l.FirstName + " " + l.LastName,
                               YearOfBirth = l.Birthday.Year.ToString(),
                               Gender = l.Sex == true ? "Name" : "Nữ",
                               l.Phone,
                               ClassName = _context.LanguageClasses.Where(x => x.Id == id).Single().Name,
                               Note = ""
                           }).OrderBy(x => x.FullName).ToList();

            int i = 1;
            var results = new List<ReportViewModel>();
            foreach (var item in reports)
            {
                ReportViewModel reportViewModel = new ReportViewModel()
                {
                    Index = i,
                    FullName = item.FullName,
                    ClassName = item.ClassName,
                    Gender = item.Gender,
                    Note = "",
                    Phone = item.Phone,
                    YearOfBirth = item.YearOfBirth

                };
                results.Add(reportViewModel);
                i++;
            }


            HtmlToPdf Renderer = new HtmlToPdf();

            Renderer.RenderHtmlAsPdf(ReportControl.GetHTMLString(results)).SaveAs("html-string.pdf");

            var PDF = Renderer.RenderHtmlAsPdf(ReportControl.GetHTMLString(results), @"C:\Users\Than Hoang\source\repos\LanguageCenterPLC_BE\LanguageCenterPLC\wwwroot\css\");
            PDF.SaveAs("html-with-assets.pdf");


            try
            {



                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A5,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = ReportControl.GetHTMLString(results),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Models\\Untility\\Template\\assets", "styles.css") },
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                var file = _converter.Convert(pdf);
                return File(file, "application/pdf");
            }
            catch (ApplicationException e)
            {
                return BadRequest(new
                {
                    result = -1,
                    message = e.Message
                });
            }
            catch
            {
                return BadRequest(new { result = -2, message = "Lỗi cơ sở dữ liệu" });
            }
        }




    }
}