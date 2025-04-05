using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SchoolDiary
{
    public partial class Uspevaemost: UserControl
    {
        private Panel scoreStripPanel;
        private Dictionary<string, List<int>> scoresBySubject; // Список оценок по каждому предмету
        private Dictionary<string, (int startIndex, int endIndex)> visibleRangeBySubject; // Диапазон видимых оценок по каждому предмету
        private Dictionary<string, Panel> panelsBySubject; // Словарь для хранения панелей по предметам
        private int currentTopPosition = 10; // Текущая позиция по вертикали для добавления новых панелей


        public Form1 parentForm { get; set; }

        public Uspevaemost()
        {
            InitializeComponent();
            customGroupBox1.BackColor = Colors.C_4EB4D0;
            customGroupBox2.BackColor = Colors.C_7DCCDE;
            customGroupBox3.BackColor = Colors.C_BAEAFD;
            vScrollBar1.BringToFront();

            // Пример данных для двух предметов
            scoresBySubject = new Dictionary<string, List<int>>()
            {
                {"Математика", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
                {"Физика", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 }},
                {"Английский язык", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
                {"Русский язык", new List<int> { 4, 3, 2}},
                {"Литература", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
                {"История России", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 }},
                {"Физ-ра", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
                {"Информатика", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 }},
                {"Химия", new List<int> { 5, 4, 3, 3, 3, 3, 2, 2, 2, 5, 4, 2 }},
                {"Биология", new List<int> { 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2, 5, 4, 3, 2 }},

            };

            visibleRangeBySubject = new Dictionary<string, (int startIndex, int endIndex)>
            {
                {"Математика", (0, 9)}, // Первые 10 оценок для математики
                {"Физика", (0, 9)},
                {"Английский язык", (0, 9)}, // Первые 10 оценок для математики
                {"Русский язык", (0, 9)},// Первые 10 оценок для физики
                {"Литература", (0, 9)}, // Первые 10 оценок для математики
                {"История России", (0, 9)},// Первые 10 оценок для физики
                {"Физ-ра", (0, 9)}, // Первые 10 оценок для математики
                {"Информатика", (0, 9)},// Первые 10 оценок для физики
                {"Химия", (0, 9)}, // Первые 10 оценок для математики
                {"Биология", (0, 9)},// Первые 10 оценок для физики
                
            };

            // Инициализация словаря panelsBySubject
            panelsBySubject = new Dictionary<string, Panel>();

            // Создаем главную панель и добавляем ее на форму
            scoreStripPanel = new Panel
            {
                Width = 1776,  // Ширина основной панели
                Height = 1500,  // Увеличили высоту для размещения нескольких предметов
                BackColor = Color.White,
                Location = new Point(88, 154)
            };
            this.Controls.Add(scoreStripPanel);

            // Добавление панелей и кнопок для каждого предмета
            foreach (string subject in scoresBySubject.Keys)
            {
                CreateScoreStripForSubject(subject);
                AddInitialScores(subject);
            }
        }

        private void Uspevaemost_Load(object sender, EventArgs e)
        {
            //if (buttonGrade1 is ButtonGrade customButton2)
            //{
            //    customButton2.ParentForm = parentForm;
            //}
        }

        private void CreateScoreStripForSubject(string subject)
        {
            // Основная панель для текущего предмета
            Panel scoresPanel = new Panel
            {
                Width = 88 * 10,   // Ширина полосы с оценками
                Height = 120,    // Высота полосы с оценками
                Left = 490 + 80,      // Смещение от левого края основной панели
                Top = currentTopPosition + 21, // Смещаем каждую новую панель вниз
                BackColor = Colors.C_C28D71,

            };
            panelsBySubject[subject] = scoresPanel;
            scoreStripPanel.Controls.Add(scoresPanel);

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
                Width = 79,
                Height = 72,
                Left = 490,      // Смещение от левого края основной панели
                Top = currentTopPosition + 21 + 16, // Центрирование кнопки относительно текущей панели
                FlatStyle = FlatStyle.Flat,
                Tag = $"{subject}_left"  // Устанавливаем тег для поиска кнопки
            };
            leftArrowButton.Click += LeftArrowClick;
            leftArrowButton.Enabled = false; // Изначально скрыта, так как нет предыдущих оценок

            // Кнопка для прокрутки вправо
            Button rightArrowButton = new Button
            {
                Text = ">",
                Width = 79,
                Height = 72,
                Left = scoreStripPanel.Width - 2 * 490 + 220,     // Смещение от правого края основной панели
                Top = currentTopPosition + 21 + 16, // Центрирование кнопки относительно текущей панели
                FlatStyle = FlatStyle.Flat,
                Tag = $"{subject}_right",  // Устанавливаем тег для поиска кнопки

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
                if (i >= scoresBySubject[subject].Count) break; // Проверяем, чтобы не выйти за пределы списка оценок

                PictureBox scoreBox = new PictureBox
                {
                    Width = 88,           // Ширина пикчербокса
                    Height = 88,          // Высота пикчербокса
                    BorderStyle = BorderStyle.FixedSingle,
                    Left = (i - startIndex) * 93,  // Позиционируем пикчербокс слева направо
                    BackColor = GetScoreColor(scoresBySubject[subject][i]),
                    //BorderColor = GetScoreBorderColor(scoresBySubject[subject][i]),
                    Top = 16
                };

                Label scoreLabel = new Label
                {
                    AutoSize = false,
                    BackColor = Color.Transparent,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = scoresBySubject[subject][i].ToString(),
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

            if (endIndex <= scoresBySubject[subject].Count)
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
            bool canGoRight = endIndex < scoresBySubject[subject].Count - 1;

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


        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            scoreStripPanel.Top = -vScrollBar1.Value;
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
                BackColor = Colors.C_C28D71, // Светло-розовый цвет фона
                BorderStyle = BorderStyle.FixedSingle, // Граница вокруг прямоугольника
                Padding = new Padding(8, 6, 8, 6), // Внутренние отступы
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

        private void vScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            scoreStripPanel.Top = -vScrollBar1.Value;

        }
    }
}
