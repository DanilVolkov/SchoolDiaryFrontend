using SchoolDiary.Objects;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolDiary.APIConnect
{
    class APIConnector
    {
        private static HttpClient client;

        public APIConnector()
        {
            client = new HttpClient();
        }

        public async Task<Student> GetStudent(int id)
        {
            Student student = null;
            HttpResponseMessage response = await client.GetAsync("https://api.nkkz.dev/students/" + id);
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
    }
}
