using SchoolDiary.APIConnect;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using SchoolDiary.Models;
namespace SchoolDiary
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class SheduleForTheDay : Window
    {
        private static Profile profileWindow;
        private static SchelduleForTheWeek schelduleForTheWeekWindow;
        private static GradeWindow gradeWindow;

        public SheduleForTheDay()
        {
            InitializeComponent();
            LoadStudentData();
            DataContext = new ViewModel();
            
            // Развернуть окно на весь экран
            this.WindowState = WindowState.Maximized;

            this.Closing += Window_Closing;
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

        private string FormatFullName(Student student)
        {
            return $"{student.LastName}  {student.FirstName[0]}. {student.MiddleName[0]}.";
        }

        public SheduleForTheDay(DateTime Currentdate)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            LoadSchedulDay(Currentdate,Currentdate);
            //ViewModel Schedule = new ViewModel();
            //Schedule.SetupCurrentDate(Currentdate);
            //DataContext = Schedule;
            LoadStudentData();
            this.Closing += Window_Closing;
        }

        private async void LoadSchedulDay(DateTime from, DateTime to)
        {
            APIConnector aPIConnector = APIConnector.GetInstance();
            try
            {
                this.DataContext = new ViewModel(await aPIConnector.GetWeekSchedule(from, to), from);

            }
            catch (Exception)
            {

            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Завершаем работу приложения при закрытии любого окна
            Application.Current.Shutdown();
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

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            // Скрываем текущее окно
            this.Hide();

            gradeWindow = GradeWindow.GetInstance(); // Получаем единственный экземпляр окна
            if (gradeWindow.IsVisible)
            {
                gradeWindow.Activate(); // Активируем, если окно уже открыто
            }
            else
            {
                gradeWindow.Show(); // Показываем, если окно скрыто
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuHelper.ToggleMenu(this);
        }
    }
    
    
   

}

