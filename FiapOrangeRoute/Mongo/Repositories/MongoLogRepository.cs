using FiapOrangeRoute.Mongo.Collections;

namespace FiapOrangeRoute.Mongo.Repositories;

public class MongoLogRepository
{
    private readonly MongoDbContext _context;

    public MongoLogRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task CreateUsuarioLogAsync(
        UsuarioLog log)
    {
        await _context.UsuarioLogs
            .InsertOneAsync(log);
    }

    public async Task CreateErrorLogAsync(
        ErrorLog log)
    {
        await _context.ErrorLogs
            .InsertOneAsync(log);
    }

    public async Task CreateLoginHistoryAsync(
        LoginHistory log)
    {
        await _context.LoginHistories
            .InsertOneAsync(log);
    }
}