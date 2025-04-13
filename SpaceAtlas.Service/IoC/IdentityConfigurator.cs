using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SpaceAtlas.DataAccess;
using SpaceAtlas.DataAccess.Entities;
using SpaceAtlas.Settings;

namespace SpaceAtlas.IoC;

public class IdentityConfigurator
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<UserEntity, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<SpaceAtlasDbContext>()
            .AddDefaultTokenProviders();
        
        
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                };
            });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        });
    }
}