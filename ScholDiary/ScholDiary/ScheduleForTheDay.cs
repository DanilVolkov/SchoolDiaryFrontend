using SchoolDiary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScholDiary
{
    public partial class ScheduleForTheDay : UserControl
    {
        public ScheduleForTheDay()
        {
            InitializeComponent();
            customGroupBox1.BackColor = Colors.C_4EB4D0;
            customGroupBox2.BackColor = Colors.C_7DCCDE;
            customGroupBox3.BackColor = Colors.C_BAEAFD;
            //ImageInButtonRoundeds.GroupBackNext(buttonBack1, buttonNext1);
            ImageInButtonRoundeds.GroupImageMenuProfileLogo(buttonMenu1, buttonProfile1, buttonRounded1);
            ImageInButtonRoundeds.GroupSheduleGrade(buttonShedule3, buttonGrade1);
            ImageInButtonRoundeds.GroupDarkLightNotifications(buttonDarkLightMode2, buttonNotifications2);
            SubjectInfoPanel subjectPanel = new SubjectInfoPanel(
                "Математика",
                "Иван Иванов", "101",
                startTime: new TimeSpan(8, 0, 0) // Начало урока в 9:00

            );
            HomeworkAndGradesPanel homeworkPanel = new HomeworkAndGradesPanel(
           homework: "Решить задачи на страницах 23-24",
           grade1: "4",
           grade2: "5",
           grade3: "3"
       );
            subjectPanel.Location = new Point(164, 133);
            homeworkPanel.Location = new Point(subjectPanel.Right + 10, subjectPanel.Top);
            this.Controls.Add(subjectPanel);
            this.Controls.Add(homeworkPanel);
        }
    }
}
