using LanguageCenterPLC.Infrastructure.Enums;

namespace LanguageCenterPLC.Application.ViewModels.Studies
{
    public class ContactViewModel
    {
        public string Id { get; set; }
        public string Name { set; get; }

        public string Phone { set; get; }

        public string Email { set; get; }

        public string Website { set; get; }

        public string Address { set; get; }

        public string Other { set; get; }

        public double? Lat { set; get; }

        public double? Lng { set; get; }

        public Status Status { set; get; }
    }
}
