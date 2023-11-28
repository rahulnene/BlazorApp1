using System.Text.Json.Serialization;

namespace BlazorApp1.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RPGClass
    {
        Knight = 1,
        Mage = 2,
        Cleric = 3,
        Rogue = 4,
    }
}