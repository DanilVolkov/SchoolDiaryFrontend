using SchoolDiary.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SchoolDiary.Models
{
    class SubjectMarks
    {
        [JsonPropertyName("subject")]
        public Subject Subject { get; set; }

        [JsonPropertyName("marks")]
        public List<Mark> Marks { get; set; }

        [JsonPropertyName("average")]
        public double? Average { get; set; }
    }
}
