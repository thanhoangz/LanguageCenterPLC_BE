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
    public class PeriodicPointDetailService : IPeriodicPointDetailService
    {
        private readonly IRepository<PeriodicPointDetail, int> _periodicPointDetailRepository;

        private readonly IUnitOfWork _unitOfWork;

        public PeriodicPointDetailService(IRepository<PeriodicPointDetail, int> periodicPointDetailRepository,
          IUnitOfWork unitOfWork)
        {
            _periodicPointDetailRepository = periodicPointDetailRepository;
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

        public List<PeriodicPointViewModel> GetAllWithConditions()
        {
            throw new NotImplementedException();
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
                periodicPointDetail.DateModified = DateTime.Now;
                _periodicPointDetailRepository.Update(periodicPointDetail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
