using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SchoolDiary.Objects
{
    public class Content
    {
        [JsonPropertyName("lesson")]
        public Lesson Lesson { get; set; }

        [JsonPropertyName("homework")]
        public Homework Homework { get; set; }

        [JsonPropertyName("marks")]
        public List<Mark> Marks { get; set; }
    }
}
