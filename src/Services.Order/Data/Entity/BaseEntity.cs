using System;
using System.Text.Json.Serialization;

namespace Services.Order.Data
{
    public abstract class BaseEntity
    {
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
    }
}