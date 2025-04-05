using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;

namespace SchoolDiary
{
    public partial class Profile: UserControl
    {

        //private List<string> personalData = new List<string>();

        public Form1 parentForm { get; set; }
        private Image ImageAccount;
        private List<string> personalDataExample = new List<string>()
        {
            "Алексей",
            "Смирнов",
            "Петрович",
            "SmirnovAP",
            "15.11.2010",
            "Ученик",
            "8Б"
        };

        public Profile()
        {
            InitializeComponent();

            this.VisibleChanged += Profile_VisibleChanged;
            // Задаем цвет для всех TextBox на форме
            SetTextBoxColor(this, Colors.C_E6D3C7);

            new GeneralSettings(customGroupBox1, customGroupBox2, new GroupBox(), this.ForeColor);

            buttonMenu1.ButtonGrade1 = buttonGrade1;
            buttonMenu1.ButtonShedule1 = buttonShedule1;
        }

        private void Profile_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // Обновляем состояние менюшки при отображении формы
                customGroupBox2.Width = MenuSettings.LastState;

                // Обновляем состояние кнопок при отображении формы
                buttonShedule1.BackgroundImage = ButtonStateManager.GetScheduleImage();
                buttonGrade1.BackgroundImage = ButtonStateManager.GetGradeImage();
                buttonShedule1.Size = ButtonStateManager.GetButtonSize();
                buttonGrade1.Size = ButtonStateManager.GetButtonSize();
            }
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            // Устанавливаем изображения и размеры кнопок
            buttonShedule1.BackgroundImage = ButtonStateManager.GetScheduleImage();
            buttonGrade1.BackgroundImage = ButtonStateManager.GetGradeImage();
            buttonShedule1.Size = ButtonStateManager.GetButtonSize();
            buttonGrade1.Size = ButtonStateManager.GetButtonSize();

            if (buttonShedule1 is ButtonShedule customButton1)
            {
                customButton1.ParentForm = parentForm;
                ImageAccount = parentForm.imageAccount;
            }
            if (buttonGrade1 is ButtonGrade customButton2)
            {
                customButton2.ParentForm = parentForm;
            }
            buttonMenu1.ParentGroupBox = customGroupBox2;
            ImageInButtonRoundeds.GroupImageMenuProfileLogo(buttonMenu1, buttonProfile1, buttonRounded1);
            ImageInButtonRoundeds.GroupDarkLightNotifications(buttonDarkLightMode1, buttonNotifications1);
            ImageInButtonRoundeds.GroupSheduleGrade(buttonShedule1, buttonGrade1);

            buttonMenu1.ParentGroupBox = customGroupBox2;
            customGroupBox2.Width = MenuSettings.LastState; // Устанавливаем последнее состояние

            List<TextBox> allTextBoxes = FindAllTextBoxes(this);
            allTextBoxes.Reverse();
            for(int i = 0; i < allTextBoxes.Count; i++)
            {
                allTextBoxes[i].Text = " " + personalDataExample[i];
            }
        }



        public List<TextBox> FindAllTextBoxes(Control parent)
        {
            List<TextBox> textBoxes = new List<TextBox>();

            foreach (Control control in parent.Controls)
            {
                // Если элемент является TextBox, добавляем его в список
                if (control is TextBox textBox)
                {
                    textBoxes.Add(textBox);
                }

                // Если элемент содержит дочерние элементы, вызываем метод рекурсивно
                if (control.HasChildren)
                {
                    textBoxes.AddRange(FindAllTextBoxes(control));
                }
            }

            return textBoxes;
        }

        private void SetTextBoxColor(Control parent, Color color)
        {
            // Проходим по всем элементам внутри родительского элемента
            foreach (Control control in parent.Controls)
            {
                // Если элемент является TextBox, меняем его цвет
                if (control is TextBox textBox)
                {
                    textBox.BackColor = color;
                }

                // Если элемент содержит вложенные элементы, вызываем метод рекурсивно
                if (control.HasChildren)
                {
                    SetTextBoxColor(control, color);
                }
            }
        }

        private void Profile_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;


            // Под размер изображения
            if (ImageAccount != null)
            {
                // Определяем координаты и размеры изображения
                int x = (Width - ImageAccount.Width * 2) / 2 + 6; // Координата X
                int y = 100; // Координата Y
                int width = ImageAccount.Width - 40; // Ширина изображения (можно настроить)
                int height = ImageAccount.Height - 40; // Высота изображения (можно настроить)

                // Рисуем изображение на форме
                e.Graphics.DrawImage(ImageAccount, new Rectangle(x, y, width, height));
            }
        }

    }
}
