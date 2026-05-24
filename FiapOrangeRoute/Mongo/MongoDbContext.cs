using MongoDB.Driver;

namespace FiapOrangeRoute.Mongo;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(
            configuration["MongoSettings:ConnectionString"]);

        _database = client.GetDatabase(
            configuration["MongoSettings:Database"]);
    }

    public IMongoCollection<UsuarioLog> UsuarioLogs =>
        _database.GetCollection<UsuarioLog>("UsuarioLogs");

    public IMongoCollection<ErrorLog> ErrorLogs =>
        _database.GetCollection<ErrorLog>("ErrorLogs");

    public IMongoCollection<LoginHistory> LoginHistories =>
        _database.GetCollection<LoginHistory>("LoginHistories");
}