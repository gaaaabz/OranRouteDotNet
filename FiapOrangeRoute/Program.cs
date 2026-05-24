using FiapOrangeRoute.Data;
using FiapOrangeRoute.HealthChecks;

using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Repositories.Implementations;

using FiapOrangeRoute.Services.Interfaces;
using FiapOrangeRoute.Services.Implementations;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Serilog;

using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

#region SERILOG

Log.Logger = new LoggerConfiguration()

    .MinimumLevel.Information()

    .MinimumLevel.Override(
        "Microsoft.AspNetCore",
        Serilog.Events.LogEventLevel.Warning)

    .MinimumLevel.Override(
        "Microsoft.EntityFrameworkCore",
        Serilog.Events.LogEventLevel.Warning)

    .WriteTo.Console()

    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate:
            "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] " +
            "{Message:lj}{NewLine}{Exception}")

    .Enrich.FromLogContext()

    .CreateLogger();

builder.Host.UseSerilog();

#endregion

#region CONTROLLERS + JSON

builder.Services.AddControllers()

    .AddJsonOptions(opt =>

        opt.JsonSerializerOptions.ReferenceHandler =

            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();

#endregion

#region SWAGGER

builder.Services.AddSwaggerGen();

#endregion

#region ENTITY FRAMEWORK + ORACLE

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<AppDbContext>(options =>

        options.UseInMemoryDatabase("TestDb"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>

        options.UseOracle(
            builder.Configuration
            .GetConnectionString("OracleConnection")));
}

#endregion

#region DEPENDENCY INJECTION - REPOSITORIES

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();

builder.Services.AddScoped<ITrilhaCarreiraRepository, TrilhaCarreiraRepository>();

builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();

builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();

builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddScoped<ILinkRepository, LinkRepository>();

builder.Services.AddScoped<ITagCarreiraRepository, TagCarreiraRepository>();

#endregion

#region DEPENDENCY INJECTION - SERVICES

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<ITipoUsuarioService, TipoUsuarioService>();

builder.Services.AddScoped<ITrilhaCarreiraService, TrilhaCarreiraService>();

builder.Services.AddScoped<IComentarioService, ComentarioService>();

builder.Services.AddScoped<IFavoritoService, FavoritoService>();

builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddScoped<ILinkService, LinkService>();

builder.Services.AddScoped<ITagCarreiraService, TagCarreiraService>();

#endregion

#region HEALTH CHECKS

var healthBuilder = builder.Services.AddHealthChecks();

if (!builder.Environment.IsEnvironment("Testing"))
{
    healthBuilder.AddDbContextCheck<AppDbContext>(
        "oracle",
        tags: new[] { "db", "oracle" });
}

healthBuilder.AddCheck<ApiHealthCheck>(
    "api",
    tags: new[] { "api" });

#endregion

#region OPENTELEMETRY

builder.Services.AddOpenTelemetry()

    .WithTracing(tracing =>
    {
        tracing

            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                .AddService("FiapOrangeRoute"))

            .AddAspNetCoreInstrumentation()

            .AddSource("Microsoft.EntityFrameworkCore")

            .AddConsoleExporter();
    })

    .WithMetrics(metrics =>
    {
        metrics

            .AddAspNetCoreInstrumentation()

            .AddConsoleExporter();
    });

#endregion

var app = builder.Build();

#region SWAGGER APP

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

#endregion

#region MIDDLEWARES

app.UseHttpsRedirection();

app.UseAuthorization();

#endregion

#region CONTROLLERS

app.MapControllers();

#endregion

#region HEALTH ENDPOINT

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),

            entries = report.Entries.Select(e => new
            {
                name = e.Key,

                status = e.Value.Status.ToString(),

                description = e.Value.Description
            })
        });

        await context.Response.WriteAsync(result);
    }
});

#endregion

app.Run();

public partial class Program { }