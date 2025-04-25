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
            AddFullWidthBorderToSecondColumn("Предмет 1", new List<int> {2, 3, 4, 5});
            AddFullWidthBorderToSecondColumn("Очень длинное название предмета, которое требует горизонтального скролла " +
                "Очень длинное название предмета, которое требует горизонтального скролла " +
                "Очень длинное название предмета, которое требует горизонтального скролла", new List<int> { 5, 4, 3, 2 });

            // Добавляем предметы в третий столбец - средняя оценка
            AddSubjectToColumn((new List<double> { 3, 3, 4, 4 }.Sum() / new List<double> { 2, 3, 4, 5 }.Count).ToString(), ThirdColumnContainer, 118);
            AddSubjectToColumn((new List<double> { 2, 3, 4, 5 }.Sum() / new List<double> { 2, 3, 4, 5 }.Count).ToString(), ThirdColumnContainer, 118);

            // Добавляем предметы в четвёртый столбец - итоговая оценка
            AddSubjectToColumn(Math.Round(new List<double> { 3, 3, 4, 4 }.Sum() / new List<double> { 2, 3, 4, 5 }.Count).ToString(), FourthColumnContainer, 118);
            AddSubjectToColumn(Math.Round(new List<double> { 2, 3, 4, 5 }.Sum() / new List<double> { 2, 3, 4, 5 }.Count).ToString(), FourthColumnContainer, 118);


        }

        private void AddSubjectToColumn(string subjectName, StackPanel container, double width)
        {
            // Создаем новый Border
            Border border = new Border
            {
                Width = width,
                Height = 80,
                Background = Brushes.LightGray,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5), // Закругление углов
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

        //Расчёты для средней и итоговой оценки сделать отдльными функциями для удобства
        // Сделать возможность добавления оценок во второй столбец и располагать их
        private void AddFullWidthBorderToSecondColumn(string subjectName, List<int> ints)
        {
            // Создаем новый Border
            Border border = new Border
            {
                Width = double.NaN, // Автоматическая ширина
                Height = 80,
                Background = Brushes.LightGray,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5), // Закругление углов
                Margin = new Thickness(0, 0, 0, 17) // Отступ снизу
            };

            // Создаем TextBlock для названия предмета
            TextBlock textBlock = new TextBlock
            {
                Text = subjectName,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.NoWrap // Не переносить текст
            };

            // Добавляем TextBlock внутрь Border
            border.Child = textBlock;

            // Добавляем Border в общий контейнер
            SecondColumnContent.Children.Add(border);
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
