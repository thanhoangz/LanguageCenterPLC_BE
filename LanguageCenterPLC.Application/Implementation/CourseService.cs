using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<bool> AddSync(CourseViewModel courseVm)
        {
            try
            {
                var course = Mapper.Map<CourseViewModel, Course>(courseVm);
                _courseRepository.Add(course);
                return new Task<bool>(() => true);
            }
            catch
            {

                return new Task<bool>(() => false);
            }


        }

        public Task<bool> Delete(int courseId)
        {
            try
            {
                var course = _courseRepository.FindById(courseId);
                _courseRepository.Remove(course);
                return new Task<bool>(() => true);
            }
            catch
            {
                return new Task<bool>(() => false);
            }

        }

        public Task<List<CourseViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public PagedResult<CourseViewModel> GetAllPaging(string keyword, int status, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<CourseViewModel> GetDetail(int courseId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Task<bool> UpdateStatusSync(int courseId, Status status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSync(CourseViewModel courseVm)
        {
            try
            {
                var course = Mapper.Map<CourseViewModel, Course>(courseVm);
                _courseRepository.Update(course);
                return new Task<bool>(() => true);
            }
            catch
            {

                return new Task<bool>(() => false);
            }
        }
    }
}
