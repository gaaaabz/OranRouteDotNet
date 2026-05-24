using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FiapOrangeRoute.HealthChecks;

public class ApiHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            HealthCheckResult.Healthy(
                "API funcionando corretamente."));
    }
}