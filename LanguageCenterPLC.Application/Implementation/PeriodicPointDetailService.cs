using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
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




        private readonly IUnitOfWork _unitOfWork;

        public PeriodicPointDetailService(IRepository<PeriodicPointDetail, int> periodicPointDetailRepository, IRepository<PeriodicPoint,
            int> periodicPointRepository, IRepository<Learner, string> learnerRepository, IRepository<StudyProcess, int> studyProcessRepository,
          IUnitOfWork unitOfWork)
        {
            _periodicPointDetailRepository = periodicPointDetailRepository;
            _periodicPointRepository = periodicPointRepository;
            _learnerRepository = learnerRepository;
            _studyProcessRepository = studyProcessRepository;
            _unitOfWork = unitOfWork;
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
            
            var periodicPointDetail = _periodicPointDetailRepository.FindAll().Where(x=>x.PeriodicPointId==periodicPointId);
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
        
        public bool Update(PeriodicPointDetailViewModel periodicPointDetailVm)
        {
            try
            {
                var periodicPointDetail = Mapper.Map<PeriodicPointDetailViewModel, PeriodicPointDetail>(periodicPointDetailVm);

                 List<PeriodicPointDetail> details = new List<PeriodicPointDetail>();
                 periodicPointDetail.DateModified = DateTime.Now;
                 _periodicPointDetailRepository.Update(periodicPointDetail);

                /*// đang xử lý test cập nhật điểm tb tổng
                var periodicPointOfClass = _periodicPointRepository.FindAll().Where(x => x.LanguageClassId == classId && x.Id <= periodicPointDetailVm.PeriodicPointId);
                foreach (var item in periodicPointOfClass)
                {
                    var periodicPointDetailOfLearner = _periodicPointDetailRepository.FindAll().Where(x => x.PeriodicPointId == item.Id && x.LearnerId == periodicPointDetailVm.LearnerId);
                    details.AddRange(periodicPointDetailOfLearner);
                }
                foreach (var item in details)
                {
                    if (item.Id != periodicPointDetailVm.Id)
                    {
                        periodicPointDetailVm.AveragePoint += item.Point;
                    }
                }
                periodicPointDetailVm.AveragePoint = (periodicPointDetailVm.AveragePoint + periodicPointDetailVm.Point) / (details.Count());*/

                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateRange(int periodicPointId, string classId)
        {
            try
            {
                List<PeriodicPointDetail> details = new List<PeriodicPointDetail>();
                var periodicPointDetail = _periodicPointDetailRepository.FindAll().Where(x => x.PeriodicPointId == periodicPointId);

                var periodicPointDetailSX = _periodicPointDetailRepository.FindAll().Where(x => x.PeriodicPointId == periodicPointId).OrderByDescending(x=>x.SortedByPoint);

                var periodicPointDetailSXTB = _periodicPointDetailRepository.FindAll().Where(x => x.PeriodicPointId == periodicPointId).OrderByDescending(x => x.SortedByAveragePoint);

                var periodicPointOfClass = _periodicPointRepository.FindAll().Where(x => x.LanguageClassId == classId && x.Id <= periodicPointId);
                foreach (var item in periodicPointOfClass)
                {
                    var periodicPointDetailOfLearner = _periodicPointDetailRepository.FindAll().Where(x => x.PeriodicPointId == item.Id);
                    details.AddRange(periodicPointDetailOfLearner);
                }

                foreach (var item in periodicPointDetail)
                {
                    
                    decimal pointTB = 0;
                    var learner = new PeriodicPointDetail();
                    learner.Id = item.Id;
                    foreach (var detail in details)
                    {
                        if (detail.LearnerId == item.LearnerId)
                        {
                            pointTB += detail.Point;
                        }
                    }
                    learner.AveragePoint = pointTB;
                 // ngu người


                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
