using Api.Filters;
using Application.Behaviours;
using Application.Extensions;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });
    }

    public static void ConfigureMvc(this IServiceCollection serviceCollection)
    {
        serviceCollection
           .AddControllers(options =>
           {
               options.OutputFormatters.RemoveType<StringOutputFormatter>();
               options.Filters.Add<ValidationFilter>();
               options.ModelValidatorProviders.Clear();
           })
           .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
           .AddJsonOptions(options =>
           {
               // Serialize enums as strings in api responses
               options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
               options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
               options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
           });
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new()
                {
                    Title = "Requisition Service",
                    Version = "v1",
                    Contact = new()
                    {
                        Name = "E-mail",
                        Email = "info@sysbeams.com"
                    }
                });

            c.AddSecurityRequirement(new()
           {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                    },
                    new List<string>()
                }
           });

            // Configure Swagger to use JWT Bearer token authentication
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "Input your Bearer token to access this API",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            });
        });
        services.AddFluentValidationRulesToSwagger();
    }

    public static void AddMockAuth(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://your-keycloak-domain/auth/realms/your-realm",
                ValidAudience = "your-client-id",
                RoleClaimType = ClaimTypes.Role,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes("S0M3RAN0MS3CR3T!1!MAG1C!1!343456y674688847"))
            };
        });
    }

    public static void AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://your-keycloak-domain/auth/realms/your-realm";
                options.Audience = "your-client-id";
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://your-keycloak-domain/auth/realms/your-realm",
                    ValidateAudience = true,
                    ValidAudience = "your-client-id",
                    ValidateLifetime = true,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }

    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Default.EnumMappingStrategy(EnumMappingStrategy.ByName);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
    public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        // Set FluentValidator Global Options
        ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member.Name.ToCamelCase();
        ValidatorOptions.Global.PropertyNameResolver = (_, member, _) => member.Name.ToCamelCase();

        return serviceCollection
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}