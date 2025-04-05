using System.Drawing;
using System.Windows.Forms;
using System;

namespace SchoolDiary
{
    public class ButtonMenu : ButtonRounded
    {
        private GroupBox parentGroupBox;

        private ButtonGrade buttonGrade1; // Добавляем ссылку на buttonGrade1
        private ButtonShedule buttonShedule1; // Добавляем ссылку на buttonShedule1

        public ButtonMenu()
        {
            ForeColor = Color.Black;
            BackColor = Colors.C_4EB4D0;
            Size = new Size(100, 30);

            this.Click += ButtonMenu_Click;
        }

        // Свойство для установки ссылки на форму
        public GroupBox ParentGroupBox
        {
            get => parentGroupBox;
            set
            {
                parentGroupBox = value;
            }
        }

        // Свойства для установки ссылок на кнопки
        public ButtonGrade ButtonGrade1
        {
            get => buttonGrade1;
            set => buttonGrade1 = value;
        }

        public ButtonShedule ButtonShedule1
        {
            get => buttonShedule1;
            set => buttonShedule1 = value;
        }


        //private void ButtonMenu_Click(object sender, EventArgs e)
        //{
        //    if (parentGroupBox.Width > MenuSettings.Difference)
        //    {
        //        parentGroupBox.Width -= 110;
        //        MenuSettings.LastState = parentGroupBox.Width;
        //        UpdateButtonImagesAndSizes(false); // Уменьшаем размеры и меняем изображения
        //    }
        //    else
        //    {
        //        parentGroupBox.Width += 110;
        //        MenuSettings.LastState = parentGroupBox.Width;
        //        UpdateButtonImagesAndSizes(true); // Увеличиваем размеры и меняем изображения
        //    }

        //    Refresh();
        //}

        //private void UpdateButtonImagesAndSizes(bool isExpanded)
        //{
        //    if (isExpanded)
        //    {
        //        // Устанавливаем изображения и размеры для расширенного состояния
        //        buttonShedule1.BackgroundImage = ButtonImages.ScheduleBig;
        //        buttonGrade1.BackgroundImage = ButtonImages.GradeBig;
        //        buttonShedule1.Size = new Size(200, 50); // Пример размера для расширенного состояния
        //        buttonGrade1.Size = new Size(200, 50); // Пример размера для расширенного состояния
        //    }
        //    else
        //    {
        //        // Устанавливаем изображения и размеры для уменьшенного состояния
        //        buttonShedule1.BackgroundImage = ButtonImages.ScheduleCollapse;
        //        buttonGrade1.BackgroundImage = ButtonImages.GradeDarkCollapse;
        //        buttonShedule1.Size = new Size(50, 30); // Пример размера для уменьшенного состояния
        //        buttonGrade1.Size = new Size(50, 30); // Пример размера для уменьшенного состояния
        //    }
        //}

        private void ButtonMenu_Click(object sender, EventArgs e)
        {
            // Переключаем состояние
            ButtonStateManager.ToggleState();

            // Обновляем ширину панели
            if (parentGroupBox.Width > MenuSettings.Difference)
            {
                parentGroupBox.Width -= 110;
            }
            else
            {
                parentGroupBox.Width += 110;
            }
            MenuSettings.LastState = parentGroupBox.Width;

            // Обновляем кнопки
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            if (buttonShedule1 != null && buttonGrade1 != null)
            {
                buttonShedule1.BackgroundImage = ButtonStateManager.GetScheduleImage();
                buttonGrade1.BackgroundImage = ButtonStateManager.GetGradeImage();
                buttonShedule1.Size = ButtonStateManager.GetButtonSize();
                buttonGrade1.Size = ButtonStateManager.GetButtonSize();
            }
        }

    }
}
