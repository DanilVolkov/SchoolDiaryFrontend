using SchoolDiary.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolDiary.APIConnect
{
    //Сделать Одиночкой
    class APIConnector
    {
        private HttpClient client;
        private int student_id;

        public APIConnector()
        {
            client = new HttpClient();
            student_id = 24; //Рандомный студент для демонстарции работы программы
        }

        public async Task<Student> GetStudent()
        {
            Student student = null;
            HttpResponseMessage response = await client.GetAsync(string.Format("https://api.nkkz.dev/students/{0}",student_id));
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

        public async Task<List<Objects.DaySchedule>> GetWeekSchedule(DateTime from, DateTime to)
        {
            List<Objects.DaySchedule> schedule = null;
            string date_from = from.ToString("yyyy-MM-dd");
            string date_to = to.ToString("yyyy-MM-dd");
            string path = string.Format("https://api.nkkz.dev/students/{0}/schedule?from={1}&to={2}", student_id, date_from, date_to);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                schedule = JsonSerializer.Deserialize<List<Objects.DaySchedule>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception();
            }
            return schedule;
        }
    }
}
