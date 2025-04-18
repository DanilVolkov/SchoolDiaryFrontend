using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SchoolDiary.Objects
{
    class DaySchedule
    {
        [JsonPropertyName("day_of_week")]
        public int? DayOfWeek { get; set; }

        [JsonPropertyName("day_of_week_str")]
        public string DayOfWeekStr { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("content")]
        public List<Content> Content { get; set; }
    }
}
