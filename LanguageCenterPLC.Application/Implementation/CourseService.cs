using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;

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

        public void Create(CourseViewModel courseVm)
        {
            var course = Mapper.Map<CourseViewModel, Course>(courseVm);
            _courseRepository.Add(course);
        }

        public void Delete(int courseId)
        {
            throw new NotImplementedException();
        }

        public PagedResult<CourseViewModel> GetAllPaging(string keyword, int status, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public CourseViewModel GetDetail(int courseId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(CourseViewModel courseVm)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(int courseId, Status status)
        {
            throw new NotImplementedException();
        }
    }
}
