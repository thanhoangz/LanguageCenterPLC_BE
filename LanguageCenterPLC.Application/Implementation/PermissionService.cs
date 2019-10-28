﻿using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class PermissionService : IPermissionService
    {

        private readonly IRepository<Permission, int> _permissionRepository;
        private readonly IRepository<Function, string> _functionRepository;
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IRepository<Permission, int> permissionRepository,
            IRepository<Function, string> functionRepository, IUnitOfWork unitOfWork,
            AppDbContext context)
        {
            _permissionRepository = permissionRepository;
            _functionRepository = functionRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public bool Add(PermissionViewModel permissionVm)
        {
            try
            {
                var permission = Mapper.Map<PermissionViewModel, Permission>(permissionVm);
                _permissionRepository.Add(permission);

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
                var permission = _permissionRepository.FindById(id);

                _permissionRepository.Remove(permission);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PermissionViewModel> GetAll()
        {
            var permissions = _permissionRepository.FindAll();


            var permissionsViewModel = Mapper.Map<List<PermissionViewModel>>(permissions);
            foreach (var item in permissionsViewModel)
            {
                item.FunctionName = _functionRepository.FindById(item.FunctionId).Name;
                item.UserName = _context.AppUsers.Where(x=>x.Id== item.AppUserId).FirstOrDefault().UserName;
            }

            return permissionsViewModel;
        }

        public List<PermissionViewModel> GetAllByUser(Guid userId)
        {
            var permissions = _permissionRepository.FindAll().Where(x => x.AppUserId == userId);

            var permissionsViewModel = Mapper.Map<List<PermissionViewModel>>(permissions);

            foreach (var item in permissionsViewModel)
            {
                item.FunctionName = _functionRepository.FindById(item.FunctionId).Name;
                item.UserName = _context.AppUsers.Where(x => x.Id == item.AppUserId).FirstOrDefault().UserName;
            }

            return permissionsViewModel;
        }

        public PermissionViewModel GetById(int id)
        {
            var permission = _permissionRepository.FindById(id);
            var permissionViewModel = Mapper.Map<PermissionViewModel>(permission);
            return permissionViewModel;
        }

        public bool IsExists(int id)
        {
            var permission = _permissionRepository.FindById(id);
            return (permission == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(PermissionViewModel permissionVm)
        {
            try
            {
                var permission = Mapper.Map<PermissionViewModel, Permission>(permissionVm);
                _permissionRepository.Update(permission);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}