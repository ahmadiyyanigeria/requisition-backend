using Api.Filters;
using Application.Behaviours;
using Application.Extensions;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                            Id = "OAuth2",
                            Type = ReferenceType.SecurityScheme
                        },
                    },
                    new List<string>()
                }
           });
        });
        services.AddFluentValidationRulesToSwagger();
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