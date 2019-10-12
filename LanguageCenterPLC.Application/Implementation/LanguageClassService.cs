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
    public class LanguageClassService : ILanguageClassService
    {
        private readonly IRepository<LanguageClass, string> _languageClassRepository;
        private readonly IRepository<Course, int> _courseRepository;

        private readonly IUnitOfWork _unitOfWork;

        public LanguageClassService(IRepository<LanguageClass, string> languageClassRepository, IRepository<Course, int> courseRepository, IUnitOfWork unitOfWork)
        {
            _languageClassRepository = languageClassRepository;
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(LanguageClassViewModel languageClassVm)
        {
            try
            {
                var languageClass = Mapper.Map<LanguageClassViewModel, LanguageClass>(languageClassVm);

                _languageClassRepository.Add(languageClass);

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
                var languageClass = _languageClassRepository.FindById(id);

                _languageClassRepository.Remove(languageClass);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<LanguageClassViewModel> GetAll()
        {
            var courses = _courseRepository.FindAll().ToList();

            var languageClasses = _languageClassRepository.FindAll().ToList();

            var languageClassViewModel = Mapper.Map<List<LanguageClassViewModel>>(languageClasses);

            foreach (var item in languageClassViewModel)
            {
                string name = _courseRepository.FindById(item.CourseId).Name;
                item.CourseName = name;
            }

            return languageClassViewModel;
        }

        public List<LanguageClassViewModel> GetAllWithConditions(DateTime start, string keyword = "", int status = 1)
        {
            var query = _languageClassRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            Status _status = (Status)status;

            query = query.Where(x => x.Status == _status).OrderBy(x => x.Name);

            // do something....

            var languageClassViewModel = Mapper.Map<List<LanguageClassViewModel>>(query);

            return languageClassViewModel;
        }

        public List<LanguageClassViewModel> GetAllWithConditions(string keyword, int status, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public LanguageClassViewModel GetById(string id)
        {
            var languageClasse = _languageClassRepository.FindById(id);
            var languageClassesViewModel = Mapper.Map<LanguageClassViewModel>(languageClasse);
            return languageClassesViewModel;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(LanguageClassViewModel languageClassVm)
        {
            try
            {
                var languageClasse = Mapper.Map<LanguageClassViewModel, LanguageClass>(languageClassVm);
                _languageClassRepository.Update(languageClasse);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
