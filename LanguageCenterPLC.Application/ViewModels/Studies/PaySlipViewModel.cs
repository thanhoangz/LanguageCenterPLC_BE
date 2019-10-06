using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class PaySlipViewModel
    {
        public string Id { get; set; }
        /// <summary>
        /// Nội dung chi
        /// </summary>
        public string Content { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }


        public string Receiver { get; set; }


        public Status Status { get; set; }

        
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }


        /* Foreign Key */

        public int PaySlipTypeId { get; set; }

        /// <summary>
        /// Nhân viên tạo phiếu, chi trả
        /// </summary>
       
        public string PersonnelId { get; set; }

        /// <summary>
        /// Nhân viên nhận chi trả (nếu có)
        /// </summary>
        /// 
        
        public string SendPersonnelId { get; set; }

        public Guid AppUserId { get; set; }

        /*Reference Table*/


        public  PersonnelViewModel Personnel { get; set; }


        public  PersonnelViewModel SendPersonnel { get; set; }


        public PaySlipTypeViewModel PaySlipType { get; set; }

        public AppUserViewModel AppUser { get; set; }

        /*List of References */

    }
}
