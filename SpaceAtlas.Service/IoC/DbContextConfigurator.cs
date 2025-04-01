using Microsoft.EntityFrameworkCore;
using SpaceAtlas.DataAccess;
using SpaceAtlas.Settings;

namespace SpaceAtlas.IoC;

public class DbContextConfigurator
{
    public static void ConfigureService(IServiceCollection services,DbSettings settings )
    {
        services.AddDbContextFactory<SpaceAtlasDbContext>(
            options => { options.UseNpgsql(settings.ConnectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SpaceAtlasDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}