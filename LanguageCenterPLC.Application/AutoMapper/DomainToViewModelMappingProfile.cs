using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Application.ViewModels.Timekeepings;
using LanguageCenterPLC.Application.ViewModels.Finances;
using LanguageCenterPLC.Application.ViewModels.Studies;


using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Application.ViewModels;

namespace LanguageCenterPLC.Application.AutoMapper
{
    class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AttendanceSheet, AttendanceSheetViewModel>();
            CreateMap<AttendanceSheetDetail, AttendanceSheetDetailViewModel>();
            CreateMap<ClassRoom, ClassRoomViewModel>();
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
            CreateMap<LogWork, LogWorkViewModel>();
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


        }
    }
}
