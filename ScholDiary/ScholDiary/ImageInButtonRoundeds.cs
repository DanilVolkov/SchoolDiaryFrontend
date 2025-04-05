using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchoolDiary
{
    public class ImageInButtonRoundeds
    {

        public static void GroupImageMenuProfileLogo(ButtonMenu buttonMenu, ButtonProfile buttonProfile, ButtonRounded buttonRounded)
        {
            buttonMenu.Text = string.Empty;
            buttonProfile.Text = string.Empty;
            buttonRounded.Text = string.Empty;
            buttonMenu.BackgroundImage = Image.FromFile("../../ImageButtons/button_openmenu_default.png");
            buttonProfile.BackgroundImage = Image.FromFile("../../ImageButtons/button_profile_default.png");
            buttonRounded.BackgroundImage = Image.FromFile("../../ImageButtons/logo-nn.png");
        }

        public static void GroupDarkLightNotifications(ButtonDarkLightMode buttonDarkLight, ButtonNotifications buttonNotifications)
        {
            buttonDarkLight.Text = string.Empty;
            buttonNotifications.Text = string.Empty;
            buttonDarkLight.BackgroundImage = Image.FromFile("../../ImageButtons/Button - icon_daynight.png");
            buttonNotifications.BackgroundImage = Image.FromFile("../../ImageButtons/Button - icon_ring.png");
        }

        // для стрелок назад и вперёд в расписаниях
        public static void GroupBackNext(ButtonBack buttonBack, ButtonNext buttonNext)
        {
            buttonBack.Text = string.Empty;
            buttonNext.Text = string.Empty;
            buttonBack.BackgroundImage = Image.FromFile("../../ImageButtons/button_back_defaoult.png");
            buttonNext.BackgroundImage = Image.FromFile("../../ImageButtons/button_forward_default.png");
        }

        //Тут менюшка ещё влияет на размеры кнопок и смену изображений у расписания на неделю и успеваемость
        public static void GroupSheduleGrade(ButtonShedule buttonShedule, ButtonGrade buttonGrade)
        {
            buttonShedule.Text = string.Empty;
            buttonGrade.Text = string.Empty;
            buttonShedule.BackgroundImage = Image.FromFile("../../ImageButtons/button_menu_schedule_default.png");
            buttonGrade.BackgroundImage = Image.FromFile("../../ImageButtons/button_menu_mark_default.png");
        }

        //кнопка назад для расписания на день, чтобы вернуться к неделе
        public static void GroupPrevius(ButtonPrevius buttonPrevius)
        {
            buttonPrevius.Text = string.Empty;
            buttonPrevius.BackgroundImage = Image.FromFile("../../ImageButtons/button_previous_default.png");
        }
    }
}
