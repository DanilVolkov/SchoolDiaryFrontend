using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolDiary
{
    public class ButtonNotifications : ButtonRounded
    {
        public ButtonNotifications()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_4EB4D0;
            Size = new Size(100, 30);

            this.Click += ButtonNotifications_Click;
        }

        private void ButtonNotifications_Click(object sender, EventArgs e)
        {

        }
    }
}
