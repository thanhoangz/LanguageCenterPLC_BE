using System;

namespace LanguageCenterPLC.Application.Interfaces
{
    public interface IAnnouncementService
    {
        //PagedResult<AnnouncementViewModel> GetAllUnReadPaging(Guid userId, int pageIndex, int pageSize);

        bool MarkAsRead(Guid userId, string id);
    }
}
