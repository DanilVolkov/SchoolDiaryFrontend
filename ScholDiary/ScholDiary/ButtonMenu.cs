using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SchoolDiary
{
    public class ButtonMenu : ButtonRounded
    {
        public ButtonMenu()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_4EB4D0;
            Size = new Size(100, 30);

            this.Click += ButtonMenu_Click;
        }

        private void ButtonMenu_Click(object sender, EventArgs e)
        {

        }
    }
}
