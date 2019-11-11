using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
                lecturer.DateCreated = DateTime.Now;
                string cardId = _lecturerRepository.FindAll().OrderByDescending(x => x.DateCreated).First().CardId;
                lecturer.CardId = cardId.Substring(2);

                int newCardId = Convert.ToInt32(lecturer.CardId) + 1;

                cardId = newCardId.ToString();
                while (cardId.Length < 7)
                {
                    cardId = "0" + cardId;
                }
                lecturer.CardId = "GV" + cardId;

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


        public List<LecturerViewModel> GetAllTutors()
        {
            List<Lecturer> lecturers = _lecturerRepository.FindAll().Where(x => x.IsTutor == true && x.Status == Status.Active).ToList();
            var lecturerViewModels = Mapper.Map<List<LecturerViewModel>>(lecturers);
            return lecturerViewModels;
        }



        public List<LecturerViewModel> GetAllWithConditions(string keyword, string position, int status)
        {
            

            var query = _lecturerRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))                 // tìm kiếm tên
            {
                query = query.Where(x => x.CardId.Contains(keyword) || x.LastName.Contains(keyword) || x.FirstName.Contains(keyword) || x.Phone.Contains(keyword) || x.Email.Contains(keyword));

            }

            Status _status = (Status)status;                      // tìm kiếm trạng thái
            if (_status == Status.Active || _status == Status.InActive)    // hoạt động or nghỉ    // kp thì là tất cả
            {
                query = query.Where(x => x.Status == _status);
            }
            if (position != "Tất cả")                 // tìm kiếm chức vụ
            {
                if (position != "Trợ giảng")
                {
                    query = query.Where(x => x.Position == position);
                } else
                {
                    query = query.Where(x => x.Position == position || x.IsTutor == true);
                }
            }


            var lecturerViewModels = Mapper.Map<List<LecturerViewModel>>(query);
            return lecturerViewModels;
        }

        public LecturerViewModel GetByCardId(string keyword)
        {
            var query = _lecturerRepository.FindAll().Where(x => x.CardId == keyword).Single();
            var lecturerViewModels = Mapper.Map<LecturerViewModel>(query);
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
                lecturer.DateModified = DateTime.Now;
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
