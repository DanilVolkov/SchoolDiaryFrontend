using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SchoolDiary.Objects
{
    public class Lesson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("schedule_id")]
        public int ScheduleId { get; set; }

        [JsonPropertyName("group_id")]
        public int GroupId { get; set; }

        [JsonPropertyName("subject_id")]
        public int SubjectId { get; set; }

        [JsonPropertyName("teacher_id")]
        public int TeacherId { get; set; }

        [JsonPropertyName("classroom_id")]
        public int ClassroomId { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("time_start")]
        public string TimeStart { get; set; }

        [JsonPropertyName("time_end")]
        public string TimeEnd { get; set; }

        [JsonPropertyName("group")]
        public Group Group { get; set; }

        [JsonPropertyName("subject")]
        public Subject Subject { get; set; }

        [JsonPropertyName("teacher")]
        public Teacher Teacher { get; set; }

        [JsonPropertyName("classroom")]
        public Classroom Classroom { get; set; }
    }
}
