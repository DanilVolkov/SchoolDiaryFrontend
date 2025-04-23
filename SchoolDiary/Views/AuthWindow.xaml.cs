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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolDiary
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private const string ValidUsername = "pupalupa337";
        private const string ValidPassword = "password123";
        public AuthWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (username == ValidUsername && password == ValidPassword)
            {
                ErrorTextBlock.Visibility = Visibility.Collapsed;

                SchelduleForTheWeek mainWindow = new SchelduleForTheWeek();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                ErrorTextBlock.Visibility = Visibility.Visible;
                UsernameTextBox.Focus();
            }
        }
    }
}
