using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FiapOrangeRoute.Mongo.Collections;

public class UsuarioLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;
}