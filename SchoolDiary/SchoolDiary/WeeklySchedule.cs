using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SchoolDiary
{
    public partial class WeeklySchedule: UserControl
    {
        public Form1 parentForm { get; set; }

        private Dictionary<ButtonDay, List<CustomGroupBox>> buttonGroupBoxMap = new Dictionary<ButtonDay, List<CustomGroupBox>>();

        private List<CustomGroupBox> customGroupBoxLessons = new List<CustomGroupBox>();
        private List<ButtonDay> buttonsDay = new List<ButtonDay>();

        //------------Надо поправить будет под смену недель
        // Базовое время
        DateTime baseTime = new DateTime(2025, 3, 24, 8, 0, 0); // 8:00

        public WeeklySchedule()
        {
            InitializeComponent();

            this.VisibleChanged += WeeklySchedule_VisibleChanged;
            // Отключаем фокусировку для всех элементов на форме
            DisableFocusForAllControls(this);
            new GeneralSettings(customGroupBox1, customGroupBox3, customGroupBox2, this.ForeColor);

            buttonMenu1.ButtonGrade1 = buttonGrade1;
            buttonMenu1.ButtonShedule1 = buttonShedule1;
        }

        private void WeeklySchedule_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // Обновляем состояние менюшки при отображении формы
                customGroupBox3.Width = MenuSettings.LastState;

                // Обновляем состояние кнопок при отображении формы
                buttonShedule1.BackgroundImage = ButtonStateManager.GetScheduleImage();
                buttonGrade1.BackgroundImage = ButtonStateManager.GetGradeImage();
                buttonShedule1.Size = ButtonStateManager.GetButtonSize();
                buttonGrade1.Size = ButtonStateManager.GetButtonSize();
            }
        }

        private void WeeklySchedule_Load(object sender, EventArgs e)
        {
            // Устанавливаем изображения и размеры кнопок
            buttonShedule1.BackgroundImage = ButtonStateManager.GetScheduleImage();
            buttonGrade1.BackgroundImage = ButtonStateManager.GetGradeImage();
            buttonShedule1.Size = ButtonStateManager.GetButtonSize();
            buttonGrade1.Size = ButtonStateManager.GetButtonSize();


            if (buttonProfile1 is ButtonProfile customButton1)
            {
                customButton1.ParentForm = parentForm;
            }
            if (buttonGrade1 is ButtonGrade customButton2)
            {
                customButton2.ParentForm = parentForm;
            }
            buttonMenu1.ParentGroupBox = customGroupBox3;
            ImageInButtonRoundeds.GroupImageMenuProfileLogo(buttonMenu1, buttonProfile1, buttonRounded1);
            ImageInButtonRoundeds.GroupDarkLightNotifications(buttonDarkLightMode1, buttonNotifications1);
            ImageInButtonRoundeds.GroupBackNext(buttonBack1, buttonNext1);
            ImageInButtonRoundeds.GroupSheduleGrade(buttonShedule1, buttonGrade1);


            SetButtonsDay(this);
            AddGroupBoxesBelowButtons();

            buttonMenu1.ParentGroupBox = customGroupBox3;
            customGroupBox3.Width = MenuSettings.LastState; // Устанавливаем последнее состояние
        }

        private void AddGroupBoxesBelowButtons()
        {
            // Находим все кнопки типа ButtonDay на форме
            List<ButtonDay> buttons = FindControlsByType<ButtonDay>(this);
            buttons.Reverse();

            foreach (ButtonDay button in buttons)
            {

                int distance = 30;
                // Определяем позицию для новых GroupBox
                int groupBoxYOffset = button.Bottom + distance; // Немного ниже кнопки

                // Создаём список для GroupBox текущей кнопки
                List<CustomGroupBox> groupBoxes = new List<CustomGroupBox>();

                for (int i = 0; i < 8; i++)
                {
                    int DV = 0;
                    if(i != 0)
                    {
                        DV = 187 + distance;
                    }

                    // Создаём новый GroupBox
                    CustomGroupBox groupBox = new CustomGroupBox
                    {
                        Text = $"Group {i + 1} below {button.Name}",
                        ShowBorder = true, // Включаем обводку
                        BorderColor = Colors.C_E6D3C7, // Устанавливаем цвет обводки
                        Location = new Point(button.Left, groupBoxYOffset += DV), // Смещаем каждый GroupBox
                        Size = new Size(187, 187),
                        BackColor = Colors.C_FBF2EB // Пример цвета фона
                    };

                    // Добавляем GroupBox на ту же родительскую панель/форму, что и кнопка
                    button.Parent.Controls.Add(groupBox);

                    // Добавляем GroupBox в список
                    groupBoxes.Add(groupBox);
                }
                buttonsDay.Add(button);
                // Добавляем кнопку и её GroupBox в словарь
                buttonGroupBoxMap[button] = groupBoxes;
            }

            var firstEntry = buttonGroupBoxMap.FirstOrDefault();
            if (firstEntry.Key != null)
            {
                List<CustomGroupBox> Coordinate_Y_customGroupBoxes = firstEntry.Value;

                // Проверяем, является ли текущая дата понедельником
                if (baseTime.DayOfWeek != DayOfWeek.Monday)
                {
                    // Находим ближайший понедельник
                    int daysToAdd = ((int)DayOfWeek.Monday - (int)baseTime.DayOfWeek + 7) % 7;
                    DateTime nearestMonday = baseTime.AddDays(daysToAdd);

                    baseTime = nearestMonday;
                }
                else
                {
                    Console.WriteLine("Текущая дата уже является понедельником.");
                }


                // Получаем день и месяц в нужном формате
                string stringFormattedDate1 = $"{baseTime.Day} {GetMonthName(baseTime.Month)}";
                string stringFormattedDate2 = $"{baseTime.AddDays(6).Day} {GetMonthName(baseTime.AddDays(6).Month)}";
                
                label1.Text = stringFormattedDate1 + " - " + stringFormattedDate2;

                for (int i = 0; i < buttonsDay.Count; i++)
                {
                    buttonsDay[i].Text = $"{baseTime.AddDays(i).Day} {GetMonthName(baseTime.AddDays(i).Month)}";
                    buttonsDay[i].Font = new Font("Arial", 14, FontStyle.Bold);
                }


                // Интервалы времени
                TimeSpan intervalLesson = TimeSpan.FromMinutes(45); // 45 минут для каждого интервала
                TimeSpan breakTime = TimeSpan.FromMinutes(10); // 10 минут перерыва

                for (int i = 0; i < 8; i++)
                {
                    // Рассчитываем времена для текущего урока
                    TimeSpan totalInterval = intervalLesson + breakTime;
                    TimeSpan multipliedInterval = new TimeSpan(totalInterval.Ticks * i); // Умножаем через Ticks

                    DateTime upperTime = baseTime.Add(multipliedInterval);
                    DateTime lowerTime = upperTime.Add(intervalLesson);

                    // Создаём CustomGroupBox
                    CustomGroupBox groupBoxLesson = new CustomGroupBox
                    {
                        Text = $"Lesson_{i + 1}",
                        ShowBorder = true, // Включаем обводку
                        BorderColor = Colors.C_AC7356, // Устанавливаем цвет обводки
                        Location = new Point(70, Coordinate_Y_customGroupBoxes[i].Location.Y), // Смещаем каждый GroupBox
                        Size = new Size(80, 187),
                        BackColor = Colors.C_E6D3C7, // Пример цвета фона
                        ShowHorizontalLine = true, // Включаем горизонтальную линию
                        HorizontalLineColor = Color.Black, // Устанавливаем цвет линии
                        HorizontalLineThickness = 2f // Устанавливаем толщину линии
                    };

                    // Добавляем Label для верхнего времени
                    Label upperTimeLabel = new Label
                    {
                        Text = upperTime.ToString("HH:mm"), // Формат времени
                        Font = new Font("Arial", 14, FontStyle.Bold), // Шрифт
                        ForeColor = Color.Black, // Цвет текста
                        AutoSize = true, // Автоматическое изменение размера по тексту
                        Location = new Point(groupBoxLesson.Width / 7, groupBoxLesson.Height / 5) // Центрируем по горизонтали
                    };
                    groupBoxLesson.Controls.Add(upperTimeLabel); // Добавляем Label в CustomGroupBox

                    // Добавляем Label для нижнего времени
                    Label lowerTimeLabel = new Label
                    {
                        Text = lowerTime.ToString("HH:mm"), // Формат времени
                        Font = new Font("Arial", 14, FontStyle.Bold), // Шрифт
                        ForeColor = Color.Black, // Цвет текста
                        AutoSize = true, // Автоматическое изменение размера по тексту
                        Location = new Point(groupBoxLesson.Width / 7, groupBoxLesson.Height / 2 + groupBoxLesson.Height / 5) // Центрируем по горизонтали
                    };
                    groupBoxLesson.Controls.Add(lowerTimeLabel); // Добавляем Label в CustomGroupBox

                    // Добавляем CustomGroupBox на форму
                    this.Controls.Add(groupBoxLesson);

                    // Добавляем CustomGroupBox в список
                    customGroupBoxLessons.Add(groupBoxLesson);
                }
            }
        }

        private static string GetMonthName(int month)
        {
            string[] months = {
                "января", "февраля", "марта", "апреля", "мая", "июня",
                "июля", "августа", "сентября", "октября", "ноября", "декабря"
            };

            return months[month - 1];
        }














        private List<T> FindControlsByType<T>(Control parent) where T : Control
        {
            List<T> result = new List<T>();

            foreach (Control control in parent.Controls)
            {
                if (control is T typedControl)
                {
                    result.Add(typedControl);
                }

                // Рекурсивно ищем в дочерних элементах
                if (control.HasChildren)
                {
                    result.AddRange(FindControlsByType<T>(control));
                }
            }

            return result;
        }

        private void SetButtonsDay(Control parent)
        {
            // Проходим по всем элементам внутри родительского элемента
            foreach (Control control in parent.Controls)
            {
                // Если элемент является Button, выполняем действие
                if (control is ButtonDay buttonDay)
                {
                    buttonDay.ParentForm = parentForm;
                }
            }
        }

        private void DisableFocusForAllControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // Отключаем фокусировку
                control.TabStop = false;

                // Если элемент содержит дочерние элементы, обрабатываем их рекурсивно
                if (control.HasChildren)
                {
                    DisableFocusForAllControls(control);
                }
            }
        }



    }
}
