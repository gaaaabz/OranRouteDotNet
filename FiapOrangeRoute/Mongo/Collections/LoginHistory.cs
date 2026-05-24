using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FiapOrangeRoute.Mongo.Collections;

public class LoginHistory
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Email { get; set; }

    public DateTime LoginDate { get; set; }
        = DateTime.UtcNow;
}