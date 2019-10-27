using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
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

        private readonly IUnitOfWork _unitOfWork;

        public EndingCoursePointService(IRepository<EndingCoursePoint, int> endingCoursePointRepository,
          IUnitOfWork unitOfWork)
        {
            _endingCoursePointRepository = endingCoursePointRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(EndingCoursePointViewModel endingCoursePointVm)
        {
            try
            {
                var endingCoursePoint = Mapper.Map<EndingCoursePointViewModel, EndingCoursePoint>(endingCoursePointVm);

                _endingCoursePointRepository.Add(endingCoursePoint);

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

        public List<EndingCoursePointViewModel> GetAllWithConditions()
        {
            throw new NotImplementedException();
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
