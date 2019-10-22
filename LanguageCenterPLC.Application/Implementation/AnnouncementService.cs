﻿using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.System;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System;

namespace LanguageCenterPLC.Application.Implementation
{
    public class AnnouncementService : IAnnouncementService
    {
        private IRepository<Announcement, string> _announcementRepository;
        private IRepository<AnnouncementUser, int> _announcementUserRepository;

        private IUnitOfWork _unitOfWork;

        public AnnouncementService(IRepository<Announcement, string> announcementRepository,
            IRepository<AnnouncementUser, int> announcementUserRepository,
            IUnitOfWork unitOfWork)
        {
            _announcementUserRepository = announcementUserRepository;
            this._announcementRepository = announcementRepository;
            this._unitOfWork = unitOfWork;
        }

        //public PagedResult<AnnouncementViewModel> GetAllUnReadPaging(Guid userId, int pageIndex, int pageSize)
        //{
        //    var query = from x in _announcementRepository.FindAll()
        //                join y in _announcementUserRepository.FindAll() on x.Id equals y.AnnouncementId
        //                into xy
        //                from annonUser in xy.DefaultIfEmpty()
        //                where annonUser.HasRead == false && (annonUser.UserId == null || annonUser.UserId == userId)
        //                select x;
        //    int totalRow = query.Count();

        //    var model = query.OrderByDescending(x => x.DateCreated)
        //        .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ProjectTo<AnnouncementViewModel>().ToList();

        //    var paginationSet = new PagedResult<AnnouncementViewModel>
        //    {
        //        Results = model,
        //        CurrentPage = pageIndex,
        //        RowCount = totalRow,
        //        PageSize = pageSize
        //    };

        //    return paginationSet;
        //}

        public bool MarkAsRead(Guid userId, string id)
        {
            bool result = false;
            var announ = _announcementUserRepository.FindSingle(x => x.AnnouncementId == id
                                                                               && x.AppUserId == userId);
            if (announ == null)
            {
                _announcementUserRepository.Add(new AnnouncementUser
                {
                    AnnouncementId = id,
                    AppUserId = userId,
                    HasRead = true
                });
                result = true;
            }
            else
            {
                if (announ.HasRead == false)
                {
                    announ.HasRead = true;
                    result = true;
                }

            }
            return result;
        }
    }
}
