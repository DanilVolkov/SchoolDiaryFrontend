using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SchoolDiary_wpf
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool hasHomework && hasHomework) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public partial class SchelduleForTheWeek : Window
    {
        private static Profile profileWindow;
        private static SchelduleForTheWeek schelduleForTheWeekWindow;

        public SchelduleForTheWeek()
        {
            InitializeComponent();
            DataContext = new ScheduleViewModel();
            this.Closing += Window_Closing;
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
                schelduleForTheWeekWindow = new SchelduleForTheWeek(); // Создаём новый экземпляр, если его ещё нет
            }
            return schelduleForTheWeekWindow;
        }

        private void OpenSchelduleForTheWeek(object sender, RoutedEventArgs e)
        {
            //SchelduleForTheWeek newWindow = new SchelduleForTheWeek();

            //// Скрываем текущее окно
            //this.Hide();

            //// Открываем новое окно
            //newWindow.Show();

            //// Подписываемся на событие закрытия нового окна
            //newWindow.Closed += (s, args) =>
            //{
            //    // Когда новое окно закрывается, показываем текущее окно снова
            //    this.Show();
            //};
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

                //MainSheduleField.Margin = new Thickness(112, 152, 0, 0);  //сделал эти трансформации отступов для сдвига при нажатии расширения кнопок меню, но судя по всему, по дизайну фигмы оно не нужно
                //TimePeriodPanel.Margin = new Thickness(64, 64, 0, 0);

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
                //MainSheduleField.Margin = new Thickness(264, 152, 0, 0);
                //TimePeriodPanel.Margin = new Thickness(216, 64, 0, 0);

                // Возвращаем исходные фоновые изображения кнопок
                ((ImageBrush)Schedule.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri("pack://application:,,,/ImageButtons/button_menu_schedule_default.png", UriKind.Absolute));
                ((ImageBrush)Grade.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                     new Uri("pack://application:,,,/ImageButtons/button_menu_mark_default.png", UriKind.Absolute));
            }
        }

        private void Schedule_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
       
    }

}

