using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnersController : ControllerBase
    {
        private readonly ILearnerService _learnerService;

        public LearnersController(ILearnerService learnerService)
        {
            _learnerService = learnerService;
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
                        learner.DateModified = DateTime.Now;
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
    }
}
