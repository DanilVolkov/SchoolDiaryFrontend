using System;
using System.Drawing;

namespace SchoolDiary
{
    public static class ButtonImages
    {
        // Изображения для расширенной темы
        public static Image ScheduleBig { get; } = Image.FromFile("../../ImageButtons/button_menu_schedule_default.png");
        public static Image GradeBig { get; } = Image.FromFile("../../ImageButtons/button_menu_mark_default.png");

        // Изображения для уменьшенной темы                 
        public static Image ScheduleCollapse { get; } = Image.FromFile("../../ImageButtons/button_manu_close_schedule_default.png");
        public static Image GradeDarkCollapse { get; } = Image.FromFile("../../ImageButtons/button_menu_close_mark_defoult.png");
    }
}