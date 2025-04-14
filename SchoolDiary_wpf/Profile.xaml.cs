using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        private static Profile profileWindow;
        private SchelduleForTheWeek schelduleForTheWeekWindow;

        public Profile()
        {
            InitializeComponent();

            // Развернуть окно на весь экран
            this.WindowState = WindowState.Maximized;


            this.Closing += Window_Closing; // Подписываемся на событие закрытия
        }

        // Метод для получения единственного экземпляра окна
        public static Profile GetInstance()
        {
            if (profileWindow == null)
            {
                profileWindow = new Profile(); // Создаём новый экземпляр, если его ещё нет
            }
            return profileWindow;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Завершаем работу приложения при закрытии любого окна
            Application.Current.Shutdown();
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB5A1")); // Цвет при нажатии
            }
        }

        private void TextBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D4AA95")); // Возврат к исходному цвету
            }
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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Действие при клике
            if (Menu.Width == 216)
            {
                Menu.Width = 64;
                Schedule.Width = 48;
                Grade.Width = 48;

                //MainSheduleField.Margin = new Thickness(112, 152, 0, 0);  //сделал эти трансформации отступов для сдвига при нажатии расширения кнопок меню, но судя по всему, по дизайну фигмы оно не нужно
                //TimePeriodPanel.Margin = new Thickness(64, 64, 0, 0);

                ((ImageBrush)Schedule.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
            new System.Uri("../../ImageButtons/button_manu_close_schedule_default.png", System.UriKind.Relative));
                ((ImageBrush)Grade.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                    new System.Uri("../../ImageButtons/button_menu_close_mark_defoult.png", System.UriKind.Relative));

            }
            else
            {
                Menu.Width = 216;
                Schedule.Width = 184;
                Grade.Width = 184;

                //MainSheduleField.Margin = new Thickness(264, 152, 0, 0);
                //TimePeriodPanel.Margin = new Thickness(216, 64, 0, 0);

                // Возвращаем исходные фоновые изображения кнопок
                ((ImageBrush)Schedule.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                    new System.Uri("../../ImageButtons/button_menu_schedule_default.png", System.UriKind.Relative));
                ((ImageBrush)Grade.Background).ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                    new System.Uri("../../ImageButtons/button_menu_mark_default.png", System.UriKind.Relative));
            }
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
