﻿using SchoolDiary.Objects;
using SchoolDiary.APIConnect;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SchoolDiary
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
            if (Menu.Width == 216)
            {
                Menu.Width = 64;
                Schedule.Width = 48;
                Grade.Width = 48;

                // Изменяем изображения
                ((ImageBrush)Schedule.Background).ImageSource = new BitmapImage(
                    new Uri("pack://application:,,,/ImageButtons/button_manu_close_schedule_default.png", UriKind.Absolute));

                ((ImageBrush)Grade.Background).ImageSource = new BitmapImage(
                    new Uri("pack://application:,,,/ImageButtons/button_menu_close_mark_defoult.png", UriKind.Absolute));
            }
            else
            {
                Menu.Width = 216;
                Schedule.Width = 184;
                Grade.Width = 184;

                // Возвращаем исходные изображения
                ((ImageBrush)Schedule.Background).ImageSource = new BitmapImage(
                    new Uri("pack://application:,,,/ImageButtons/button_menu_schedule_default.png", UriKind.Absolute));

                ((ImageBrush)Grade.Background).ImageSource = new BitmapImage(
                    new Uri("pack://application:,,,/ImageButtons/button_menu_mark_default.png", UriKind.Absolute));
            }
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            APIConnector connector = new APIConnector();
            Student stud = await connector.GetStudent(24);
            NameTextBox.Text = stud.first_name;
        }
    }
}
