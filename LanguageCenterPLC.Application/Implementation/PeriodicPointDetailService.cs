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
    public class PeriodicPointDetailService : IPeriodicPointDetailService
    {
        private readonly IRepository<PeriodicPointDetail, int> _periodicPointDetailRepository;
        private readonly IRepository<PeriodicPoint, int> _periodicPointRepository;
        private readonly IRepository<Learner, string> _learnerRepository;
        private readonly IRepository<StudyProcess, int> _studyProcessRepository;
        private readonly AppDbContext _context;



        private readonly IUnitOfWork _unitOfWork;

        public PeriodicPointDetailService(IRepository<PeriodicPointDetail, int> periodicPointDetailRepository, IRepository<PeriodicPoint,
            int> periodicPointRepository, IRepository<Learner, string> learnerRepository, IRepository<StudyProcess, int> studyProcessRepository,
          IUnitOfWork unitOfWork, AppDbContext context)
        {
            _periodicPointDetailRepository = periodicPointDetailRepository;
            _periodicPointRepository = periodicPointRepository;
            _learnerRepository = learnerRepository;
            _studyProcessRepository = studyProcessRepository;
            _unitOfWork = unitOfWork;
            _context = context;

        }
        public bool Add(PeriodicPointDetailViewModel periodicPointDetailVm)
        {
            try
            {

                var periodicPointDetail = Mapper.Map<PeriodicPointDetailViewModel, PeriodicPointDetail>(periodicPointDetailVm);

                _periodicPointDetailRepository.Add(periodicPointDetail);

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

                var periodicPoint = _periodicPointRepository.FindAll();
                int periodicPointId = 0;
                string languageId = "";
                foreach (var item in periodicPoint)
                {
                    if (periodicPointId < item.Id)
                    {
                        periodicPointId = item.Id;
                        languageId = item.LanguageClassId;
                    }

                }

                var learner = _studyProcessRepository.FindAll();


                //qwery ra điều kiện cần
                var learnerQwery = learner.Where(x => x.LanguageClassId == languageId).ToList();
                foreach (var item in learnerQwery)
                {
                    var periodicPointDetail = new PeriodicPointDetail();
                    periodicPointDetail.Point = 0;
                    periodicPointDetail.AveragePoint = 0;
                    periodicPointDetail.SortedByPoint = 0;
                    periodicPointDetail.SortedByAveragePoint = 0;
                    periodicPointDetail.LearnerId = item.LearnerId;
                    periodicPointDetail.PeriodicPointId = periodicPointId;
                    periodicPointDetail.DateCreated = DateTime.Now;
                    periodicPointDetail.Status = (Status)1;
                    _periodicPointDetailRepository.Add(periodicPointDetail);
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
                var periodicPointDetail = _periodicPointDetailRepository.FindById(id);

                _periodicPointDetailRepository.Remove(periodicPointDetail);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PeriodicPointDetailViewModel> GetAll()
        {
            var periodicPointDetail = _periodicPointDetailRepository.FindAll().ToList();
            var periodicPointDetailViewModels = Mapper.Map<List<PeriodicPointDetailViewModel>>(periodicPointDetail);
            return periodicPointDetailViewModels;
        }

        public List<PeriodicPointDetailViewModel> GetAllWithConditions(int periodicPointId)
        {

            var periodicPointDetail = _periodicPointDetailRepository.FindAll().Where(x => x.PeriodicPointId == periodicPointId);
            var periodicPointDetailViewModels = Mapper.Map<List<PeriodicPointDetailViewModel>>(periodicPointDetail);
            foreach (var item in periodicPointDetailViewModels)
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
            return periodicPointDetailViewModels;
        }

        public PeriodicPointDetailViewModel GetById(int id)
        {
            var periodicPointDetail = _periodicPointDetailRepository.FindById(id);
            var periodicPointDetailViewModels = Mapper.Map<PeriodicPointDetailViewModel>(periodicPointDetail);
            return periodicPointDetailViewModels;
        }

        public bool IsExists(int id)
        {
            var periodicPointDetail = _periodicPointDetailRepository.FindById(id);
            return (periodicPointDetail == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="periodicPointDetailVm"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public bool Update(PeriodicPointDetailViewModel periodicPointDetailVm, string classId)
        {

            List<PeriodicPointDetail> details = new List<PeriodicPointDetail>();
            var periodicPointDetail = _context.PeriodicPointDetails.ToList().Where(x => x.Id == periodicPointDetailVm.Id).Single();
         
            periodicPointDetail.DateModified = DateTime.Now;

            // đang xử lý test cập nhật điểm tb tổng
            var periodicPointInClass = _periodicPointRepository.FindAll().Where(x => x.LanguageClassId == classId && x.Id <= periodicPointDetailVm.PeriodicPointId);
            foreach (var item in periodicPointInClass)
            {
                var PointDetail = _periodicPointDetailRepository.FindAll().Where(x => x.PeriodicPointId == item.Id && x.LearnerId == periodicPointDetailVm.LearnerId);
                details.AddRange(PointDetail);
            }

            periodicPointDetail.AveragePoint = 0;
            foreach (var item in details)
            {
                if (item.Id != periodicPointDetail.Id)
                {
                    periodicPointDetail.AveragePoint += item.Point;

                }
            }
            periodicPointDetail.AveragePoint = (periodicPointDetail.AveragePoint + periodicPointDetailVm.Point) / details.Count();
            periodicPointDetail.Point = periodicPointDetailVm.Point;
            _context.PeriodicPointDetails.Update(periodicPointDetail);
            _context.SaveChanges();
            UpdateSortIndexRange(periodicPointDetail.PeriodicPointId);
            _context.SaveChanges();


            return false;

        }

        

        public bool UpdateSortIndexRange(int periodicPointId)
        {
            try
            {
                var allAverPoint = _context.PeriodicPointDetails.Where(x => x.PeriodicPointId == periodicPointId && x.Status == Status.Active).OrderByDescending(x => x.AveragePoint).ToList();
              

                for (int i = 0; i < allAverPoint.Count(); i++)
                {

                    var temp = allAverPoint[i];
                    temp.SortedByAveragePoint = i+1;
                    _context.PeriodicPointDetails.Update(temp);
                }
                _context.SaveChanges();

                var allPoint = _context.PeriodicPointDetails.Where(x => x.PeriodicPointId == periodicPointId && x.Status == Status.Active).OrderByDescending(x => x.Point).ToList();
                for (int i = 0; i < allPoint.Count(); i++)
                {
                    var temp = allPoint[i];
                    temp.SortedByPoint= i+1;
                    _context.PeriodicPointDetails.Update(temp);
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
