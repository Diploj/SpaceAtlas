using Microsoft.AspNetCore.Identity;
using SpaceAtlas.IoC;
using SpaceAtlas.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var settings = new DbSettings(configuration,"SpaceAtlasDbContext");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



DbContextConfigurator.ConfigureService(builder.Services, settings);
MapperConfigurator.ConfigureServices(builder.Services);
SwaggerConfigurator.ConfigureServices(builder.Services);
SerilogConfigurator.ConfigureService(builder);
RegisterServices.Register(builder.Services);
IdentityConfigurator.Configure(builder.Services);
var app = builder.Build();
SwaggerConfigurator.ConfigureApplication(app);
DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }
    }
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();