using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScholDiary
{
    public class SubjectInfoPanel : Panel
    {
        private readonly Label lblSubjectName;
        private readonly Label lblSeparator;
        private readonly PictureBox pbCircle1;
        private readonly PictureBox pbCircle2;
        private readonly Label lblTeacher;
        private readonly Label lblRoom;

        // Добавляем поля для времени начала и конца урока
        private readonly Label lblStartTime;
        private readonly Label lblEndTime;
        private readonly Label lblLineBetweenTimes;

        public SubjectInfoPanel(string subjectName, string teacher, string room, TimeSpan startTime)
        {
            // Настройки панели
            this.Width = 712;
            this.Height = 168;
            this.BackColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Left = 221;
            this.Top = 133;

            // Метка с названием предмета
            lblSubjectName = new Label
            {
                Text = subjectName,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Roboto", 20, FontStyle.Bold),
                Dock = DockStyle.Top,
                Margin = new Padding(5),
                Height = 25
            };
            this.Controls.Add(lblSubjectName);

            // Линия-разделитель
            lblSeparator = new Label
            {
                AutoSize = false,
                Text = "",
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Top,
                Height = 2,
                Margin = new Padding(5, 0, 5, 0), // Отступы по 5 пикселей с каждой стороны
                Width = this.ClientRectangle.Width - 10 // Ширина равна ширине панели минус 10 пикселей (по 5 пикселей с каждого края)
            };
            this.Controls.Add(lblSeparator);

            // Создание круглых изображений для пикчербоксов
            Bitmap circleImage = CreateRoundBitmap(Color.White, 20);

            // Первый кружочек
            pbCircle1 = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(15, 35),
                Image = circleImage,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(pbCircle1);

            // Второй кружочек
            pbCircle2 = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(15, 70),
                Image = circleImage,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(pbCircle2);

            // Информация под кружочками
            lblTeacher = new Label
            {
                Text = $"{teacher}",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10),
                Location = new Point(40, 38),
                Size = new Size(240, 22)
            };
            this.Controls.Add(lblTeacher);

            lblRoom = new Label
            {
                Text = $"Кабинет № {room}",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10),
                Location = new Point(40, 73),
                Size = new Size(240, 22)
            };
            this.Controls.Add(lblRoom);

            // Время начала урока
            lblStartTime = new Label
            {
                Location = new Point(15, 105), // Слева внизу
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblStartTime);

            // Время окончания урока
            lblEndTime = new Label
            {
                Location = new Point(this.Width - 115, 105), // Справа внизу
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblEndTime);

            // Линию между полями времени
            lblLineBetweenTimes = new Label
            {
                AutoSize = false,
                Text = "",
                BorderStyle = BorderStyle.Fixed3D,
                Location = new Point(lblStartTime.Right + 10, lblStartTime.Top + (lblStartTime.Height / 2)),
                Width = lblEndTime.Left - (lblStartTime.Right + 10),
                Height = 1
            };
            this.Controls.Add(lblLineBetweenTimes);

            // Устанавливаем начальное время
            SetLessonTimes(startTime);
        }

        private void SetLessonTimes(TimeSpan startTime)
        {
            // Устанавливаем время начала урока
            lblStartTime.Text = startTime.ToString(@"hh\:mm");

            // Рассчитываем и устанавливаем время окончания урока
            var endTime = startTime.Add(new TimeSpan(0, 45, 0)); // Добавляем 45 минут
            lblEndTime.Text = endTime.ToString(@"hh\:mm");
        }

        // Метод для создания круглого изображения
        private static Bitmap CreateRoundBitmap(Color color, int size)
        {
            Bitmap bitmap = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Brush brush = new SolidBrush(color);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(brush, 0, 0, size, size);
                brush.Dispose();
            }
            return bitmap;
        }
    }
}