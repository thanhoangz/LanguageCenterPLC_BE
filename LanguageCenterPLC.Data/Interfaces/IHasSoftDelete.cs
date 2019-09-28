namespace LanguageCenterPLC.Infrastructure.Interfaces
{
    public interface IHasSoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
