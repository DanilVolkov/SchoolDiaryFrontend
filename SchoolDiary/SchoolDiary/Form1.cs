using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolDiary
{
    public partial class Form1: Form
    {
        public Form form1;
        public Profile profile = new Profile();
        public WeeklySchedule weeklySchedule = new WeeklySchedule();
        public Uspevaemost uspevaemost = new Uspevaemost();
        public Image imageAccount; // Переменная для хранения изображения аккаунта

        public Form1()
        {
            InitializeComponent();

            // Подписываемся на событие KeyDown
            this.KeyDown += Form1_KeyDown;

            // Загрузка изображения из папки
            string imagePath = System.IO.Path.Combine(Application.StartupPath, "../../ImageButtons", "Account.jpg");
            try
            {
                imageAccount = Image.FromFile(imagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
            }

            profile.parentForm = this;
            
            weeklySchedule.parentForm = this;

            uspevaemost.parentForm = this;

            this.Text = "SchoolDiary";

            // Добавляем оба элемента на форму
            this.Controls.Add(profile);
            this.Controls.Add(weeklySchedule);
            this.Controls.Add(uspevaemost);

            // По умолчанию показываем только профиль
            //ShowProfile();
            ShowWeeklySchedule();
            this.ClientSize = new Size(weeklySchedule.Right, weeklySchedule.Bottom);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, была ли нажата клавиша ESC
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Закрываем форму
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            // Разрешаем форме перехватывать нажатия клавиш
            this.KeyPreview = true;
        }

        // В дальнейшем для оптимизации надо упростить будет

        // Метод для показа профиля
        public void ShowProfile()
        {
            profile.Visible = true;
            weeklySchedule.Visible = false;
            uspevaemost.Visible = false;
        }

        // Метод для показа расписания на нееделю
        public void ShowWeeklySchedule()
        {
            profile.Visible = false;
            // false для расписания дня
            uspevaemost.Visible = false;
            weeklySchedule.Visible = true;
        }

        //Метод для показа расписания на день
        public void ShowDaySchedule()
        {
            profile.Visible = false;
            weeklySchedule.Visible = false;
            uspevaemost.Visible = false;
            // false для успеваимости
            // true для расписания дня
        }

        //Метод для показа успеваемости
        public void ShowGrade()
        {
            profile.Visible = false;
            weeklySchedule.Visible = false;
            // false для расписания дня
            uspevaemost.Visible = true;
        }
    }
}
