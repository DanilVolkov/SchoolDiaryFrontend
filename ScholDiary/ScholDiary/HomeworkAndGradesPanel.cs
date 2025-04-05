using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScholDiary
{
    public class HomeworkAndGradesPanel : Panel
    {
        private readonly Label lblHomeworkTitle;
        private readonly TextBox tbHomework;
        private readonly PictureBox pbGrade1;
        private readonly PictureBox pbGrade2;
        private readonly PictureBox pbGrade3;

        public HomeworkAndGradesPanel(string homework, string grade1, string grade2, string grade3)
        {
            // Фиксированные размеры панели
            this.Width = 1034;
            this.Height = 168;
            this.BackColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;

            // Заголовок для домашнего задания
            lblHomeworkTitle = new Label
            {
                Text = "Домашнее задание:",
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Top = 10,
                Left = 10
            };
            this.Controls.Add(lblHomeworkTitle);

            // Текстовое поле для домашнего задания
            tbHomework = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Arial", 11),
                Top = lblHomeworkTitle.Bottom + 5,
                Left = 10,
                Width = this.Width - 150, // Оставляем место для оценок
                Height = this.Height / 2 - 20
            };
            tbHomework.Text = homework; // Переданное домашнее задание
            this.Controls.Add(tbHomework);

            // Первая оценка
            pbGrade1 = new PictureBox
            {
                Size = new Size(30, 30),
                Location = new Point(this.Width - 85, tbHomework.Top + 10),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Normal
            };
            pbGrade1.Image = CreateSquareBitmap(grade1, Color.White, 30); // Переданная оценка 1
            this.Controls.Add(pbGrade1);

            // Вторая оценка
            pbGrade2 = new PictureBox
            {
                Size = new Size(30, 30),
                Location = new Point(this.Width - 50, tbHomework.Top + 10),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Normal
            };
            pbGrade2.Image = CreateSquareBitmap(grade2, Color.White, 30); // Переданная оценка 2
            this.Controls.Add(pbGrade2);

            // Третья оценка
            pbGrade3 = new PictureBox
            {
                Size = new Size(30, 30),
                Location = new Point(this.Width - 15, tbHomework.Top + 10),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Normal
            };
            pbGrade3.Image = CreateSquareBitmap(grade3, Color.White, 30); // Переданная оценка 3
            this.Controls.Add(pbGrade3);
        }

        // Метод для создания квадратного изображения с текстом внутри
        private static Bitmap CreateSquareBitmap(string text, Color backgroundColor, int size)
        {
            Bitmap bitmap = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(backgroundColor);
                g.DrawString(text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new PointF((size - g.MeasureString(text, new Font("Arial", 14, FontStyle.Bold)).Width) / 2, (size - g.MeasureString(text, new Font("Arial", 14, FontStyle.Bold)).Height) / 2));
            }
            return bitmap;
        }
    }
}