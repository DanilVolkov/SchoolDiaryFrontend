using SchoolDiary.APIConnect;
using SchoolDiary.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SchoolDiary
{
    public partial class GradeWindow : Window
    {

        private static Profile profileWindow;
        private static SchelduleForTheWeek schelduleForTheWeekWindow;
        private static GradeWindow gradeWindow;

        public GradeWindow()
        {
            InitializeComponent();
            this.Closing += Window_Closing;

            // Развернуть окно на весь экран
            this.WindowState = WindowState.Maximized;
            LoadStudentData();

            // Добавляем предметы в первый столбец
            AddSubjectToColumn("Математика", FirstColumnContainer, 298);
            AddSubjectToColumn("История", FirstColumnContainer, 298);

            // Добавляем предметы во второй столбец
            AddFullWidthBorderToSecondColumn(new List<int> {2, 3, 4, 5});
            AddFullWidthBorderToSecondColumn(new List<int> { 5, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2, 4, 3, 2 });

            // Добавляем предметы в третий столбец - средняя оценка
            AddSubjectToThirdOrFourthColumn((new List<double> { 3, 3, 4, 4 }.Sum() / new List<double> { 2, 3, 4, 5 }.Count), ThirdColumnContainer, 118);
            AddSubjectToThirdOrFourthColumn((new List<double> { 2, 3, 4, 5 }.Sum() / new List<double> { 2, 3, 4, 5 }.Count), ThirdColumnContainer, 118);

            // Добавляем предметы в четвёртый столбец - итоговая оценка
            AddSubjectToThirdOrFourthColumn(Math.Round(new List<double> { 3, 3, 4, 4 }.Sum() / new List<double> { 2, 3, 4, 5 }.Count), FourthColumnContainer, 118);
            AddSubjectToThirdOrFourthColumn(Math.Round(new List<double> { 2, 3, 4, 5 }.Sum() / new List<double> { 2, 3, 4, 5 }.Count), FourthColumnContainer, 118);
        }

        private void AddSubjectToColumn(string subjectName, StackPanel container, double width)
        {
            // Создаем новый Border
            Border border = new Border
            {
                Width = width,
                Height = 80,
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E6D3C7")),
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AC7356")),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8, 8, 32, 8), // Закругление углов: ВЛ=8, ВП=8, ПН=32, НЛ=8
                Margin = new Thickness(0, 0, 0, 17) // Отступ снизу
            };

            // Создаем TextBlock для названия предмета
            TextBlock textBlock = new TextBlock
            {
                Text = subjectName,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Добавляем TextBlock внутрь Border
            border.Child = textBlock;

            // Добавляем Border в контейнер
            container.Children.Add(border);
        }

        private void AddFullWidthBorderToSecondColumn(List<int> grades)
        {
            // Создаем новый Border
            Border border = new Border
            {
                Width = double.NaN, // Автоматическая ширина
                Height = 80,
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBF2EB")),
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D4AA95")),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(32, 8, 32, 8), // Закругление углов: ВЛ=32, ВП=8, ПН=32, НЛ=8
                Margin = new Thickness(0, 0, 0, 17) // Отступ снизу
            };

            // Создаем контейнер для размещения элементов внутри Border
            StackPanel container = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left, // Выравнивание по левому краю
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(8) // Отступ между квадратами
            };

            // Создаем квадраты для каждой оценки
            foreach (int grade in grades)
            {
                Border gradeBorder = new Border
                {
                    Width = 48,
                    Height = 48,
                    Background = GetGradeBackgroundColor(grade), // Цвет фона в зависимости от оценки
                    BorderBrush = GetGradeBorderColor(grade),    // Цвет границы в зависимости от оценки
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(8),          // Закругление углов
                    Margin = new Thickness(3)                   // Отступ между квадратами
                };

                // Добавляем текст с оценкой внутрь квадрата
                TextBlock gradeText = new TextBlock
                {
                    Text = grade.ToString(),
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Brushes.Black
                };

                gradeBorder.Child = gradeText;

                // Добавляем квадрат в контейнер
                container.Children.Add(gradeBorder);
            }

            // Устанавливаем контейнер как содержимое Border
            border.Child = container;

            // Добавляем Border в общий контейнер
            SecondColumnContent.Children.Add(border);
        }

        private void AddSubjectToThirdOrFourthColumn(double value, StackPanel container, double width)
        {
            // Округляем значение для определения цвета
            int roundedValueForColor = (int)Math.Round(value);

            // Создаем новый Border
            Border border = new Border
            {
                Width = width,
                Height = 80,
                Background = GetGradeBackgroundColor(roundedValueForColor), // Цвет фона в зависимости от округленного значения
                BorderBrush = GetGradeBorderColor(roundedValueForColor),    // Цвет границы в зависимости от округленного значения
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(32, 8, 8, 8),               // Закругление углов: ВЛ=32, ВП=8, НЛ=8, ПН=8
                Margin = new Thickness(0, 0, 0, 17)                        // Отступ снизу
            };

            // Создаем TextBlock для значения
            TextBlock textBlock = new TextBlock
            {
                Text = value.ToString("0.##"),                             // Отображаем исходное значение
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.Black
            };

            // Добавляем TextBlock внутрь Border
            border.Child = textBlock;

            // Добавляем Border в контейнер
            container.Children.Add(border);
        }

        // Метод для получения цвета фона в зависимости от оценки
        private SolidColorBrush GetGradeBackgroundColor(int grade)
        {
            switch (grade)
            {
                case 5:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#81DF81"));
                case 4:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C5E477"));
                case 3:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBC351"));
                case 2:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FE7472"));
                default:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#808080"));
            }
        }

        // Метод для получения цвета границы в зависимости от оценки
        private SolidColorBrush GetGradeBorderColor(int grade)
        {
            switch (grade)
            {
                case 5:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#027513"));
                case 4:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#49AB49"));
                case 3:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A76E04"));
                case 2:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9D0404"));
                default:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#808080"));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Завершаем работу приложения при закрытии любого окна
            Application.Current.Shutdown();
        }

        public static GradeWindow GetInstance()
        {
            if (gradeWindow == null)
            {
                gradeWindow = new GradeWindow(); // Создаём новый экземпляр, если его ещё нет
            }
            return gradeWindow;
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
            return $"{student.LastName}  {student.FirstName[0]}. {student.MiddleName[0]}.";
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
           new Uri("pack://application:,,,/Assets/ImageButtons/button_manu_close_schedule_default.png", UriKind.Absolute));
                ((ImageBrush)Grade.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri("pack://application:,,,/Assets/ImageButtons/button_menu_close_mark_defoult.png", UriKind.Absolute));

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
                    new Uri("pack://application:,,,/Assets/ImageButtons/button_menu_schedule_default.png", UriKind.Absolute));
                ((ImageBrush)Grade.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                     new Uri("pack://application:,,,/Assets/ImageButtons/button_menu_mark_default.png", UriKind.Absolute));
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
