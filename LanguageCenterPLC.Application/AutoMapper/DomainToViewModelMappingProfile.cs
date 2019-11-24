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
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AttendanceSheet, AttendanceSheetViewModel>();
            CreateMap<AttendanceSheetDetail, AttendanceSheetDetailViewModel>();
            CreateMap<Classroom, ClassroomViewModel>();
            CreateMap<Contact, ContactViewModel>();
            CreateMap<Course, CourseViewModel>();
            CreateMap<EndingCoursePoint, EndingCoursePointViewModel>();
            CreateMap<EndingCoursePointDetail, EndingCoursePointDetailViewModel>();
            CreateMap<Feedback, FeedbackViewModel>();
            CreateMap<Footer, FooterViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<GuestType, GuestTypeViewModel>();
            CreateMap<LanguageClass, LanguageClassViewModel>();
            CreateMap<Learner, LearnerViewModel>();
            CreateMap<Lecturer, LecturerViewModel>();
          
            CreateMap<PaySlip, PaySlipViewModel>();
            CreateMap<PaySlipType, PaySlipTypeViewModel>();
            CreateMap<PeriodicPoint, PeriodicPointViewModel>();
            CreateMap<PeriodicPointDetail, PeriodicPointDetailViewModel>();
            CreateMap<Permission, PermissionViewModel>();
            CreateMap<Personnel, PersonnelViewModel>();
            CreateMap<Receipt, ReceiptViewModel>();
            CreateMap<ReceiptDetail, ReceiptDetailViewModel>();
            CreateMap<ReceiptType, ReceiptTypeViewModel>();
            CreateMap<StudyProcess, StudyProcessViewModel>();
            CreateMap<SystemConfig, SystemConfigViewModel>();
            CreateMap<TeachingSchedule, TeachingScheduleViewModel>();
            CreateMap<Timesheet, TimesheetViewModel>();
            CreateMap<Announcement, AnnouncementViewModel>().MaxDepth(2);
            CreateMap<ClassSession, ClassSessionViewModel>();
        }
    }
}
