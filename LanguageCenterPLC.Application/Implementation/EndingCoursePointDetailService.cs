using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageCenterPLC.Application.Implementation
{
    public class EndingCoursePointDetailService : IEndingCoursePointDetailService
    {
        private readonly IRepository<EndingCoursePointDetail, int> _endingCoursePointDetailRepository;
        private readonly IRepository<EndingCoursePoint, int> _endingCoursePoinRepository;
        private readonly IRepository<Learner, string> _learnerRepository;
        private readonly IRepository<StudyProcess, int> _studyProcessRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IRepository<LanguageClass, string> _languageclassRepository;
        private readonly IRepository<Lecturer, int> _lecturerRepository;


        public EndingCoursePointDetailService(IRepository<EndingCoursePointDetail, int> endingCoursePointDetailRepository,
          IUnitOfWork unitOfWork, IRepository<Learner, string> learnerRepository, IRepository<StudyProcess, int> studyProcessRepository, 
          IRepository<EndingCoursePoint, int> endingCoursePoinRepository, AppDbContext context,
          IRepository<Lecturer, int> lecturerRepository, IRepository<LanguageClass, string> languageclassRepository)
        {
            _endingCoursePointDetailRepository = endingCoursePointDetailRepository;
            _endingCoursePoinRepository = endingCoursePoinRepository;
            _learnerRepository = learnerRepository;
            _studyProcessRepository = studyProcessRepository;
            _unitOfWork = unitOfWork;
            _context = context;
            _languageclassRepository = languageclassRepository;
            _lecturerRepository = lecturerRepository;
        }


        public bool Add(EndingCoursePointDetailViewModel endingCoursePointDetailVm)
        {
            try
            {
                var endingCoursePointDetail = Mapper.Map<EndingCoursePointDetailViewModel, EndingCoursePointDetail>(endingCoursePointDetailVm);

                _endingCoursePointDetailRepository.Add(endingCoursePointDetail);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddRange()
        {
            try
            {
                // tìm id endingpoint lớn nhất để lấy giá trị thêm vào chi tiết
                var endingCoursePoint = _endingCoursePoinRepository.FindAll();
                int endingCoursePointId = 0;
                string languageId = "";
                foreach (var item in endingCoursePoint)
                {
                    if (endingCoursePointId < item.Id)
                    {
                        endingCoursePointId = item.Id;
                        languageId = item.LanguageClassId;
                    }

                }

                var learner = _studyProcessRepository.FindAll();


                //qwery ra điều kiện cần
                var learnerQwery = learner.Where(x => x.LanguageClassId == languageId).ToList();
                foreach (var item in learnerQwery)
                {
                    var endingCoursePointDetail = new EndingCoursePointDetail();
                    endingCoursePointDetail.ListeningPoint = 0;
                    endingCoursePointDetail.SayingPoint = 0;
                    endingCoursePointDetail.ReadingPoint = 0;
                    endingCoursePointDetail.WritingPoint = 0;
                    endingCoursePointDetail.TotalPoint = 0;
                    endingCoursePointDetail.DateCreated = DateTime.Now;
                    endingCoursePointDetail.SortOrder = 0;
                    endingCoursePointDetail.LearnerId = item.LearnerId;
                    endingCoursePointDetail.EndingCoursePointId = endingCoursePointId;
                    endingCoursePointDetail.Status = (Status)1;
                    _endingCoursePointDetailRepository.Add(endingCoursePointDetail);
                    _unitOfWork.Commit();

                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var endingCoursePointDetail = _endingCoursePointDetailRepository.FindById(id);

                _endingCoursePointDetailRepository.Remove(endingCoursePointDetail);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<EndingCoursePointDetailViewModel> GetAll()
        {
            var endingCoursePointDetail = _endingCoursePointDetailRepository.FindAll().ToList();
            var endingCoursePointDetailViewModels = Mapper.Map<List<EndingCoursePointDetailViewModel>>(endingCoursePointDetail);
            return endingCoursePointDetailViewModels;
        }

        public List<EndingCoursePointDetailViewModel> GetAllWithConditions(int endingPointId)
        {
             
            var endingPointPointDetail = _endingCoursePointDetailRepository.FindAll().Where(x => x.EndingCoursePointId == endingPointId);
            var endingCoursePointDetailViewModels = Mapper.Map<List<EndingCoursePointDetailViewModel>>(endingPointPointDetail);
            foreach (var item in endingCoursePointDetailViewModels)
            {
                string name = _learnerRepository.FindById(item.LearnerId).FirstName + ' ' + _learnerRepository.FindById(item.LearnerId).LastName;
                DateTime briday = _learnerRepository.FindById(item.LearnerId).Birthday;
                bool sex = _learnerRepository.FindById(item.LearnerId).Sex;
                string cardId = _learnerRepository.FindById(item.LearnerId).CardId;

                item.LearnerName = name;
                item.LearnerBriday = briday;
                item.LearnerSex = sex;
                item.LearnerCardId = cardId;

            }
            return endingCoursePointDetailViewModels;
        }

        public EndingCoursePointDetailViewModel GetById(int id)
        {
            var endingCoursePointDetail = _endingCoursePointDetailRepository.FindById(id);
            var endingCoursePointDetailViewModels = Mapper.Map<EndingCoursePointDetailViewModel>(endingCoursePointDetail);
            return endingCoursePointDetailViewModels;
        }

        public bool IsExists(int id)
        {
            var endingCoursePointDetail = _endingCoursePointDetailRepository.FindById(id);
            return (endingCoursePointDetail == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(EndingCoursePointDetailViewModel endingCoursePointDetailVm , Guid userId)
        {
            try
            {
                var endingCoursePointDetail = Mapper.Map<EndingCoursePointDetailViewModel, EndingCoursePointDetail>(endingCoursePointDetailVm);
                endingCoursePointDetail.DateModified = DateTime.Now;
                endingCoursePointDetail.TotalPoint = endingCoursePointDetail.ListeningPoint + endingCoursePointDetail.SayingPoint 
                    + endingCoursePointDetail.ReadingPoint + endingCoursePointDetail.WritingPoint;
                endingCoursePointDetail.AveragePoint = (endingCoursePointDetail.ListeningPoint + endingCoursePointDetail.SayingPoint
                    + endingCoursePointDetail.ReadingPoint + endingCoursePointDetail.WritingPoint) / 4;
                _endingCoursePointDetailRepository.Update(endingCoursePointDetail);
                _context.SaveChanges();

                UpdateSortIndexRange(endingCoursePointDetail.EndingCoursePointId);
                _context.SaveChanges();
                LogSystem logSystem = new LogSystem();
                logSystem.PeriodicPointId = endingCoursePointDetail.EndingCoursePointId;
                logSystem.LecturerId = _endingCoursePoinRepository.FindById(endingCoursePointDetail.EndingCoursePointId).LecturerId;
                logSystem.UserId = userId;
                logSystem.Content = "Cập nhật điểm cuối khóa - Học viên: " + _learnerRepository.FindById(endingCoursePointDetail.LearnerId).FirstName + " " +
                   _learnerRepository.FindById(endingCoursePointDetail.LearnerId).LastName
                   + "- Lớp: " + _languageclassRepository.FindById(_endingCoursePoinRepository.FindById(endingCoursePointDetail.EndingCoursePointId).LanguageClassId).Name;
                logSystem.DateCreated = DateTime.Now;
                logSystem.DateModified = DateTime.Now;
                logSystem.IsManagerPointLog = true;
                _context.LogSystems.Add(logSystem);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateSortIndexRange(int endingPointId)
        {
            try
            {
                var allAverPoint = _context.EndingCoursePointDetails.Where(x => x.EndingCoursePointId == endingPointId && x.Status == Status.Active).OrderByDescending(x => x.TotalPoint).ToList();
                var totalPoint = _context.EndingCoursePointDetails.Where(x => x.EndingCoursePointId == endingPointId && x.Status == Status.Active)
                .Select(x=>x.TotalPoint).Distinct().OrderByDescending(x=>x).ToList();

                for (int i = 0; i < allAverPoint.Count(); i++)
                {
                   
                    var temp = allAverPoint[i];
                   for (int j = 0; j < totalPoint.Count(); j++)
                   {
                       if (temp.TotalPoint == totalPoint[j])
                       {
                           temp.SortOrder = j+1;

                      }                     
                   }
                    _context.EndingCoursePointDetails.Update(temp);

                }
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
