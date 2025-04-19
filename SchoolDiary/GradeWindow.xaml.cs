using SchoolDiary.APIConnect;
using SchoolDiary.Objects;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SchoolDiary
{
    public partial class GradeWindow : Window
    {

        private static Profile profileWindow;
        private static SchelduleForTheWeek schelduleForTheWeekWindow;
        private static MainWindow mainWindow;

        public GradeWindow()
        {
            InitializeComponent();

            // Развернуть окно на весь экран
            this.WindowState = WindowState.Maximized;
            LoadStudentData();
        }


        private async void LoadStudentData()
        {
            APIConnector apiConnector = new APIConnector();
            try
            {
                Student student = await apiConnector.GetStudent();
                // Устанавливаем ФИО в TextBlock кнопки профиля
                FullNameTextBlock.Text = FormatFullName(student);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось загрузить данные студента.");
            }
        }

        private string FormatFullName(Student student)
        {
            string initials = "";
            if (!string.IsNullOrEmpty(student.FirstName))
            {
                initials += $"{student.FirstName[0]}.";
            }
            if (!string.IsNullOrEmpty(student.MiddleName))
            {
                initials += $"{student.MiddleName[0]}.";
            }

            return $"{student.LastName} {initials}";
        }

        private void OpenSchelduleForTheWeek(object sender, RoutedEventArgs e)
        {
            // Скрываем текущее окно
            this.Hide();

            schelduleForTheWeekWindow = SchelduleForTheWeek.GetInstance(); // Получаем единственный экземпляр окна
            if (schelduleForTheWeekWindow.IsVisible)
            {
                schelduleForTheWeekWindow.Activate(); // Активируем, если окно уже открыто
            }
            else
            {
                schelduleForTheWeekWindow.Show(); // Показываем, если окно скрыто
            }
        }

        private void OpenProfile(object sender, RoutedEventArgs e)
        {
            // Скрываем текущее окно
            this.Hide();

            // Получаем единственный экземпляр окна профиля
            profileWindow = Profile.GetInstance();

            if (profileWindow.IsVisible)
            {
                // Если окно уже открыто, активируем его
                profileWindow.Activate();
            }
            else
            {
                // Если окно скрыто, показываем его
                profileWindow.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Действие при клике
            if (Menu.Width == 216)
            {
                Menu.Width = 64;
                Schedule.Width = 48;
                Grade.Width = 48;
                Menu.Opacity = 1;

                ((ImageBrush)Schedule.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
           new Uri("pack://application:,,,/ImageButtons/button_manu_close_schedule_default.png", UriKind.Absolute));
                ((ImageBrush)Grade.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri("pack://application:,,,/ImageButtons/button_menu_close_mark_defoult.png", UriKind.Absolute));

            }
            else
            {
                Menu.Width = 216;
                Schedule.Width = 184;
                Grade.Width = 184;
                Menu.Opacity = 0.6;
                Schedule.Opacity = 1;
                Grade.Opacity = 1;

                // Возвращаем исходные фоновые изображения кнопок
                ((ImageBrush)Schedule.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri("pack://application:,,,/ImageButtons/button_menu_schedule_default.png", UriKind.Absolute));
                ((ImageBrush)Grade.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                     new Uri("pack://application:,,,/ImageButtons/button_menu_mark_default.png", UriKind.Absolute));
            }
        }

        public class IndexToOffsetConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                // Получаем элемент управления
                if (value is FrameworkElement element && element.DataContext is int index)
                {
                    // Максимальное смещение (например, 100 пикселей)
                    double maxOffset = 100;

                    // Смещение для каждой оценки: каждая оценка сдвигается влево на 13.3 пикселя
                    double offset = Math.Min(index * 13.3, maxOffset);

                    // Возвращаем отрицательное значение для наложения
                    return -offset;
                }
                return 0;
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

        }

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
                            borderColor = (Color)ColorConverter.ConvertFromString("#808080"); // Серый (по умолчанию)
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

        private void Grade_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
