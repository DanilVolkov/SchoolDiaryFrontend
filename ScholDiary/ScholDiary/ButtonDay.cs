using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary
{
    class ButtonDay : ButtonRounded
    {
        private Form1 parentForm;

        // Конструктор
        public ButtonDay()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_D4AA95;
            Size = new Size(100, 30); // Размер кнопки по умолчанию

            // Привязываем обработчик события Click
            this.Click += OpenDay_Click;
        }

        public Form1 ParentForm
        {
            get => parentForm;
            set
            {
                parentForm = value;
            }
        }

        // Обработчик события Click
        private void OpenDay_Click(object sender, EventArgs e)
        {
            if (parentForm != null)
            {
                // Показываем окно расписания
                //parentForm.ShowDaySchedule();
            }
        }
    }
}
