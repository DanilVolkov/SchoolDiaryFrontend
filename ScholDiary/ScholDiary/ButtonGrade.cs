using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolDiary
{
    public class ButtonGrade : ButtonRounded
    {
        private Form1 parentForm;

        // Конструктор
        public ButtonGrade()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_7DCCDE;
            Size = new Size(100, 30); // Размер кнопки по умолчанию

            // Привязываем обработчик события Click
            this.Click += ButtonGrade_Click;
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
        private void ButtonGrade_Click(object sender, EventArgs e)
        {
            if (parentForm != null)
            {
                // Показываем окно расписания
                //parentForm.ShowGrade();
            }
        }
    }
}
