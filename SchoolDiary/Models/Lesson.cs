using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace SchoolDiary.Models
{

    public class CustomDateConverter : JsonConverter<DateTime>
    {
        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
        {
            writer.WriteStringValue(date.ToString("yyyy-MM-dd"));
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), "yyyy-MM-dd", null);
        }
    }

    public class CustomTimeConverter : JsonConverter<DateTime>
    {
        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
        {
            writer.WriteStringValue(date.ToString("HH:mm:ss"));
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), "HH:mm:ss", null);
        }
    }

    public class Lesson
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("schedule_id")]
        public int? ScheduleId { get; set; }

        [JsonPropertyName("group_id")]
        public int? GroupId { get; set; }

        [JsonPropertyName("subject_id")]
        public int? SubjectId { get; set; }

        [JsonPropertyName("teacher_id")]
        public int? TeacherId { get; set; }

        [JsonPropertyName("classroom_id")]
        public int? ClassroomId { get; set; }

        [JsonPropertyName("date")]
        [JsonConverter(typeof(CustomDateConverter))]
        public DateTime Date { get; set; }

        [JsonPropertyName("time_start")]
        [JsonConverter(typeof(CustomTimeConverter))]
        public DateTime TimeStart { get; set; }

        [JsonPropertyName("time_end")]
        [JsonConverter(typeof(CustomTimeConverter))]
        public DateTime TimeEnd { get; set; }

        [JsonPropertyName("group")]
        public Group Group { get; set; }

        [JsonPropertyName("subject")]
        public Subject Subject { get; set; }

        [JsonPropertyName("teacher")]
        public Teacher Teacher { get; set; }

        [JsonPropertyName("classroom")]
        public Classroom Classroom { get; set; }

        public int RowIndex { get; set; } 
        public int ColumnIndex { get; set; }
    }
}
