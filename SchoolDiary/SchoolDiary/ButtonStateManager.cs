using System.Drawing;

namespace SchoolDiary
{
    public static class ButtonStateManager
    {
        // Текущее состояние расширения
        public static bool IsExpanded { get; private set; } = true;

        // Размеры кнопок для расширенного и уменьшенного состояния
        public static Size ExpandedSize { get; } = new Size(200, 50);
        public static Size CollapsedSize { get; } = new Size(48, 48);

        // Изменение состояния
        public static void ToggleState()
        {
            IsExpanded = !IsExpanded;
        }

        // Получение изображений для кнопок
        public static Image GetScheduleImage()
        {
            //return IsExpanded ? ButtonImages.ScheduleBig : ButtonImages.ScheduleCollapse;
            return IsExpanded ? ButtonImages.ScheduleCollapse : ButtonImages.ScheduleBig;
        }

        public static Image GetGradeImage()
        {
            return IsExpanded ? ButtonImages.GradeDarkCollapse :  ButtonImages.GradeBig;
        }

        // Получение размеров кнопок
        public static Size GetButtonSize()
        {
            return IsExpanded ? CollapsedSize : ExpandedSize;
        }
    }
}