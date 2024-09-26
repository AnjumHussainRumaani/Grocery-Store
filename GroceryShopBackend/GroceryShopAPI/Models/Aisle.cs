using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GroceryShopAPI.Models
{
    public class Aisle
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
