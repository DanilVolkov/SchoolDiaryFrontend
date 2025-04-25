using SchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolDiary.APIConnect
{
    class APIConnector
    {
        private static HttpClient client;
        private static int student_id;
        private static APIConnector connector;

        private APIConnector()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.nkkz.dev/");
        }
        public static APIConnector GetInstance()
        {
            if (connector == null)
            {
                connector = new APIConnector();
            }
            return connector;
        }
        public async Task<Student> GetStudent()
        {
            Student student = null;
            HttpResponseMessage response = await client.GetAsync(string.Format("students/{0}",student_id));
            if (response.IsSuccessStatusCode)
            {
                student = JsonSerializer.Deserialize<Student>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception();
            }
            return student;
        }

        public async Task<List<Models.DaySchedule>> GetWeekSchedule(DateTime from, DateTime to)
        {
            List<Models.DaySchedule> schedule = null;
            string date_from = from.ToString("yyyy-MM-dd");
            string date_to = to.ToString("yyyy-MM-dd");
            string path = string.Format("students/{0}/schedule?from={1}&to={2}", student_id, date_from, date_to);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                schedule = JsonSerializer.Deserialize<List<Models.DaySchedule>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception();
            }
            return schedule;
        }

        public async Task<List<SubjectMarks>> GetMarks(DateTime from, DateTime to)
        {
            List<SubjectMarks> marks = null;
            string date_from = from.ToString("yyyy-MM-dd");
            string date_to = to.ToString("yyyy-MM-dd");
            string path = string.Format("students/{0}/marks?from={1}&to={2}", student_id, date_from, date_to);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                marks = JsonSerializer.Deserialize<List<SubjectMarks>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception();
            }
            return marks;
        }

        public async Task<bool> AuthUser(string user_name, string password)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", user_name),
                new KeyValuePair<string, string>("password", HashPassword.GetHash(password)),
            });

            HttpResponseMessage response = await client.PostAsync("login", formContent);
            if (response.IsSuccessStatusCode)
            {
                Token token = JsonSerializer.Deserialize<Token>(await response.Content.ReadAsStringAsync());
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                response = await client.GetAsync("students/me");
                Student s = JsonSerializer.Deserialize<Student>(await response.Content.ReadAsStringAsync());
                student_id = (int)s.Id;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
