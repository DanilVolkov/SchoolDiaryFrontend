using System.Text.Json.Serialization;

namespace SchoolDiary.Objects
{
    public class Group
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
