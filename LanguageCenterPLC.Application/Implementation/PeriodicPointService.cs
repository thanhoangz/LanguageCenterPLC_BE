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
    public class PeriodicPointService : IPeriodicPointService
    {
        private readonly IRepository<PeriodicPoint, int> _periodicPointRepository;
        private readonly IRepository<Lecturer, int> _lecturerRepository;
        private readonly IRepository<LanguageClass, string> _languageclassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;


        public PeriodicPointService(IRepository<PeriodicPoint, int> periodicPointRepository,
            IRepository<Lecturer, int> lecturerRepository, IRepository<LanguageClass, string> languageclassRepository,
          IUnitOfWork unitOfWork, AppDbContext context)
        {
            _periodicPointRepository = periodicPointRepository;
            _lecturerRepository = lecturerRepository;
            _languageclassRepository = languageclassRepository;
            _unitOfWork = unitOfWork;
            _context = context;

        }
        public bool Add(PeriodicPointViewModel periodicPointVm)
        {
            try
            {
                var periodicPoint = Mapper.Map<PeriodicPointViewModel, PeriodicPoint>(periodicPointVm);

                _periodicPointRepository.Add(periodicPoint);
                LogSystem logSystem = new LogSystem();
                logSystem.PeriodicPointId = periodicPoint.Id;
                logSystem.UserId = periodicPoint.AppUserId;
                logSystem.LecturerId = periodicPoint.LecturerId;
                logSystem.Content = "Tạo bảng điểm định kì - Lớp: " + _languageclassRepository.FindById(periodicPoint.LanguageClassId).Name + " - Tuần: " + periodicPoint.Week;
                logSystem.DateCreated = DateTime.Now;
                logSystem.DateModified = DateTime.Now;
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
                var periodicPoint = _periodicPointRepository.FindById(id);
                _periodicPointRepository.Remove(periodicPoint);
              
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PeriodicPointViewModel> GetAll()
        {
            var periodicPoint = _periodicPointRepository.FindAll().ToList();
            var periodicPointViewModels = Mapper.Map<List<PeriodicPointViewModel>>(periodicPoint);
            return periodicPointViewModels;
        }

        public List<PeriodicPointViewModel> GetAllWithConditions(string languageClassId)
        {
            var periodicPoint = _periodicPointRepository.FindAll().Where(x => x.LanguageClassId == languageClassId).OrderBy(x=>x.Week);
            var periodicPointViewModels = Mapper.Map<List<PeriodicPointViewModel>>(periodicPoint);
            foreach (var item in periodicPointViewModels)
            {
                string lecturerName = _lecturerRepository.FindById(item.LecturerId).FirstName + ' ' + _lecturerRepository.FindById(item.LecturerId).LastName;
                string languageName = _languageclassRepository.FindById(item.LanguageClassId).Name;

                item.LecturerName = lecturerName;
                item.LanguageClassName = languageName;
            }
            return periodicPointViewModels;
        }

        public PeriodicPointViewModel GetById(int id)
        {
            var periodicPoint = _periodicPointRepository.FindById(id);
            var periodicPointViewModel = Mapper.Map<PeriodicPointViewModel>(periodicPoint);
            string lecturerName = _lecturerRepository.FindById(periodicPointViewModel.LecturerId).FirstName + ' ' + _lecturerRepository.FindById(periodicPointViewModel.LecturerId).LastName;
            string languageName = _languageclassRepository.FindById(periodicPointViewModel.LanguageClassId).Name;

            periodicPointViewModel.LecturerName = lecturerName;
            periodicPointViewModel.LanguageClassName = languageName;
            
            return periodicPointViewModel;
        }

        public bool IsExists(int id)
        {
            var periodicPoint = _periodicPointRepository.FindById(id);
            return (periodicPoint == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(PeriodicPointViewModel periodicPointVm)
        {
            try
            {
                var periodicPoint = Mapper.Map<PeriodicPointViewModel, PeriodicPoint>(periodicPointVm);
                periodicPoint.DateModified = DateTime.Now;
                _periodicPointRepository.Update(periodicPoint);             
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
