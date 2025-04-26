using SchoolDiary.APIConnect;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using SchoolDiary.Models;

namespace SchoolDiary
{
    public partial class SchelduleForTheWeek : Window
    {
        private DateTime _currentWeekStart;
        private static Profile profileWindow;
        private static SchelduleForTheWeek schelduleForTheWeekWindow;
        private static SheduleForTheDay mainWindow;
        private static GradeWindow gradeWindow;

        public SchelduleForTheWeek()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;
            LoadScheduleWeek(new DateTime(2025, 3, 24), new DateTime(2025, 3, 30));
            this.Closing += Window_Closing;
            
            LoadStudentData();

        }
        private async void LoadScheduleWeek(DateTime from, DateTime to)
        {
            APIConnector aPIConnector = APIConnector.GetInstance();
            try
            {
                this.DataContext = new ScheduleViewModel(await aPIConnector.GetWeekSchedule(from,to),from);

            }
            catch (Exception)
            {

            }
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


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Завершаем работу приложения при закрытии любого окна
            Application.Current.Shutdown();
        }

        public static SchelduleForTheWeek GetInstance()
        {
            if (schelduleForTheWeekWindow == null)
            {
                schelduleForTheWeekWindow = new SchelduleForTheWeek(); 
            }
            return schelduleForTheWeekWindow;
        }

        private void OpenSchelduleForTheDate(object sender, RoutedEventArgs e)
        {
            this.Hide();
            if (sender is Button button && button.Tag is DateTime date)
            {
                mainWindow = new SheduleForTheDay(date);

                if (mainWindow.IsVisible)
                {
                    
                    mainWindow.Activate();
                }
                else
                {
                    
                    mainWindow.Show();
                }
            }
        }

        private void OpenProfile(object sender, RoutedEventArgs e)
        {
           
            this.Hide();

            
            profileWindow = Profile.GetInstance();

            if (profileWindow.IsVisible)
            {
                
                profileWindow.Activate();
            }
            else
            {
               
                profileWindow.Show();
            }
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
           
            this.Hide();

            gradeWindow = GradeWindow.GetInstance();
            if (gradeWindow.IsVisible)
            {
                gradeWindow.Activate(); 
            }
            else
            {
                gradeWindow.Show(); 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuHelper.ToggleMenu(this);
        }   

    }

}

