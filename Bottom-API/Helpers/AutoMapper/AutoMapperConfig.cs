using Bottom_API.Helpers.AutoMapper;
using AutoMapper;

namespace Bottom_API.Helpers.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EfToDtoMappingProfile());
                cfg.AddProfile(new DtoToEfMappingProfile());
            });
        }
    }
}