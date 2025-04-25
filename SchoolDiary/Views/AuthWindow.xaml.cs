using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Resources;
using SchoolDiary.APIConnect;

namespace SchoolDiary
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            APIConnector connector = APIConnector.GetInstance();
            if (await connector.AuthUser(username, password))
            {
                ErrorTextBlock.Visibility = Visibility.Collapsed;
                SchelduleForTheWeek mainWindow = new SchelduleForTheWeek();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                ErrorTextBlock.Visibility = Visibility.Visible;

                WrongInput(sender, e);

                //UsernameTextBox.Focus();
            }
        }
        private void WrongInput(object sender, RoutedEventArgs e)
        {
            Uri resourceUri = new Uri("Assets/ImageButtons/Authentication_WrongInput.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;

            UsernameTextBox.Background = brush;
            PasswordBox.Background = brush;
            PasswordTextBox.Background = brush;
        }

        private void NormalInput(object sender, RoutedEventArgs e)
        {
            Uri resourceUri = new Uri("Assets/ImageButtons/Background_Authorization_Fields.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;

            UsernameTextBox.Background = brush;
            PasswordBox.Background = brush;
            PasswordTextBox.Background = brush;
        }

        private void Login_MouseDown(object sender, RoutedEventArgs e)
        {
            NormalInput(sender, e);
            if (UsernameTextBox.Text == "Логин") { 
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = Brushes.Black;
            }
        }

        private void Login_LostCapture(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "")
            {
                UsernameTextBox.Text = "Логин";
                UsernameTextBox.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#B5AEA9");
            }
        }

        private void Placeholder_MouseDown(object sender, RoutedEventArgs e){
            PasswordTextBox.Visibility = Visibility.Hidden;
            PasswordBox.Clear();
            PasswordBox.Focus();
            NormalInput(sender, e);
        }

        private void Placeholder_LostCapture(object sender, RoutedEventArgs e)
        {
            if(PasswordBox.Password == "") PasswordTextBox.Visibility = Visibility.Visible;
            NormalInput(sender, e);
        }
    }
}
