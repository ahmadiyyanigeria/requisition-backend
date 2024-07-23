using Api.Extensions;
using Application.Commands;
using Application.Configurations;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//Log.Logger = LogConfiguration.CreateLogger(configuration, isDevelopment);

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
//builder.WebHost.UseSentry();

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddDatabase(configuration);

builder.Services.Configure<ApprovalFlowConfiguration>(builder.Configuration.GetSection("ApprovalFlows"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwagger();
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureMvc();
builder.Services.AddHealthChecks();
builder.Services.AddMapster();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateRequisition).Assembly));
builder.Services.AddValidators();

var app = builder.Build();
app.ConfigureExceptionHandler();
//app.UseSentryTracing();
app.ConfigureCors();
app.ConfigureSwagger(configuration);


app.UseHttpsRedirection();
app.UseRouting();
//app.MapMetrics();
app.UseHttpMetrics();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/healthz");

MigrationExtensions.ApplyMigration(app.Services, !builder.Environment.IsProduction());

app.Run();


// Make the Program class public for testing using a partial class declaration
#pragma warning disable CA1050

public partial class Program { }
#pragma warning restore CA1050
