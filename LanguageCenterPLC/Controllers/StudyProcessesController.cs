using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyProcessesController : ControllerBase
    {
        private readonly IStudyProcessService _studyProcessService;

        public StudyProcessesController(IStudyProcessService studyProcessService)
        {
            _studyProcessService = studyProcessService;
        }

        // GET: api/StudyProcesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudyProcessViewModel>>> GetStudyProcesses()
        {
            return await Task.FromResult(_studyProcessService.GetAll());
        }

        // GET: api/StudyProcesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudyProcessViewModel>> GetStudyProcess(int id)
        {
            var studyProcess = _studyProcessService.GetById(id);

            if (studyProcess == null)
            {
                return NotFound("Không tìm thấy mã = " + id);
            }

            return await Task.FromResult(studyProcess);
        }

        // PUT: api/StudyProcesses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudyProcess(int id, StudyProcessViewModel studyProcess)
        {
            if (studyProcess.Id != id)
            {
                throw new Exception(string.Format("Id và id của mã quá trình học tập không giống nhau!"));
            }

            try
            {
                await Task.Run(() =>
                {
                    studyProcess.DateModified = DateTime.Now;
                    _studyProcessService.Update(studyProcess);
                    _studyProcessService.SaveChanges();
                    return Ok();
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyProcessExists(id))
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


        // POST: api/StudyProcesses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<StudyProcessViewModel>> PostStudyProcess(StudyProcessViewModel studyProcess)
        {
            if (studyProcess != null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        studyProcess.DateCreated = DateTime.Now;
                        studyProcess.Status = Status.Active;
                        studyProcess.InDate = DateTime.Now;              
                        _studyProcessService.Add(studyProcess);
                        _studyProcessService.SaveChanges();
                        return Ok("Thêm thành công!");
                    });
                }
                catch
                {
                    throw new Exception(string.Format("Lỗi khi thêm dữ liệu"));
                }
            }
            return CreatedAtAction("GetStudyProcess", new { id = studyProcess.Id }, studyProcess);
        }

        // DELETE: api/StudyProcesses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudyProcess(int id)
        {
            var studyProcess = _studyProcessService.GetById(id);
            if (studyProcess == null)
            {
                return NotFound("Không tìm thấy mã Id = " + id);
            }
            try
            {
                await Task.Run(() =>
                {
                    _studyProcessService.Delete(id);
                    _studyProcessService.SaveChanges();
                });
            }
            catch
            {
                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }
            return Ok();
        }

        // DELETE: api/StudyProcesses/delete-by-learnerId
        [HttpDelete]
        [Route("delete-by-classId-learnerId")]
        public async Task<ActionResult> DeleteStudyProcessByLearner(string classId, string learnerId)
        {
            try
            {
                await Task.Run(() =>
                {
                    _studyProcessService.DeleteByLearner(classId, learnerId);
                    _studyProcessService.SaveChanges();
                });
            }
            catch
            {
                throw new Exception(string.Format("Có lỗi xảy ra không thể xóa!"));
            }
            return Ok();
        }

        [HttpPost("/api/StudyProcesses/update-status")]
        public async Task<ActionResult<StudyProcessViewModel>> UpdateStatusById(int studyProcessId, Status status)
        {
            try
            {
                await Task.Run(() =>
                {
                    _studyProcessService.UpdateStatus(studyProcessId, status);
                    _studyProcessService.SaveChanges();
                    return Ok("Update thành công!");
                });
            }
            catch
            {
                throw new Exception(string.Format("Lỗi !"));
            }
            return Ok();
        }


        [HttpPost("/api/StudyProcesses/paging")]
        public async Task<ActionResult<PagedResult<StudyProcessViewModel>>> PagingPaySlip(string keyword = "", int status = 0, int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                return await Task.FromResult(_studyProcessService.GetAllPaging(keyword, status, pageSize, pageIndex));
            }
            catch
            {
                throw new Exception(string.Format("Lỗi xảy ra ở phân trang!"));
            }
        }

        [HttpPost("/api/StudyProcesses/get-all-with-conditions")]
        public async Task<ActionResult<IEnumerable<StudyProcessViewModel>>> GetAllConditions(string LanguageClassId = "", string LearnerId = "", int status = 1)
        {
            return await Task.FromResult(_studyProcessService.GetAllWithConditions(LanguageClassId, LearnerId, status));
        }

        [HttpPost("/api/StudyProcesses/get-study-by-classid")]
        public async Task<ActionResult<IEnumerable<StudyProcessViewModel>>> GetStudyByClassId(string LanguageClassId = "", int status = 1)
        {
            return await Task.FromResult(_studyProcessService.GetStudyProcessByClassId(LanguageClassId, status));
        }

        [HttpPost]
        [Route("get-by-classId-learnerId")]
        public async Task<ActionResult<StudyProcessViewModel>> GetStudyByClass(string classId = "", string learnerId = "")
        {
            return await Task.FromResult(_studyProcessService.GetByClassLearner(classId, learnerId));
        }

        // phần này của thằng Bò cấm động vào :v
        [HttpPost("/api/StudyProcesses/get-language-class-of-learner")]
        public async Task<ActionResult<IEnumerable<StudyProcessViewModel>>> GetAllClassOfLearner(string learnerId)
        {
            return await Task.FromResult(_studyProcessService.GetAllClassOfLearner(learnerId));
        }
        [HttpPost("/api/StudyProcesses/get-learner-for-receipt")]
        public async Task<ActionResult<IEnumerable<StudyProcessViewModel>>> GetLearnerForReceipt()
        {
            return await Task.FromResult(_studyProcessService.GetLearnerForReceipt());
        }

        // Bò đã hạ code lấy học viên chưa đóng tiền học phí theo lớp tại đây đừng có động vào
        [HttpPost("/api/StudyProcesses/get-learner-not-paid-tuition")]
        public async Task<ActionResult<IEnumerable<StudyProcessViewModel>>> GetLearNotPaidTuiTion(int month, int year, string classId)
        {
            return await Task.FromResult(_studyProcessService.GetLearNotPaidTuiTion(month, year, classId));
        }
        // The End
        private bool StudyProcessExists(int id)
        {
            return _studyProcessService.IsExists(id);
        }

        [HttpPost]
        [Route("get-by-classId")]
        public async Task<ActionResult<IEnumerable<StudyProcessViewModel>>> GetByClassId(string classId = "", int status=1)
        {
            return await Task.FromResult(_studyProcessService.GetAllInClass(classId, status));
        }


        
    }
}
