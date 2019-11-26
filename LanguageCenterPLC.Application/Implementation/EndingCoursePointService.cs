using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageCenterPLC.Application.Implementation
{
    public class EndingCoursePointService : IEndingCoursePointService
    {
        private readonly IRepository<EndingCoursePoint, int> _endingCoursePointRepository;
        private readonly IRepository<Lecturer, int> _lecturerRepository;
        private readonly IRepository<LanguageClass, string> _languageclassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;


        public EndingCoursePointService(IRepository<EndingCoursePoint, int> endingCoursePointRepository,
          IUnitOfWork unitOfWork, IRepository<Lecturer, int> lecturerRepository, IRepository<LanguageClass, string> languageclassRepository, AppDbContext context)
        {
            _endingCoursePointRepository = endingCoursePointRepository;
            _lecturerRepository = lecturerRepository;
            _languageclassRepository = languageclassRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public bool Add(EndingCoursePointViewModel endingCoursePointVm)
        {
            try
            {
                var endingCoursePoint = Mapper.Map<EndingCoursePointViewModel, EndingCoursePoint>(endingCoursePointVm);

                _endingCoursePointRepository.Add(endingCoursePoint);
                LogSystem logSystem = new LogSystem();
                logSystem.EndingCoursePointId = endingCoursePoint.Id;
                logSystem.UserId = endingCoursePoint.AppUserId;
                logSystem.LecturerId = endingCoursePoint.LecturerId;
                logSystem.Content = "Tạo bảng điểm cuối khóa lớp " + _languageclassRepository.FindById(endingCoursePoint.LanguageClassId).Name;
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

        public bool Delete(int id)
        {
            try
            {
                var endingCoursePoint = _endingCoursePointRepository.FindById(id);

                _endingCoursePointRepository.Remove(endingCoursePoint);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<EndingCoursePointViewModel> GetAll()
        {
            var endingCoursePoints = _endingCoursePointRepository.FindAll().ToList();
            var endingCoursePointViewModels = Mapper.Map<List<EndingCoursePointViewModel>>(endingCoursePoints);
            return endingCoursePointViewModels;
        }

        public EndingCoursePointViewModel GetAllWithConditions(string languageClassId)
        {
            var endingCoursePoints = _endingCoursePointRepository.FindAll().Where(x => x.LanguageClassId == languageClassId).SingleOrDefault();
           
            var endingCoursePointViewModels = Mapper.Map<EndingCoursePointViewModel>(endingCoursePoints);
            if (endingCoursePoints != null)
            {
                string lecturerName = _lecturerRepository.FindById(endingCoursePointViewModels.LecturerId).FirstName + ' ' + _lecturerRepository.FindById(endingCoursePointViewModels.LecturerId).LastName;
                string languageName = _languageclassRepository.FindById(endingCoursePointViewModels.LanguageClassId).Name;

                endingCoursePointViewModels.LecturerName = lecturerName;
                endingCoursePointViewModels.LanguageClassName = languageName;
            }
            else
            {
                endingCoursePointViewModels = null;
            }

            return endingCoursePointViewModels;

        }

        public EndingCoursePointViewModel GetById(int id)
        {
            var endingCoursePoint = _endingCoursePointRepository.FindById(id);
            var endingCoursePointViewModel = Mapper.Map<EndingCoursePointViewModel>(endingCoursePoint);
            return endingCoursePointViewModel;
        }

        public bool IsExists(int id)
        {
            var endingCoursePoint = _endingCoursePointRepository.FindById(id);
            return (endingCoursePoint == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(EndingCoursePointViewModel endingCoursePointVm)
        {
            try
            {
                var endingCoursePoint = Mapper.Map<EndingCoursePointViewModel, EndingCoursePoint>(endingCoursePointVm);
                endingCoursePoint.DateModified = DateTime.Now;
                _endingCoursePointRepository.Update(endingCoursePoint);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
