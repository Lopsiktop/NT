using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NT.Application.Common.Interfaces.Authentication;
using NT.Application.Common.Interfaces.Persistence;
using NT.Infrastructure.Authentication;
using NT.Infrastructure.Persistence;
using System.Text;

namespace NT.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var settings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, settings);
        services.AddSingleton(settings);

        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
                };
            });

        services.AddDbContext<NTDbContext>(x =>
        {
            x.UseNpgsql(configuration.GetConnectionString("PSQ"));
        });

        services.AddAuthorization(x =>
        {
            x.AddPolicy(IdentityPolicy.AdminPolicy,
                x => x.RequireClaim(IdentityPolicy.RoleClaim, "1"));
        });

        services.AddScoped<INTDbContext, NTDbContext>();

        return services;
    }
}
