using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Globalization;

namespace GroceryShopAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        private DateTime _expiryDate;
        private DateTime _purchaseDate;

        [BsonIgnore] // Ignore this field in MongoDB storage
        public string ExpiryDate
        {
            get => _expiryDate.ToString("yyyy-MM-dd"); // Convert DateTime to string
            set
            {
                // Convert the string value to DateTime, defaulting to Now if invalid
                _expiryDate = DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate)
                    ? parsedDate
                    : DateTime.Now;
            }
        }

        [BsonIgnore] // Ignore this field in MongoDB storage
        public string PurchaseDate
        {
            get => _purchaseDate.ToString("yyyy-MM-dd"); // Convert DateTime to string
            set
            {
                // Convert the string value to DateTime, defaulting to Now if invalid
                _purchaseDate = DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate)
                    ? parsedDate
                    : DateTime.Now;
            }
        }

        // Use these properties to store actual DateTime values in MongoDB
        [BsonElement("ExpiryDate")]
        public DateTime ExpiryDateInternal
        {
            get => _expiryDate;
            set => _expiryDate = value;
        }

        [BsonElement("PurchaseDate")]
        public DateTime PurchaseDateInternal
        {
            get => _purchaseDate;
            set => _purchaseDate = value;
        }

        [BsonRepresentation(BsonType.String)]
        public Guid AisleId { get; set; }
    }
}
