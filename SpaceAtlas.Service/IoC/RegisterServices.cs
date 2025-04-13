using SpaceAtlas.Algoritms;
using SpaceAtlas.BL.Planet;
using SpaceAtlas.BL.Star;
using SpaceAtlas.BL.User;
using SpaceAtlas.DataAccess.Entities;
using SpaceAtlas.DataAccess.Repository;

namespace SpaceAtlas.IoC;

public class RegisterServices
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IRepository<PlanetEntity>, Repository<PlanetEntity>>();
        services.AddScoped<IRepository<StarEntity>, Repository<StarEntity>>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStarService, StarService>();
        services.AddScoped<IPlanetService, PlanetService>();
        services.AddScoped<TokenGenerator>();
    }
}