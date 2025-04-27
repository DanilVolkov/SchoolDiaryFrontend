using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace SchoolDiary.Models
{
    public static class MenuHelper
    {
        public static void ToggleMenu(Window window)
        {
            var menu = window.FindName("Menu") as DockPanel;
            var scheduleButton = window.FindName("Schedule") as Button;
            var gradeButton = window.FindName("Grade") as Button;

            if (menu != null && scheduleButton != null && gradeButton != null)
            {
                if (menu.Width == 216)
                {
                    menu.Width = 64;
                    scheduleButton.Width = 48;
                    gradeButton.Width = 48;
                    menu.Opacity = 1;

                    SetButtonImage(scheduleButton, "pack://application:,,,/Assets/ImageButtons/button_manu_close_schedule_default.png");
                    SetButtonImage(gradeButton, "pack://application:,,,/Assets/ImageButtons/button_menu_close_mark_defoult.png");
                }
                else
                {
                    menu.Width = 216;
                    scheduleButton.Width = 184;
                    gradeButton.Width = 184;
                    menu.Opacity = 0.6;
                    scheduleButton.Opacity = 1;
                    gradeButton.Opacity = 1;

                    SetButtonImage(scheduleButton, "pack://application:,,,/Assets/ImageButtons/button_menu_schedule_default.png");
                    SetButtonImage(gradeButton, "pack://application:,,,/Assets/ImageButtons/button_menu_mark_default.png");
                }
            }
        }

        private static void SetButtonImage(Button button, string imagePath)
        {
            ((ImageBrush)button.Background).ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
        }
    }
}
