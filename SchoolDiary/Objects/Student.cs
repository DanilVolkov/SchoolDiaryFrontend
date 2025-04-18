﻿using System.Text.Json.Serialization;

namespace SchoolDiary.Objects
{
    public class Student
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("middle_name")]
        public string MiddleName { get; set; }

        [JsonPropertyName("date_of_birth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("role_id")]
        public int? RoleId { get; set; }

        [JsonPropertyName("group_id")]
        public int? GroupId { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }
    }
}
