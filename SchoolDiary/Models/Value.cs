using System.Text.Json.Serialization;

namespace SchoolDiary.Models
{
    public class Value
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
