﻿using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCenterPLC.Data.Entities
{
    /// <summary>
    /// Khóa học
    /// </summary>
    [Table("Courses")]
    public class Course : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int TraingTime { get; set; }

        [Required]
        public int NumberOfSession { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        public Status Status { get; set; }

        public string Note { get; set; }

        /* Foreign Key */
        /*Reference Table*/
        /*List of References */
        public ICollection<LanguageClass> LanguageClasses { set; get; }
    }
}
