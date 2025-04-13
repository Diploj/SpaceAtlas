using Microsoft.AspNetCore.Identity;
using SpaceAtlas.IoC;
using SpaceAtlas.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbContext"));
builder.Services.AddControllers();
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", builder => 
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
DbContextConfigurator.ConfigureService(builder.Services,builder.Configuration);
MapperConfigurator.ConfigureServices(builder.Services);
SwaggerConfigurator.ConfigureServices(builder.Services);
SerilogConfigurator.ConfigureService(builder);
RegisterServices.Register(builder.Services);
IdentityConfigurator.Configure(builder.Services, builder.Configuration);

var app = builder.Build();


app.UseCors("AllowAll");
SwaggerConfigurator.ConfigureApplication(app);
DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();