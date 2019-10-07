﻿using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using System;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class TeachingScheduleViewModel
    {
      public int Id { get; set; }
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string TimeShift { get; set; }

        public string DaysOfWeek { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        /* Foreign Key */
        public int LecturerId { get; set; }

        public int ClassRoomId { get; set; }

        public string LanguageClassId { get; set; }

        /*Reference Table*/

        public  LecturerViewModel Lecturer { get; set; }

        public  ClassRoomViewModel ClassRoom { get; set; }

        public  LanguageClassViewModel LanguageClass { get; set; }
    }
}
