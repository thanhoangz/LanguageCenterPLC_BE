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

        public List<LanguageClassViewModel> GetAllWithConditions(DateTime? start, DateTime? end, string keyword = "", int status = 1)
        {
            var query = _languageClassRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            Status _status = (Status)status;
            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.Name);
            }

            /*tìm các lớp học có ngày bắt đầu nằm trong khoảng ngày truyền vào. */
            if (start != null && end != null)
            {
                query = query.Where(x => x.StartDay >= start && x.EndDay <= end);
            }

            var languageClassViewModel = Mapper.Map<List<LanguageClassViewModel>>(query);

            return languageClassViewModel;
        }


        public LanguageClassViewModel GetById(string id)
        {
            var courses = _courseRepository.FindAll().ToList();
            var languageClasse = _languageClassRepository.FindById(id);
            var languageClassesViewModel = Mapper.Map<LanguageClassViewModel>(languageClasse);
            foreach (var item in courses)
            {
                if (languageClassesViewModel.CourseId == item.Id)
                {
                    languageClassesViewModel.CourseName = item.Name;
                    break;
                }
            }
            return languageClassesViewModel;
        }

        public bool IsExists(string id)
        {
            var languageClasse = _languageClassRepository.FindById(id);
            return (languageClasse == null) ? false : true;
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
