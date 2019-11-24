using AutoMapper;
using LanguageCenterPLC.Application.ViewModels;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Finances;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Application.ViewModels.System;
using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Data.Entities;

namespace LanguageCenterPLC.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {

            CreateMap<AppRoleViewModel, AppRole>()
                .ConstructUsing(c => new AppRole(c.Name, c.Description));
            CreateMap<AppUserViewModel, AppUser>()
                .ConstructUsing(c => new AppUser());
            CreateMap<AttendanceSheetViewModel, AttendanceSheet>();
            CreateMap<AttendanceSheetDetailViewModel, AttendanceSheetDetail>();
            CreateMap<ClassroomViewModel, Classroom>();
            CreateMap<ContactViewModel, Contact>()
                .ConstructUsing(c => new Contact(c.Id, c.Name, c.Phone,
            c.Email, c.Website, c.Address, c.Other, c.Lng, c.Lat, c.Status));
            CreateMap<CourseViewModel, Course>();
            CreateMap<EndingCoursePointViewModel, EndingCoursePoint>();
            CreateMap<EndingCoursePointDetailViewModel, EndingCoursePointDetail>();
            CreateMap<FeedbackViewModel, Feedback>()
                .ConstructUsing(c => new Feedback(c.Id, c.Name, c.Email,
            c.Message, c.Status));
            CreateMap<FooterViewModel, Footer>();
            CreateMap<FunctionViewModel, Function>()
                .ConstructUsing(c => new Function(c.Name, c.URL,
            c.ParentId, c.IconCss, c.SortOrder));
            CreateMap<GuestTypeViewModel, GuestType>();
            CreateMap<LanguageClassViewModel, LanguageClass>();
            CreateMap<LearnerViewModel, Learner>();
            CreateMap<LecturerViewModel, Lecturer>();
            CreateMap<PaySlipViewModel, PaySlip>();
            CreateMap<PaySlipTypeViewModel, PaySlipType>();
            CreateMap<PeriodicPointViewModel, PeriodicPoint>();
            CreateMap<PeriodicPointDetailViewModel, PeriodicPointDetail>();
            CreateMap<PermissionViewModel, Permission>()
                .ConstructUsing(c => new Permission(c.AppUserId, c.FunctionId,
            c.CanCreate, c.CanRead, c.CanUpdate, c.CanDelete));
            CreateMap<PersonnelViewModel, Personnel>();
            CreateMap<ReceiptViewModel, Receipt>();
            CreateMap<ReceiptDetailViewModel, ReceiptDetail>();
            CreateMap<ReceiptTypeViewModel, ReceiptType>();
            CreateMap<StudyProcessViewModel, StudyProcess>();
            CreateMap<ClassSessionViewModel, ClassSession>();
            CreateMap<SystemConfigViewModel, SystemConfig>();
            CreateMap<TeachingScheduleViewModel, TeachingSchedule>();
            CreateMap<TimesheetViewModel, Timesheet>();
            CreateMap<AnnouncementViewModel, Announcement>()
                .ConstructUsing(c => new Announcement(c.Title, c.Content, c.UserId, c.Status));

            CreateMap<AnnouncementUserViewModel, AnnouncementUser>()
                .ConstructUsing(c => new AnnouncementUser(c.AnnouncementId, c.UserId, c.HasRead));

        }
    }
}
