﻿using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageCenterPLC.Application.Implementation
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IRepository<Personnel, string> _personelRepository;

        private readonly IUnitOfWork _unitOfWork;
        public PersonnelService(IRepository<Personnel, string> personelRepository, IUnitOfWork unitOfWork)
        {
            _personelRepository = personelRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(PersonnelViewModel personnelVm)
        {
            try
            {
                var personnel = Mapper.Map<PersonnelViewModel, Personnel>(personnelVm);

                _personelRepository.Add(personnel);

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
                var personnel = _personelRepository.FindById(id);

                _personelRepository.Remove(personnel);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PersonnelViewModel> GetAll()
        {
            List<Personnel> personnels = _personelRepository.FindAll().ToList();
            var personnelViewModels = Mapper.Map<List<PersonnelViewModel>>(personnels);
            return personnelViewModels;
        }

        public List<PersonnelViewModel> GetAllWithConditions(DateTime briday, string keyword, int status, int sex)
        {
            throw new NotImplementedException();
        }

        public PersonnelViewModel GetById(string id)
        {
            var personnel = _personelRepository.FindById(id);
            var personnelViewModel = Mapper.Map<PersonnelViewModel>(personnel);
            return personnelViewModel;
        }

        public bool IsExists(string id)
        {
            var personnel = _personelRepository.FindById(id);
            return (personnel == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(PersonnelViewModel personnelVm)
        {
            try
            {
                var personnel = Mapper.Map<PersonnelViewModel, Personnel>(personnelVm);
                _personelRepository.Update(personnel);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}