using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace LanguageCenterPLC.Application.AutoMapper
{
    class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}
