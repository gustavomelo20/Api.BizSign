using System.Text;
using Api.BizSign.Core.Services;
using Api.BizSign.Core.Services.Contracts;
using Api.BizSign.Infrastructure.Data;
using Api.BizSign.Infrastructure.Repositories;
using Api.BizSign.Infrastructure.Repositories.Contract;
using Api.BizSign.WebApi.Request;
using Api.BizSign.WebApi.Request.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.BizSign.Infrastructure.Configuration;

public class ServiceConfiguration
{
    public static WebApplicationBuilder Build(WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseMongoDB(
                builder.Configuration.GetValue<string>("MONGODB_CONNECTION_STRING") ?? "",
                builder.Configuration.GetValue<string>("MONGODB_DATABASE") ?? ""
            )
        );

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
        builder.Services.AddScoped<ISignatoryRepository, SignatoryRepository>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IJwtService, JwtService>();

        var key = builder.Configuration["Jwt:Key"]
                  ?? throw new InvalidOperationException("JWT Key not configured");

        var keyBytes = Encoding.UTF8.GetBytes(key);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });


        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IValidator<AuthRequest>, AuthValidator>();
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .Select(e => new
                    {
                        field = e.Key,
                        error = e.Value!.Errors.First().ErrorMessage
                    });

                return new BadRequestObjectResult(new
                {
                    message = "Erro de validação nos campos enviados",
                    errors
                });
            };
        });

        return builder;
    }
}