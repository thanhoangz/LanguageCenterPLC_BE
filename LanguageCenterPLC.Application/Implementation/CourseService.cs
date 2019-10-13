using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace LanguageCenterPLC.Application.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course, int> _courseRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IRepository<Course, int> courseRepository,
           IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(CourseViewModel courseVm)
        {
            try
            {
                var course = Mapper.Map<CourseViewModel, Course>(courseVm);

                _courseRepository.Add(course);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Delete(int courseId)
        {
            try
            {
                var course = _courseRepository.FindById(courseId);

                _courseRepository.Remove(course);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<CourseViewModel> GetAll()
        {
            List<Course> courses = _courseRepository.FindAll().ToList();
            var coursesViewModel = Mapper.Map<List<CourseViewModel>>(courses);
            return coursesViewModel;
        }

        public List<CourseViewModel> GetAllWithConditions(string keyword, int status)
        {
            var query = _courseRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            Status _status = (Status)status;

            if (_status == Status.Active || _status == Status.InActive)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.Name);
            }

            var coursesViewModel = Mapper.Map<List<CourseViewModel>>(query);

            return coursesViewModel;
        }

        public PagedResult<CourseViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex)
        {
            var query = _courseRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            Status _status = (Status)status;

            query = query.Where(x => x.Status == _status);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Price)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            var resultPaging = Mapper.Map<List<CourseViewModel>>(data);

            return new PagedResult<CourseViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = resultPaging,
                RowCount = totalRow
            };
        }

        public CourseViewModel GetById(int courseId)
        {
            var course = _courseRepository.FindById(courseId);
            var courseViewModel = Mapper.Map<CourseViewModel>(course);
            return courseViewModel;
        }

        public bool IsExists(int id)
        {
            var course = _courseRepository.FindById(id);
            return (course == null) ? false : true; 
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(CourseViewModel courseVm)
        {
            try
            {
                var course = Mapper.Map<CourseViewModel, Course>(courseVm);
                _courseRepository.Update(course);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStatus(int courseId, Status status)
        {
            try
            {
                var course = _courseRepository.FindById(courseId);
                course.Status = status;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
