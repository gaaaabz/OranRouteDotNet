using FiapOrangeRoute.Data;

using FiapOrangeRoute.HealthChecks;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Serilog

Log.Logger = new LoggerConfiguration()

    .MinimumLevel.Information()

    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)

    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)

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

// Controllers + JSON

builder.Services.AddControllers()

    .AddJsonOptions(opt =>

        opt.JsonSerializerOptions.ReferenceHandler =

            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Entity Framework + Oracle

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("TestDb"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseOracle(builder.Configuration
            .GetConnectionString("OracleConnection")));
}

// Health Checks

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

// OpenTelemetry

builder.Services.AddOpenTelemetry()

    .WithTracing(tracing =>
        {
        tracing
            .SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService("FiapEscola"))

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

var app = builder.Build();

if (app.Environment.IsDevelopment())

{

    app.UseSwagger();

    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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

app.Run();

public partial class Program { }
