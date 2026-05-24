using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace FiapOrangeRoute.HealthChecks;

public class MongoHealthCheck : IHealthCheck
{
    private readonly IMongoDatabase _database;

    public MongoHealthCheck(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _database.ListCollectionNamesAsync(
                cancellationToken: cancellationToken);

            return HealthCheckResult.Healthy(
                "MongoDB conectado.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                $"Erro MongoDB: {ex.Message}");
        }
    }
}