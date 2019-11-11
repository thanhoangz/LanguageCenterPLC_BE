using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool Add(PersonnelViewModel personnelVm)     // add nhân viên
        {
            try
            {
                var personnel = Mapper.Map<PersonnelViewModel, Personnel>(personnelVm);

                #region sinh mã cardId tăng tự động
                personnel.DateCreated = DateTime.Now;
                personnel.Id = TextHelper.RandomString(10);
                string cardId = _personelRepository.FindAll().OrderByDescending(x => x.DateCreated).First().CardId;
                personnel.CardId = cardId.Substring(2);

                int newCardId = Convert.ToInt32(personnel.CardId) + 1;
                
                cardId = newCardId.ToString();
                while (cardId.Length < 5)
                {
                    cardId = "0" + cardId;
                }
                personnel.CardId = "NV" + cardId;
                personnel.SalaryOfDay = personnel.BasicSalary / 25;
                #endregion
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

        public List<PersonnelViewModel> GetAllWithConditions(string keyword,string position, int status)
        {
            var query = _personelRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))                 // tìm kiếm tên
            {
                query = query.Where(x => x.CardId.Contains(keyword) || x.LastName.Contains(keyword) || x.FirstName.Contains(keyword) || x.Phone.Contains(keyword) || x.Email.Contains(keyword));

            }

            Status _status = (Status)status;                      // tìm kiếm trạng thái
            if (_status == Status.Active || _status == Status.InActive)    // hoạt động or nghỉ    // kp thì là tất cả
            {
                query = query.Where(x => x.Status == _status);
            }
            if ( position != "Tất cả")                 // tìm kiếm chức vụ
            {
                query = query.Where(x => x.Position == position );
            }


            var personnelViewModels = Mapper.Map<List<PersonnelViewModel>>(query);
            return personnelViewModels;
        }

        public PersonnelViewModel GetByCardId(string keyword)
        {
            var query = _personelRepository.FindAll().Where(x => x.CardId == keyword).Single();

            var personnelViewModels = Mapper.Map<PersonnelViewModel>(query);
            return personnelViewModels;
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
