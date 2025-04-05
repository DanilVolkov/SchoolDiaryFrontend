using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolDiary
{
    public class ButtonShedule : ButtonRounded
    {
        private Form1 parentForm;

        // Конструктор без параметров для дизайнера
        public ButtonShedule()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_7DCCDE;
            Size = new Size(100, 30);

            this.Click += ButtonShedule_Click;
        }

        // Свойство для установки ссылки на форму
        public Form1 ParentForm
        {
            get => parentForm;
            set
            {
                parentForm = value;
            }
        }

        // Обработчик события Click
        private void ButtonShedule_Click(object sender, EventArgs e)
        {
            if (parentForm != null)
            {
                // Показываем окно расписания
                //parentForm.ShowWeeklySchedule();
            }
        }
    }
}
