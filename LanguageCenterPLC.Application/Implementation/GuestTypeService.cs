using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class GuestTypeService : IGuestTypeService
    {
        private readonly IRepository<GuestType, int> _guestTypeRepository;

        private readonly IUnitOfWork _unitOfWork;

        public GuestTypeService(IRepository<GuestType, int> guestTypeRepository,
           IUnitOfWork unitOfWork)
        {
            _guestTypeRepository = guestTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(GuestTypeViewModel guestTypeViewModel)
        {
            try
            {
                var guestType = Mapper.Map<GuestTypeViewModel, GuestType>(guestTypeViewModel);

                _guestTypeRepository.Add(guestType);

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
                var guestType = _guestTypeRepository.FindById(id);

                _guestTypeRepository.Remove(guestType);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<GuestTypeViewModel> GetAll()
        {
            List<GuestType> guestTypes = _guestTypeRepository.FindAll().ToList();
            var guestTypesViewModel = Mapper.Map<List<GuestTypeViewModel>>(guestTypes);
            return guestTypesViewModel;
        }

        public List<GuestTypeViewModel> GetAllWithConditions(string keyword, int status)
        {
            var query = _guestTypeRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            Status _status = (Status)status;

            if (_status == Status.Active || _status == Status.InActive)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.Name);
            }

            var guestTypesViewModel = Mapper.Map<List<GuestTypeViewModel>>(query);

            return guestTypesViewModel;
        }

       

        public GuestTypeViewModel GetById(int Id)
        {
            var guestType = _guestTypeRepository.FindById(Id);
            var guestTypeViewModel = Mapper.Map<GuestTypeViewModel>(guestType);
            return guestTypeViewModel;
        }

        public bool IsExists(int id)
        {
            var guestType = _guestTypeRepository.FindById(id);
            return (guestType == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(GuestTypeViewModel guestTypeViewModel)
        {
            try
            {
                var guestType = Mapper.Map<GuestTypeViewModel, GuestType>(guestTypeViewModel);
                _guestTypeRepository.Update(guestType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
