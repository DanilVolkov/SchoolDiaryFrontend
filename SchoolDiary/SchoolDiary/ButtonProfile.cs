using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolDiary
{
    public class ButtonProfile : ButtonRounded
    {
        private Form1 parentForm;

        // Конструктор
        public ButtonProfile()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_4EB4D0;
            Size = new Size(100, 30); // Размер кнопки по умолчанию

            // Привязываем обработчик события Click
            this.Click += ButtonProfile_Click;
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
        private void ButtonProfile_Click(object sender, EventArgs e)
        {
            if (parentForm != null)
            {
                // Показываем окно расписания
                parentForm.ShowProfile();
            }
        }
    }
}
