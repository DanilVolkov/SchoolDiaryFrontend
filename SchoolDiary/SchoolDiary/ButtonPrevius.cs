using System;
using System.Drawing;
using System.Windows.Forms;


namespace SchoolDiary
{
    public class ButtonPrevius : ButtonRounded
    {
        public ButtonPrevius()
        {
            ForeColor = Color.Black;

            Size = new Size(100, 30);

            this.Click += ButtonPrevius_Click;
        }

        // Обработчик события Click
        private void ButtonPrevius_Click(object sender, EventArgs e)
        {

        }
    }
}
