using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnersController : ControllerBase
    {
        private readonly ILearnerService _learnerService;
        private readonly AppDbContext _context;

        public LearnersController(ILearnerService learnerService, AppDbContext context)
        {
            _learnerService = learnerService;
            _context = context;
        }

        // GET: api/Learners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LearnerViewModel>>> GetLearners()
        {
            return await Task.FromResult(_learnerService.GetAll());
        }

        // GET: api/Learners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LearnerViewModel>> GetLearner(string id)
        {
            var leaner = _learnerService.GetById(id);

            if (leaner == null)
            {
                return NotFound("Không tìm thấy khóa học có id = " + id);
            }

            return await Task.FromResult(leaner);
        }


        // GET: api/Learners/get-by-cardid
        [HttpGet("/api/Learners/get-by-cardid/{id}")]
        public async Task<ActionResult<LearnerViewModel>> GetByCardId(string id)
        {
            return await Task.FromResult(_learnerService.GetByCardId(id));
        }

        // GET: api/Learners/get-in-class
        [HttpGet("/api/Learners/get-in-class/{id}")]
        public async Task<ActionResult<IEnumerable<LearnerViewModel>>> GetLearnersInClass(string id)
        {
            return await Task.FromResult(_learnerService.GetAllInClass(id));
        }

        // GET: api/Learners/get-chua-co-lop
        [HttpGet("/api/Learners/get-chua-co-lop")]
        public async Task<ActionResult<IEnumerable<LearnerViewModel>>> GetChuaCoLop()
        {
            return await Task.FromResult(_learnerService.ChuaCoLop());
        }


        // GET: api/Learners/get-da-co-lop
        [HttpGet("/api/Learners/get-da-co-lop")]
        public async Task<ActionResult<IEnumerable<LearnerViewModel>>> GetDaCoLop()
        {
            return await Task.FromResult(_learnerService.DaCoLop());
        }

        // GET: api/Learners/get-out-class
        [HttpGet("/api/Learners/get-out-class/{id}")]
        public async Task<ActionResult<IEnumerable<LearnerViewModel>>> GetLearnersOutClass(string id)
        {
            return await Task.FromResult(_learnerService.GetAllOutClass(id));
        }


        [HttpGet("/api/Learners/get-out-class-with-condition/{classId}/{keyword}")]
        public async Task<ActionResult<IEnumerable<LearnerViewModel>>> GetLearnersOutClassWithCondition(string classId, string keyword)
        {
            return await Task.FromResult(_learnerService.GetOutClassWithCondition(classId, keyword));
        }

        [HttpGet]
        [Route("getlearninginclass/{classId}")]
        public async Task<ActionResult<IEnumerable<LearnerViewModel>>> GetFullLearningByClass(string classId)
        {
            return await Task.FromResult(_learnerService.GetFullLearningByClass(classId));
        }

        [HttpPost]
        [Route("get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<LearnerViewModel>>> GetAllConditions(string keyword = "", int status = 1)
        {
            return await Task.FromResult(_learnerService.GetAllWithConditions(keyword, status));
        }

        [HttpPost]
        [Route("get-learner-cardId")]
        public async Task<ActionResult<LearnerViewModel>> GetLearnerCardIdForReceipt(string cardId)
        {
            return await Task.FromResult(_learnerService.GetLearnerCardIdForReceipt(cardId));
        }

       
        // PUT: api/Learners/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLearner(string id, LearnerViewModel learner)
        {
            if (learner.Id != id)
            {
                throw new Exception(string.Format("Id và Id của người học không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    learner.DateModified = DateTime.Now;
                    _learnerService.Update(learner);
                    _learnerService.SaveChanges();
                    return Ok();
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Learners
        [HttpPost]
        public async Task<ActionResult<Learner>> PostLearner(LearnerViewModel learner)
        {
            if (learner != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        learner.DateCreated = DateTime.Now;
                        if (String.IsNullOrEmpty(learner.ParentFullName) || String.IsNullOrEmpty(learner.ParentPhone))
                        {
                            learner.ParentFullName = "";
                            learner.ParentPhone = "";
                        }

                        _learnerService.Add(learner);
                        _learnerService.SaveChanges();
                        return Ok("thêm khóa học thành công!");
                    });

                }
                catch
                {

                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }

            }

            return CreatedAtAction("GetLearner", new { id = learner.Id }, learner);
        }

        // DELETE: api/Learners/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Learner>> DeleteLearner(string id)
        {
            var learner = _learnerService.GetById(id);
            if (learner == null)
            {
                return NotFound("Không tìm thấy khóa học có Id = " + id);
            }

            try
            {
                await Task.Run(() =>
                {
                    _learnerService.Delete(id);
                    _learnerService.SaveChanges();
                });
            }
            catch
            {

                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }

            return Ok();
        }


        private bool LearnerExists(string id)
        {
            return _learnerService.IsExists(id);
        }



        [HttpPost]
        [Route("get-score-by-learner")]
        public async Task<List<Object>> GetFullScore(string id)
        {
            // lấy ra danh sách lớp học viên đã và đang học                                
            var studyProcessOfLearner = _context.StudyProcesses.Where(x => x.LearnerId == id).OrderBy(y => y.DateCreated).ToList();
            var resultList = new List<Object>();

            foreach (var study in studyProcessOfLearner)
            {
                LanguageClass languageClass = _context.LanguageClasses.Where(x => x.Id == study.LanguageClassId).SingleOrDefault();


                var PeriodicPoints = new List<Object>();
                var allPeriodicPoint = _context.PeriodicPoints.Where(x => x.LanguageClassId == study.LanguageClassId).OrderBy(y => y.ExaminationDate).ToList();
                if (allPeriodicPoint.Count != 0)
                {
                    foreach (var periodic in allPeriodicPoint)
                    {

                        var detailPointOfLearner = _context.PeriodicPointDetails.Where(x => x.LearnerId == id && x.PeriodicPointId == periodic.Id).SingleOrDefault();
                        if (detailPointOfLearner != null)
                        {

                            PeriodicPoints.Add(
                                new
                                {
                                    periodic.Week,
                                    detailPointOfLearner = Mapper.Map<PeriodicPointDetailViewModel>(detailPointOfLearner)
                                });
                        }

                    }

                }


                var EndingCoursePoint = new Object();
                var allEndingCoursePonint = _context.EndingCoursePoints.Where(x => x.LanguageClassId == study.LanguageClassId).OrderBy(y => y.ExaminationDate).ToList();
                foreach (var ending in allEndingCoursePonint)
                {
                    var detailEndingOfLearner = _context.EndingCoursePointDetails.Where(x => x.LearnerId == id && x.EndingCoursePointId == ending.Id).SingleOrDefault();
                    if (detailEndingOfLearner != null)
                    {
                        EndingCoursePoint = new 
                        { 
                            detailEndingOfLearner.AveragePoint,
                            detailEndingOfLearner.ListeningPoint,
                            detailEndingOfLearner.ReadingPoint,
                            detailEndingOfLearner.SayingPoint,
                            detailEndingOfLearner.SortOrder,
                            detailEndingOfLearner.WritingPoint,
                            detailEndingOfLearner.TotalPoint,
                        };
                    }
                }


                var languageClassVm = new
                {
                    languageClass.Id,
                    languageClass.Name,
                };

                resultList.Add(
                    new
                    {
                        Class = languageClassVm,
                        PeriodicPoints,
                        EndingCoursePoint
                    });
            }
            return await Task.FromResult(resultList);
        }


 
    }
}
