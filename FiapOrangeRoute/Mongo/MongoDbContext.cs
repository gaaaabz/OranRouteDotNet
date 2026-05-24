using System;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

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

// Classes mínimas para resolver CS0246.
// Ajuste propriedades e tipos conforme o modelo real da aplicação.
public class UsuarioLog
{
    public string Id { get; set; }
    public string Usuario { get; set; }
    public DateTime Timestamp { get; set; }
    public string Acao { get; set; }
}

public class ErrorLog
{
    public string Id { get; set; }
    public string Mensagem { get; set; }
    public string StackTrace { get; set; }
    public DateTime OccurredAt { get; set; }
}

public class LoginHistory
{
    public string Id { get; set; }
    public string Usuario { get; set; }
    public DateTime LoginAt { get; set; }
    public string Ip { get; set; }
}