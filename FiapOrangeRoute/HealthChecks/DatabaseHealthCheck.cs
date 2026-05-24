using FiapOrangeRoute.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FiapOrangeRoute.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly AppDbContext _context;

    public DatabaseHealthCheck(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect =
                await _context.Database
                    .CanConnectAsync(cancellationToken);

            if (canConnect)
            {
                return HealthCheckResult.Healthy(
                    "Banco Oracle conectado.");
            }

            return HealthCheckResult.Unhealthy(
                "Banco Oracle indisponível.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                $"Erro Oracle: {ex.Message}");
        }
    }
}