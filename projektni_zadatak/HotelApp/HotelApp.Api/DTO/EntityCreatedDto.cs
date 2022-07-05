using System.Text.Json.Serialization;

namespace HotelApp.Api.DTO
{
    public class EntityCreatedDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }
        public string? Message { get; set; }
    }
}
