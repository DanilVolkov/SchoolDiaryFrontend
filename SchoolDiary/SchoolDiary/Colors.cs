using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolDiary
{
    public static class Colors
    {
        // Определение цветов с использованием шестнадцатеричных кодов в именах
        public static readonly Color C_7DCCDE = ColorTranslator.FromHtml("#7DCCDE");
        public static readonly Color C_4EB4D0 = ColorTranslator.FromHtml("#4EB4D0");
        public static readonly Color C_BAEAFD = ColorTranslator.FromHtml("#BAEAFD");
        public static readonly Color C_5AB0D8 = ColorTranslator.FromHtml("#5AB0D8");
        public static readonly Color C_609EDE = ColorTranslator.FromHtml("#609EDE");
        public static readonly Color C_E6D3C7 = ColorTranslator.FromHtml("#E6D3C7");
        public static readonly Color C_D4AA95 = ColorTranslator.FromHtml("#D4AA95");
        public static readonly Color C_C28D71 = ColorTranslator.FromHtml("#C28D71");
        public static readonly Color C_AC7356 = ColorTranslator.FromHtml("#AC7356");
        public static readonly Color C_FBF2EB = ColorTranslator.FromHtml("#FBF2EB"); // Возможно не используется
        public static readonly Color C_81DF81 = ColorTranslator.FromHtml("#81DF81");
        public static readonly Color C_C5E477 = ColorTranslator.FromHtml("#C5E477");
        public static readonly Color C_FBC351 = ColorTranslator.FromHtml("#FBC351");
        public static readonly Color C_FE7472 = ColorTranslator.FromHtml("#FE7472");
        public static readonly Color C_9A9A9A = ColorTranslator.FromHtml("#9A9A9A");
        public static readonly Color C_027513 = ColorTranslator.FromHtml("#027513");
        public static readonly Color C_49AB49 = ColorTranslator.FromHtml("#49AB49");
        public static readonly Color C_A76E04 = ColorTranslator.FromHtml("#A76E04");
        public static readonly Color C_9D0404 = ColorTranslator.FromHtml("#9D0404");
        public static readonly Color C_5C5C5C = ColorTranslator.FromHtml("#5C5C5C");
        public static readonly Color C_EFEFEF = ColorTranslator.FromHtml("#EFEFEF");
    }

    public class GeneralSettings
    {
        public GeneralSettings(GroupBox groupBox1, GroupBox groupBox2, GroupBox groupBox3, Color ColorForm)
        {
            groupBox1.BackColor = Colors.C_4EB4D0;
            groupBox2.BackColor =  Colors.C_7DCCDE;
            groupBox3.BackColor = Colors.C_BAEAFD;
            ColorForm = Colors.C_EFEFEF;
        }
    }
}
