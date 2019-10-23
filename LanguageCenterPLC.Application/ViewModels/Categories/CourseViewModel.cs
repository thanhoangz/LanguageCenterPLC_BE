using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace LanguageCenterPLC.Application.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Content { get; set; }

        public int TraingTime { get; set; }

        public int NumberOfSession { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public Status Status { get; set; }

        public string Note { get; set; }

        public List<LanguageClassViewModel> LanguageClasses { set; get; }
    }
}
