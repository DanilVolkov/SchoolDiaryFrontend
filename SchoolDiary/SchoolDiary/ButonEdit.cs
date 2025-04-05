using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary
{
    class ButonEdit : ButtonRounded
    {
        private Form1 parentForm;

        // Конструктор без параметров для дизайнера
        public ButonEdit()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_EFEFEF;
            Size = new Size(100, 30);
            this.Click += ButonEdit_Click;
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
        private void ButonEdit_Click(object sender, EventArgs e)
        {
            if (parentForm != null)
            {
                // Показываем окно расписания
                parentForm.ShowWeeklySchedule();
            }
        }
    }
}
