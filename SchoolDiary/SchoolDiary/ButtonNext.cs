using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolDiary
{
    public class ButtonNext : ButtonRounded
    {
        public ButtonNext()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_BAEAFD;
            Size = new Size(100, 30);

            this.Click += ButtonNext_Click;
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {

        }
    }
}
