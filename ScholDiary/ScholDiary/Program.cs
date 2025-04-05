using ScholDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolDiary
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form hostForm = new Form();
            hostForm.Text = "My App";
            //hostForm.Size = new System.Drawing.Size(1920, 1080);
            Application.EnableVisualStyles();
            Grade grade = new Grade();
            //ScheduleForTheDay scheduleForTheDay = new ScheduleForTheDay();
            hostForm.Controls.Add(grade);
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(hostForm);
        }
    }
}
