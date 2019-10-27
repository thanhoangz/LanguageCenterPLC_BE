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
    public class EndingCoursePointDetailService : IEndingCoursePointDetailService
    {
        private readonly IRepository<EndingCoursePointDetail, int> _endingCoursePointDetailRepository;

        private readonly IUnitOfWork _unitOfWork;

        public EndingCoursePointDetailService(IRepository<EndingCoursePointDetail, int> endingCoursePointDetailRepository,
          IUnitOfWork unitOfWork)
        {
            _endingCoursePointDetailRepository = endingCoursePointDetailRepository;
            _unitOfWork = unitOfWork;
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

        public List<EndingCoursePointDetailViewModel> GetAllWithConditions()
        {
            throw new NotImplementedException();
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

        public bool Update(EndingCoursePointDetailViewModel endingCoursePointDetailVm)
        {
            try
            {
                var endingCoursePointDetail = Mapper.Map<EndingCoursePointDetailViewModel, EndingCoursePointDetail>(endingCoursePointDetailVm);
                endingCoursePointDetail.DateModified = DateTime.Now;
                _endingCoursePointDetailRepository.Update(endingCoursePointDetail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
