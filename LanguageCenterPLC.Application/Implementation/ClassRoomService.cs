using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageCenterPLC.Application.Implementation
{
    public class ClassRoomService : IClassroomService
    {
        private readonly IRepository<Classroom, int> _classRoomRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ClassRoomService(IRepository<Classroom, int> classRoomRepository,
           IUnitOfWork unitOfWork)
        {
            _classRoomRepository = classRoomRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Add(ClassroomViewModel classroomVm)
        {
            try
            {
                var classRoom = Mapper.Map<ClassroomViewModel, Classroom>(classroomVm);

                _classRoomRepository.Add(classRoom);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int classroomId)
        {
            try
            {
                var classRoom = _classRoomRepository.FindById(classroomId);

                _classRoomRepository.Remove(classRoom);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ClassroomViewModel> GetAll()
        {
            List<Classroom> classRooms = _classRoomRepository.FindAll().ToList();
            var classRoomViewModel = Mapper.Map<List<ClassroomViewModel>>(classRooms);
            return classRoomViewModel;
        }

        public PagedResult<ClassroomViewModel> GetAllPaging(string keyword, int pageSize, int pageIndex)
        {
            var query = _classRoomRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            var totalRow = query.Count();
            var data = query.OrderBy(x => x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            var resultPaging = Mapper.Map<List<ClassroomViewModel>>(data);

            return new PagedResult<ClassroomViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = resultPaging,
                RowCount = totalRow
            };
        }

        public List<ClassroomViewModel> GetAllWithConditions(string keyword, int status)
        {
            var query = _classRoomRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            Status _status = (Status)status;
            if (_status == Status.Active || _status == Status.InActive)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.Name);
            }

            var classRoomViewModel = Mapper.Map<List<ClassroomViewModel>>(query);

            return classRoomViewModel;
        }

        public ClassroomViewModel GetById(int classroomId)
        {
            var classRoom = _classRoomRepository.FindById(classroomId);
            var classRoomviewModel = Mapper.Map<ClassroomViewModel>(classRoom);
            return classRoomviewModel;
        }

        public bool IsExists(int classroomId)
        {
            var classRoom = _classRoomRepository.FindById(classroomId);
            return (classRoom == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(ClassroomViewModel classroomVm)
        {
            try
            {
                var classRoom = Mapper.Map<ClassroomViewModel, Classroom>(classroomVm);
                _classRoomRepository.Update(classRoom);
                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
