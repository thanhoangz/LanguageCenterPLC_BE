using LanguageCenterPLC.Infrastructure.Enums;

namespace LanguageCenterPLC.Infrastructure.Interfaces
{
    public interface ISwitchable
    {
        Status Status { get; set; }
    }
}
