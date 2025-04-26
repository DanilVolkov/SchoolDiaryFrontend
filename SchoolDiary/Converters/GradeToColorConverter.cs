using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SchoolDiary.Converters
{
    public class GradeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string grade)
            {
                // Определяем цвет фона (старые цвета остаются без изменений)
                Color backgroundColor;
                switch (grade)
                {
                    case "5":
                        backgroundColor = (Color)ColorConverter.ConvertFromString("#81DF81"); // Светло-зеленый
                        break;
                    case "4":
                        backgroundColor = (Color)ColorConverter.ConvertFromString("#C5E477"); // Светло-желтый
                        break;
                    case "3":
                        backgroundColor = (Color)ColorConverter.ConvertFromString("#FBC351"); // Оранжевый
                        break;
                    case "2":
                        backgroundColor = (Color)ColorConverter.ConvertFromString("#FE7472"); // Красный
                        break;
                    default:
                        backgroundColor = (Color)ColorConverter.ConvertFromString("#808080"); // Серый (по умолчанию)
                        break;
                }

                // Определяем цвет границы (используем новые цвета)
                Color borderColor;
                switch (grade)
                {
                    case "5":
                        borderColor = (Color)ColorConverter.ConvertFromString("#027513"); // Темно-зеленый
                        break;
                    case "4":
                        borderColor = (Color)ColorConverter.ConvertFromString("#49AB49"); // Светло-зеленый
                        break;
                    case "3":
                        borderColor = (Color)ColorConverter.ConvertFromString("#A76E04"); // Оранжевый
                        break;
                    case "2":
                        borderColor = (Color)ColorConverter.ConvertFromString("#9D0404"); // Красный
                        break;
                    default:
                        borderColor = (Color)ColorConverter.ConvertFromString("#404040"); // Серый (по умолчанию)
                        break;
                }

                // Возвращаем Brush в зависимости от параметра
                if (parameter?.ToString() == "BorderBrush")
                {
                    return new SolidColorBrush(borderColor);
                }
                else
                {
                    return new SolidColorBrush(backgroundColor);
                }
            }
            return Brushes.Gray; // По умолчанию
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }



    }
}
