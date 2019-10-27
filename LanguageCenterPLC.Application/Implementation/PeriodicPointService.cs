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
    public class PeriodicPointService : IPeriodicPointService
    {
        private readonly IRepository<PeriodicPoint, int> _periodicPointRepository;

        private readonly IUnitOfWork _unitOfWork;

        public PeriodicPointService(IRepository<PeriodicPoint, int> periodicPointRepository,
          IUnitOfWork unitOfWork)
        {
            _periodicPointRepository = periodicPointRepository;
            _unitOfWork = unitOfWork;
        }
        public bool Add(PeriodicPointViewModel periodicPointVm)
        {
            try
            {
                var periodicPoint = Mapper.Map<PeriodicPointViewModel, PeriodicPoint>(periodicPointVm);

                _periodicPointRepository.Add(periodicPoint);

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

        public List<PeriodicPointViewModel> GetAllWithConditions()
        {
            throw new NotImplementedException();
        }

        public PeriodicPointViewModel GetById(int id)
        {
            var periodicPoint = _periodicPointRepository.FindById(id);
            var periodicPointViewModel = Mapper.Map<PeriodicPointViewModel>(periodicPoint);
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
