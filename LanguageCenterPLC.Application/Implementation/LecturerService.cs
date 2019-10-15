using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageCenterPLC.Application.Implementation
{
    public class LecturerService : ILecturerService
    {
        private readonly IRepository<Lecturer, int> _lecturerRepository;

        private readonly IUnitOfWork _unitOfWork;

        public LecturerService(IRepository<Lecturer, int> lecturerRepository,
          IUnitOfWork unitOfWork)
        {
            _lecturerRepository = lecturerRepository;
            _unitOfWork = unitOfWork;
        }


        public bool Add(LecturerViewModel lecturerVm)
        {
            try
            {
                var lecturer = Mapper.Map<LecturerViewModel, Lecturer>(lecturerVm);

                _lecturerRepository.Add(lecturer);

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
                var lecturer = _lecturerRepository.FindById(id);

                _lecturerRepository.Remove(lecturer);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<LecturerViewModel> GetAll()
        {
            List<Lecturer> lecturers = _lecturerRepository.FindAll().ToList();
            var lecturerViewModels = Mapper.Map<List<LecturerViewModel>>(lecturers);
            return lecturerViewModels;
        }

        public List<LecturerViewModel> GetAllWithConditions(string cardId, string keyword, int status, string position)
        {
            var query = _lecturerRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.LastName.Contains(keyword));
            }

            Status _status = (Status)status;

            if (_status == Status.Active || _status == Status.InActive)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.LastName);
            }

            var lecturerViewModels = Mapper.Map<List<LecturerViewModel>>(query);

            return lecturerViewModels;
        }

        public LecturerViewModel GetById(int id)
        {
            var lecturer = _lecturerRepository.FindById(id);
            var lecturerViewModels = Mapper.Map<LecturerViewModel>(lecturer);
            return lecturerViewModels;
        }

        public bool IsExists(int id)
        {
            var lecturer = _lecturerRepository.FindById(id);
            return (lecturer == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(LecturerViewModel lecturerVm)
        {
            try
            {
                var lecturer = Mapper.Map<LecturerViewModel, Lecturer>(lecturerVm);
                _lecturerRepository.Update(lecturer);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
