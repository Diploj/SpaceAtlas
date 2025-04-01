using SpaceAtlas.BL.Mapper;
using SpaceAtlas.Mapper;

namespace SpaceAtlas.IoC;

public class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserBLProfile));
        services.AddAutoMapper(typeof(StarBLProfile));
        services.AddAutoMapper(typeof(PlanetBLProfile));
        services.AddAutoMapper(typeof(UserProfile));
        services.AddAutoMapper(typeof(StarProfile));
        services.AddAutoMapper(typeof(PlanetProfile));
    }
}