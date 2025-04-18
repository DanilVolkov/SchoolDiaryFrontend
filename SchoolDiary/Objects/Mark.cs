using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolDiary.Objects
{
    public class Mark
    {
        [JsonPropertyName("lesson_id")]
        public int? LessonId { get; set; }

        [JsonPropertyName("student_id")]
        public int? StudentId { get; set; }

        [JsonPropertyName("value_id")]
        public int? ValueId { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("value")]
        public Value Value { get; set; }
    }
}
