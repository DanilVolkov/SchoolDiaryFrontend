using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Objects
{
    class Student
    {
        public int id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string date_of_birth { get; set; }
        public int role_id { get; set; }
        public int group_id { get; set; }
        public string role { get; set; }
        public string group { get; set; }
    }
}
