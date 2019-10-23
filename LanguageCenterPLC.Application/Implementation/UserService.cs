using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<AppUser, Guid> _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository<AppUser, Guid> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public bool Add(AppUserViewModel userVm)
        {
            try
            {
                var user = Mapper.Map<AppUserViewModel, AppUser>(userVm);

                _userRepository.Add(user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var user = _userRepository.FindById(new Guid(id));

                _userRepository.Remove(user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<AppUserViewModel> GetAll()
        {
            List<AppUser> user = _userRepository.FindAll().ToList();
            var userViewModel = Mapper.Map<List<AppUserViewModel>>(user);
            return userViewModel;
        }

        public AppUserViewModel GetById(string id)
        {
            var user = _userRepository.FindById(new Guid(id));
            var userViewModel = Mapper.Map<AppUserViewModel>(user);
            return userViewModel;
        }

        public bool IsExists(string id)
        {
            var user = _userRepository.FindById(new Guid(id));
            return (user == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(AppUserViewModel userVm)
        {
            try
            {
                var user = Mapper.Map<AppUserViewModel, AppUser>(userVm);
                _userRepository.Update(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
