using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolDiary
{
    public class ButtonDarkLightMode : ButtonRounded
    {

        // Конструктор
        public ButtonDarkLightMode()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_4EB4D0;
            Size = new Size(100, 30);

            this.Click += ButtonDarkLightMode_Click;
        }

        // Обработчик события Click
        private void ButtonDarkLightMode_Click(object sender, EventArgs e)
        {

        }
    }
}
