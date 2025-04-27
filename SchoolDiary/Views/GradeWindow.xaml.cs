using SchoolDiary.APIConnect;
using SchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        public class Quarter
        {
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

            public override string ToString()
            {
                return $"{Name}";
            }
        }

        // Создание списка четвертей
        List<Quarter> quarters = new List<Quarter>
        {
            new Quarter { Name = "I", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 10, 28) },
            new Quarter { Name = "II", StartDate = new DateTime(2024, 10, 29), EndDate = new DateTime(2024, 12, 26) },
            new Quarter { Name = "III", StartDate = new DateTime(2024, 12, 27), EndDate = new DateTime(2025, 3, 23) },
            new Quarter { Name = "IV", StartDate = new DateTime(2025, 3, 24), EndDate = new DateTime(2025, 6, 1) }
        };

        private int currentQuarterIndex = 3; // Стартовая четверть (IV)

        public GradeWindow()
        {
            InitializeComponent();
            this.Closing += Window_Closing;

            // Развернуть окно на весь экран
            this.WindowState = WindowState.Maximized;

            LoadStudentData();

            UpdateQuarter();
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
                FontFamily = new FontFamily("/Fonts/Roboto-Medium.ttf#Roboto"), // Указываем шрифт Roboto Medium
                FontSize = 28, // Размер шрифта
                FontWeight = FontWeights.Medium, // Жирность шрифта
                Foreground = Brushes.Black, // Цвет текста
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Добавляем TextBlock внутрь Border
            border.Child = textBlock;

            // Добавляем Border в контейнер
            container.Children.Add(border);
        }

        private void AddFullWidthBorderToSecondColumn(List<string> grades)
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

            bool isFirstGrade = true; // Флаг для проверки первой оценки

            foreach (string grade in grades)
            {
                var gradeConverter = new Converters.GradeToColorConverter();
                Border gradeBorder = new Border
                {
                    Width = 48,
                    Height = 48,
                    Background = gradeConverter.Convert(grade, typeof(Brush), null, CultureInfo.CurrentCulture) as SolidColorBrush, // Цвет фона в зависимости от оценки
                    BorderBrush = gradeConverter.Convert(grade, typeof(Brush), "BorderBrush", CultureInfo.CurrentCulture) as SolidColorBrush, // Цвет границы в зависимости от оценки
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10), // Закругление углов
                    Margin = isFirstGrade ? new Thickness(48, 3, 3, 3) : new Thickness(8, 3, 3, 3) // Отступ для первой оценки и между квадратами
                };

                // Добавляем текст с оценкой внутрь квадрата
                TextBlock gradeText = new TextBlock
                {
                    Text = grade, // Отображаем значение как есть (число или "Н")
                    FontSize = 40,
                    FontFamily = new FontFamily("/Fonts/Montserrat-SemiBold.ttf#Montserrat"),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(0, 4, 0, 0), // Добавляем небольшой отступ сверху
                    Foreground = Brushes.Black
                };
                gradeBorder.Child = gradeText;

                // Добавляем квадрат в контейнер
                container.Children.Add(gradeBorder);

                // После добавления первой оценки меняем флаг
                if (isFirstGrade)
                {
                    isFirstGrade = false;
                }
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
            var gradeConverter = new Converters.GradeToColorConverter();

            // Создаем новый Border
            Border border = new Border
            {
                Width = width,
                Height = 80,
                Background = gradeConverter.Convert(roundedValueForColor.ToString(), typeof(Brush), null, CultureInfo.CurrentCulture) as SolidColorBrush, // Цвет фона в зависимости от округленного значения
                BorderBrush = gradeConverter.Convert(roundedValueForColor.ToString(), typeof(Brush), "BorderBrush", CultureInfo.CurrentCulture) as SolidColorBrush,    // Цвет границы в зависимости от округленного значения
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(32, 8, 8, 8),               // Закругление углов: ВЛ=32, ВП=8, НЛ=8, ПН=8
                Margin = new Thickness(0, 0, 0, 17)                        // Отступ снизу
            };

            // Создаем TextBlock для значения
            TextBlock textBlock = new TextBlock
            {
                Text = value.ToString("0.##"),                             // Отображаем исходное значение
                FontSize = 40,                                            // Устанавливаем размер шрифта
                FontFamily = new FontFamily("/Fonts/Montserrat-SemiBold.ttf#Montserrat"), // Указываем шрифт Montserrat SemiBold
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(0, 4, 0, 0), // Добавляем небольшой отступ сверху
                Foreground = Brushes.Black
            };

            // Добавляем TextBlock внутрь Border
            border.Child = textBlock;

            // Добавляем Border в контейнер
            container.Children.Add(border);
        }

        // Метод для получения цвета фона в зависимости от оценки
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
            APIConnector apiConnector = APIConnector.GetInstance();
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

        private void LeftQuarter_Click(object sender, RoutedEventArgs e)
        {
            isQuarterUpdated = false;
            if (currentQuarterIndex > 0)
            {
                currentQuarterIndex--; // Переход к предыдущей четверти
                UpdateQuarter(); // Обновление интерфейса
            }
        }

        private void RightQuarter_Click(object sender, RoutedEventArgs e)
        {
            isQuarterUpdated = false;
            if (currentQuarterIndex < quarters.Count - 1)
            {
                currentQuarterIndex++; // Переход к следующей четверти
                UpdateQuarter(); // Обновление интерфейса
            }
        }

        private bool isQuarterUpdated = false;

        private void UpdateQuarter()
        {
            if (isQuarterUpdated)
                return; // Выходим, если метод уже вызывался
            Quarter currentQuarter = quarters[currentQuarterIndex];
            QuarterTextBlock.Text = $"{currentQuarter.Name} четверть";

            DateTime fromDate = currentQuarter.StartDate;
            DateTime toDate = currentQuarter.EndDate;
            LoadPerformanceData(fromDate, toDate);

            LeftQuarter.Visibility = currentQuarterIndex > 0 ? Visibility.Visible : Visibility.Collapsed;
            RightQuarter.Visibility = currentQuarterIndex < quarters.Count - 1 ? Visibility.Visible : Visibility.Collapsed;
            isQuarterUpdated = true; // Устанавливаем флаг
        }

        private async void LoadPerformanceData(DateTime fromDate, DateTime toDate)
        {
            try
            {
                APIConnector apiConnector = APIConnector.GetInstance();

                // Получаем данные оценок за определенный период (например, за последний месяц)
                List<SubjectMarks> subjectMarks = await apiConnector.GetMarks(fromDate, toDate);

                // Очищаем предыдущие данные
                FirstColumnContainer.Children.Clear();
                SecondColumnContent.Children.Clear();
                ThirdColumnContainer.Children.Clear();
                FourthColumnContainer.Children.Clear();

                // Добавляем данные в интерфейс
                foreach (var subjectMark in subjectMarks)
                {
                    // 1. Добавляем предмет в первый столбец
                    AddSubjectToColumn(subjectMark.Subject.Name, FirstColumnContainer, 298);

                    // 2. Добавляем оценки во второй столбец
                    var grades = subjectMark.Marks.Select(mark => mark.Value.Name).ToList();
                    AddFullWidthBorderToSecondColumn(grades);

                    // 3. Добавляем среднюю оценку в третий столбец
                    double average = subjectMark.Average ?? 0; // Если среднее значение null, используем 0
                    AddSubjectToThirdOrFourthColumn(average, ThirdColumnContainer, 118);

                    // 4. Добавляем округленную среднюю оценку в четвертый столбец
                    double roundedAverage = Math.Round(average);
                    AddSubjectToThirdOrFourthColumn(roundedAverage, FourthColumnContainer, 118);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить данные успеваемости.");
                Debug.WriteLine($"Ошибка: {ex.Message}");
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
            MenuHelper.ToggleMenu(this);
        }
    }
}
