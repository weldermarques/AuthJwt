﻿using AuthJwt.Core;
using AuthJwt.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthJwt.Api.Extensions;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionString = 
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        
        Configuration.Secrets.ApiKey = 
            builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? string.Empty;
        Configuration.Secrets.JwtPrivateKey = 
            builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;
        Configuration.Secrets.PasswordSaltKey = 
            builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey") ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(s 
            => s.UseNpgsql(
                Configuration.Database.ConnectionString, 
                opts => opts.MigrationsAssembly("AuthJwt.Api")));
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(s =>
        {
            s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(s =>
        {
            s.RequireHttpsMetadata = false;
            s.SaveToken = true;
            s.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
    }
}
