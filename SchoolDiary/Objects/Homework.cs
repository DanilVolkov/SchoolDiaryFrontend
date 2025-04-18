using System.Text.Json.Serialization;

namespace SchoolDiary.Objects
{
    public class Homework
    {
        [JsonPropertyName("lesson_id")]
        public int LessonId { get; set; }

        [JsonPropertyName("teacher_id")]
        public int TeacherId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }
    }
}
