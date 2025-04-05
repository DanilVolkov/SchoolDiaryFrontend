using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolDiary
{
    public class ButtonBack : ButtonRounded
    {
        // Свойство для ссылки на форму профиля
        //private WeeklySchedule _profileForm;

        // Конструктор
        public ButtonBack()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_BAEAFD;
            Size = new Size(100, 30); // Размер кнопки по умолчанию

            // Привязываем обработчик события Click
            this.Click += ButtonBack_Click;
        }

        // Обработчик события Click
        private void ButtonBack_Click(object sender, EventArgs e)
        {

        }
    }
}
