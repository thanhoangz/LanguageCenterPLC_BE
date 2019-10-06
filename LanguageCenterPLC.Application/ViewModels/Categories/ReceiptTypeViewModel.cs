using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageCenterPLC.Application.ViewModels.Categories
{
    public class ReceiptTypeViewModel
    {
       public int Id { get; set; }
        public string Name { get; set; }

       
        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Note { get; set; }

        /*List of References */

        public ICollection<ReceiptViewModel> Receipts { set; get; }
    }
}
