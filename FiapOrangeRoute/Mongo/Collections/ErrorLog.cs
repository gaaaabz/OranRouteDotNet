using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FiapOrangeRoute.Mongo.Collections;

public class ErrorLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Message { get; set; }

    public string StackTrace { get; set; }

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;
}