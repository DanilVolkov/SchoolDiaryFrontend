using ScholDiary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SchoolDiary
{
    public partial class Grade : UserControl
    {
        private Panel scoreStripPanel;
        private Dictionary<string, List<int>> scoresBySubject; // Список оценок по каждому предмету
        private Dictionary<string, (int startIndex, int endIndex)> visibleRangeBySubject; // Диапазон видимых оценок по каждому предмету
        private Dictionary<string, Panel> panelsBySubject; // Словарь для хранения панелей по предметам
        private int currentTopPosition = 10; // Текущая позиция по вертикали для добавления новых панелей
        Dictionary<string, Dictionary<string, List<int>>> quarterScoresBySubject;
        private string currentQuarter = "I четверть";
        public Grade()
        {
            InitializeComponent();

            customGroupBox1.BackColor = Colors.C_4EB4D0;
            customGroupBox2.BackColor = Colors.C_7DCCDE;
            customGroupBox3.BackColor = Colors.C_BAEAFD;
            vScrollBar1.BringToFront();
            label1.Text = "I четверть";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Font = new Font("Roboto", 20, FontStyle.Regular);
            // Пример данных для двух предметов
            //scoresBySubject = new Dictionary<string, List<int>>()
            //{
            //    {"Математика", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
            //    {"Физика", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 }},
            //    {"Английский язык", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
            //    {"Русский язык", new List<int> { 4, 3, 2}},
            //    {"Литература", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
            //    {"История России", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 }},
            //    {"Физ-ра", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
            //    {"Информатика", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 }},
            //    {"Химия", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
            //    {"Биология", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 2 , 2 }},

            //};
            quarterScoresBySubject = new Dictionary<string, Dictionary<string, List<int>>>()
            {
                {
                    "Математика", new Dictionary<string, List<int>>
                    {
                        { "I четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                        { "II четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                        { "III четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                        { "IV четверть", new List<int> { 4, 3, 2, 5 } }
                    }
                },
                {
                    "Физика", new Dictionary<string, List<int>>
                    {
                        { "I четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                        { "II четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                        { "III четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                        { "IV четверть", new List<int> { 4 } }
                    }
                },
                        {
                "Английский язык", new Dictionary<string, List<int>>()
                {
                    { "I четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "II четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "III четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "IV четверть", new List<int> { 4 } }
                }
            },
            {
                "Русский язык", new Dictionary<string, List<int>>()
                {
                    { "I четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "II четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "III четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "IV четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } }
                }
            },
            {
                "Литература", new Dictionary<string, List<int>>()
                {
                    { "I четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "II четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "III четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "IV четверть", new List<int> { 4 } }
                }
            },
            {
                "История России", new Dictionary<string, List<int>>()
                {
                    { "I четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "II четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "III четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "IV четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } }
                }
            },
            {
                "Физ-ра", new Dictionary<string, List<int>>()
                {
                    { "I четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "II четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "III четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "IV четверть", new List<int> { 4 } }
                }
            },
            {
                "Информатика", new Dictionary<string, List<int>>()
                {
                    { "I четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "II четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "III четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "IV четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } }
                }
            },
            {
                "Химия", new Dictionary<string, List<int>>()
                {
                    { "I четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "II четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "III четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "IV четверть", new List<int> { 4 } }
                }
            },
            {
                "Биология", new Dictionary<string, List<int>>()
                {
                    { "I четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "II четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } },
                    { "III четверть", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 } },
                    { "IV четверть", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 } }
                }
            }

                // Аналогично для остальных предметов
            };

            visibleRangeBySubject = new Dictionary<string, (int startIndex, int endIndex)>
            {
                {"Математика", (0, 6)}, // Первые 10 оценок для математики
                {"Физика", (0, 6)},
                {"Английский язык", (0, 6)}, // Первые 10 оценок для математики
                {"Русский язык", (0, 6)},// Первые 10 оценок для физики
                {"Литература", (0, 6)}, // Первые 10 оценок для математики
                {"История России", (0, 6)},// Первые 10 оценок для физики
                {"Физ-ра", (0, 6)}, // Первые 10 оценок для математики
                {"Информатика", (0, 6)},// Первые 10 оценок для физики
                {"Химия", (0, 6)}, // Первые 10 оценок для математики
                {"Биология", (0, 6)},// Первые 10 оценок для физики
            
            };

            // Инициализация словаря panelsBySubject
            panelsBySubject = new Dictionary<string, Panel>();

            // Создаем главную панель и добавляем ее на форму
            scoreStripPanel = new Panel
            {
                Width = 1776,  // Ширина основной панели
                Height = 150* quarterScoresBySubject.Keys.Count + 21,  // Увеличили высоту для размещения нескольких предметов
                BackColor = Color.White,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom, // Элемент привязан ко всем сторонам формы
                Location = new Point(80, 154), // Позиция задана относительно родителя
                AutoScroll = true, // Включаем автоматическую прокрутку, если контент выходит за пределы панели
            };
            this.Controls.Add(scoreStripPanel);
            vScrollBar1.Maximum = 150 * quarterScoresBySubject.Keys.Count / 2 + 21;
            vScrollBar1.Minimum = -120;
            // Добавление панелей и кнопок для каждого предмета
            foreach (string subject in quarterScoresBySubject.Keys)
            {
                CreateScoreStripForSubject(subject);
                AddInitialScores(subject);
            }
            ImageInButtonRoundeds.GroupBackNext(buttonBack1, buttonNext1);
            ImageInButtonRoundeds.GroupImageMenuProfileLogo(buttonMenu2, buttonProfile2,buttonRounded2);
            ImageInButtonRoundeds.GroupSheduleGrade(buttonShedule2, buttonGrade2);
            ImageInButtonRoundeds.GroupDarkLightNotifications(buttonDarkLightMode1, buttonNotifications1);
        }

        private void CreateScoreStripForSubject(string subject)
        {
            // Основная панель для текущего предмета
            Panel scoresPanel = new Panel
            {
                Width = 88 * 10,   // Ширина полосы с оценками
                Height = 120,    // Высота полосы с оценками
                Left = 490 + 65,      // Смещение от левого края основной панели
                Top = currentTopPosition + 21, // Смещаем каждую новую панель вниз
                BackColor = Colors.C_E6D3C7,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top, // Привязываем к левому краю и сверху
            };
            panelsBySubject[subject] = scoresPanel;
            scoreStripPanel.Controls.Add(scoresPanel);
            scoresPanel.BringToFront();

            // Создаем скругленный прямоугольник с названием предмета
            Label roundedRect = CreateRoundedRectangleWithText(subject, currentTopPosition);
            scoreStripPanel.Controls.Add(roundedRect);

            // Создаем кнопки прокрутки для каждого предмета
            CreateNavigationButtons(subject);

            // Увеличиваем позицию для следующего предмета
            currentTopPosition += 151; // 40 высота панели + 10 отступ между панелями
        }

        private void CreateNavigationButtons(string subject)
        {
            // Кнопка для прокрутки влево
            Button leftArrowButton = new Button
            {
                Text = "<",
                Font = new Font("Roboto", 28, FontStyle.Regular),
                BackColor = Colors.C_E6D3C7,
                Width = 88,
                Height = 88,
                Left = 480+80,      // Смещение от левого края основной панели
                
                Top = currentTopPosition + 21 + 16, // Центрирование кнопки относительно текущей панели
                FlatStyle = FlatStyle.Flat,
                Tag = $"{subject}_left",  // Устанавливаем тег для поиска кнопки
                Anchor = AnchorStyles.Left | AnchorStyles.Top, // Привязываем к левому краю и сверху
            };
            leftArrowButton.Click += LeftArrowClick;
            leftArrowButton.Enabled = false; // Изначально скрыта, так как нет предыдущих оценок

            // Кнопка для прокрутки вправо
            Button rightArrowButton = new Button
            {
                Text = ">",
                Font = new Font("Roboto", 28, FontStyle.Regular),
                BackColor = Colors.C_E6D3C7,
                Width = 88,
                Height = 88,
                Left = scoreStripPanel.Width -  490+45,     // Смещение от правого края основной панели
                Top = currentTopPosition + 21 + 16, // Центрирование кнопки относительно текущей панели
                FlatStyle = FlatStyle.Flat,
                Tag = $"{subject}_right",  // Устанавливаем тег для поиска кнопки
                Anchor = AnchorStyles.Right | AnchorStyles.Top, // Привязываем к правому краю и сверху
            };
            rightArrowButton.Click += RightArrowClick;

            // Добавляем кнопки на основную панель
            scoreStripPanel.Controls.Add(leftArrowButton);
            scoreStripPanel.Controls.Add(rightArrowButton);
            leftArrowButton.BringToFront();
            rightArrowButton.BringToFront();
        }
        private void AddInitialScores(string subject)
        {
            Panel scoresPanel = panelsBySubject[subject]; // Получаем правильную панель для предмета
            scoresPanel.Controls.Clear(); // Очищаем предыдущие пикчербоксы

            (int startIndex, int endIndex) = visibleRangeBySubject[subject];

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (i >= quarterScoresBySubject[subject][currentQuarter].Count) break; // Проверяем, чтобы не выйти за пределы списка оценок

                PictureBox scoreBox = new PictureBox
                {
                    Width = 88,           // Ширина пикчербокса
                    Height = 88,          // Высота пикчербокса
                    BorderStyle = BorderStyle.FixedSingle,
                    Left = (i - startIndex) * 93 + 110,  // Позиционируем пикчербокс слева направо
                    BackColor = GetScoreColor(quarterScoresBySubject[subject][currentQuarter][i]),
                    //BorderColor = GetScoreBorderColor(scoresBySubject[subject][i]),
                    Top = 16,
                    Anchor = AnchorStyles.Left | AnchorStyles.Top, // Привязываем к левому краю и сверху
                };

                Label scoreLabel = new Label
                {
                    AutoSize = false,
                    BackColor = Color.Transparent,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = quarterScoresBySubject[subject][currentQuarter][i].ToString(),
                    Dock = DockStyle.Fill
                };

                scoreBox.Controls.Add(scoreLabel);
                scoresPanel.Controls.Add(scoreBox);
            }

            CheckButtonsVisibility(subject);
        }

        private void LeftArrowClick(object sender, EventArgs e)
        {
            string subject = ((Button)sender).Tag.ToString().Split('_')[0]; // Извлекаем название предмета из тега кнопки

            (int startIndex, int endIndex) = visibleRangeBySubject[subject];

            if (startIndex > 0)
            {
                startIndex--;
                endIndex--;
                visibleRangeBySubject[subject] = (startIndex, endIndex);
                AddInitialScores(subject);
            }
            CheckButtonsVisibility(subject);
        }

        private void RightArrowClick(object sender, EventArgs e)
        {
            string subject = ((Button)sender).Tag.ToString().Split('_')[0]; // Извлекаем название предмета из тега кнопки

            (int startIndex, int endIndex) = visibleRangeBySubject[subject];

            if (endIndex <= quarterScoresBySubject[subject][currentQuarter].Count)
            {
                startIndex++;
                endIndex++;
                visibleRangeBySubject[subject] = (startIndex, endIndex);
                AddInitialScores(subject);
            }
            CheckButtonsVisibility(subject);
        }

        private void CheckButtonsVisibility(string subject)
        {
            (int startIndex, int endIndex) = visibleRangeBySubject[subject];

            bool canGoLeft = startIndex > 0;
            bool canGoRight = endIndex < quarterScoresBySubject[subject][currentQuarter].Count - 1;

            Button leftButton = FindButtonBySubjectAndSide(subject, "left");
            Button rightButton = FindButtonBySubjectAndSide(subject, "right");

            leftButton.Visible = canGoLeft;
            leftButton.Enabled = canGoLeft;
            rightButton.Visible = canGoRight;

            rightButton.Enabled = canGoRight;
        }

        private Button FindButtonBySubjectAndSide(string subject, string side)
        {
            foreach (Control control in scoreStripPanel.Controls)
            {
                if (control is Button button && button.Tag != null && button.Tag.Equals($"{subject}_{side}"))
                {
                    return button;
                }
            }
            throw new Exception($"Button not found for subject '{subject}' and side '{side}'.");
        }

        private Color GetScoreColor(int score)
        {
            switch (score)
            {
                case 5:
                    return Colors.C_81DF81;
                case 4:
                    return Colors.C_C5E477;
                case 3:
                    return Colors.C_FBC351;
                default:
                    return Colors.C_FE7472;
            }
        }
        private Color GetScoreBorderColor(int score)
        {
            switch (score)
            {
                case 5:
                    return Colors.C_027513;
                case 4:
                    return Colors.C_49AB49;
                case 3:
                    return Colors.C_A76E04;
                default:
                    return Colors.C_9D0404;
            }
        }

        

        private Label CreateRoundedRectangleWithText(string text, int topPosition)
        {
            Label label = new Label
            {
                Text = text,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Size = new Size(480, 120), // Размер прямоугольника
                Location = new Point(10, topPosition + 21), // Расположение относительно верхней границы
                BackColor = Colors.C_E6D3C7, // 
                BorderStyle = BorderStyle.FixedSingle, // Граница вокруг прямоугольника
                Padding = new Padding(8, 6, 8, 6), // Внутренние отступы
                Anchor = AnchorStyles.Left | AnchorStyles.Top, // Привязываем к левому краю и сверху
            };

            // Применение стиля скругления углов
            label.Paint += (s, e) =>
            {
                GraphicsPath path = new GraphicsPath();
                //path.AddLine(label.Left + 20, label.Top, label.Left + label.Width - 21, label.Top);
                //path.AddArc(label.Left + label.Width - 41, label.Top, 20, 20, 270, 90);
                //path.AddLine(label.Left + label.Width - 1, label.Top + 20, label.Left + label.Width - 1, label.Bottom - 21);
                //path.AddArc(label.Left + label.Width - 41, label.Bottom - 41, 20, 20, 0, 90);
                //path.AddLine(label.Left + label.Width - 21, label.Bottom - 1, label.Left + 21, label.Bottom - 1);
                //path.AddArc(label.Left, label.Bottom - 41, 20, 20, 90, 90);
                //path.AddLine(label.Left + 1, label.Bottom - 21, label.Left + 1, label.Top + 21);
                //path.AddArc(label.Left, label.Top, 20, 20, 180, 90);
                //path.CloseFigure();

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                SolidBrush brush = new SolidBrush(label.BackColor);
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(Pens.Black, path);
            };

            return label;
        }

        private void customGroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            //scoreStripPanel.Top = -vScrollBar1.Value*141;
            scoreStripPanel.Top = -vScrollBar1.Value;
           
            //if (scoreStripPanel.Top == 0)
            //{
            //    // Принудительно сбрасываем значение скроллбара на ноль
            //    vScrollBar1.Value = 0;
            //}

            Refresh();

        }

        

        private void buttonNext1_Click(object sender, EventArgs e)
        {
            if (currentQuarter != "IV четверть")
            {
                currentQuarter = GetNextQuarter(currentQuarter); ; // Переход к предыдущей четверти
                label1.Text = currentQuarter;
                UpdateVisibleRangeBySubject(currentQuarter);
                AddInitialScoresForAllSubjects();
            }
        }

        private void buttonBack1_Click(object sender, EventArgs e)
        {
            if (currentQuarter !="I четверть")
            {
                currentQuarter= GetPreviousQuarter(currentQuarter); // Переход к следующей четверти
                label1.Text = currentQuarter;
                UpdateVisibleRangeBySubject(currentQuarter);
                AddInitialScoresForAllSubjects();
            }
        }
        private string GetPreviousQuarter(string currentQuarter)
        {
            switch (currentQuarter)
            {
                case "II четверть": return "I четверть";
                case "III четверть": return "II четверть";
                case "IV четверть": return "III четверть";
                default: return currentQuarter;
            }
        }

        private string GetNextQuarter(string currentQuarter)
        {
            switch (currentQuarter)
            {
                case "I четверть": return "II четверть";
                case "II четверть": return "III четверть";
                case "III четверть": return "IV четверть";
                default: return currentQuarter;
            }
        }
        private void UpdateVisibleRangeBySubject(string quarterNumber)
        {
            foreach (var subject in quarterScoresBySubject.Keys)
            {
                // Определяем стартовую и конечную позицию для данной четверти
                int startIndex = 0;
                int endIndex = 6;

                // Сохраняем диапазон в словарь
                visibleRangeBySubject[subject] = (startIndex, endIndex);
            }
        }
        private void AddInitialScoresForAllSubjects()
        {
            foreach (var subject in quarterScoresBySubject.Keys)
            {
                AddInitialScores(subject);
            }
        }

        private void buttonShedule2_Click(object sender, EventArgs e)
        {
            ScheduleForTheDay scheduleForTheDay = new ScheduleForTheDay();
            
            
        }
    }
}
