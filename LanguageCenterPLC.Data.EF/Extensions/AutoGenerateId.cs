using System;

namespace LanguageCenterPLC.Data.EF.Extensions
{
    public static class AutoGenerateId
    {
        public static Guid CreateId()
        {
            return Guid.NewGuid();
        }
    }
}
