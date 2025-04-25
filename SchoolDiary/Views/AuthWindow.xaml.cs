using System.Windows;
using System.Windows.Controls;


namespace SchoolDiary
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private const string ValidUsername = "123";
        private const string ValidPassword = "123";
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
