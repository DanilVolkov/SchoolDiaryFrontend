using System.Text.Json.Serialization;

namespace SchoolDiary.Models
{
    class Token
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("toekn_type")]
        public string ToeknType { get; set; }
    }
}
