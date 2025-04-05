using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace SchoolDiary
{
    public class ButtonRounded : Control
    {
        private Image _backgroundImage;

        [Description("Фоновое изображение кнопки")]
        public override Image BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                Refresh(); // Перерисовываем элемент при изменении изображения
            }
        }

        private ImageLayout _backgroundImageLayout = ImageLayout.Stretch;

        [Description("Режим масштабирования фонового изображения")]
        public override ImageLayout BackgroundImageLayout
        {
            get => _backgroundImageLayout;
            set
            {
                _backgroundImageLayout = value;
                Refresh(); // Перерисовываем элемент при изменении режима
            }
        }

        private ContentAlignment _textAlign = ContentAlignment.MiddleCenter;

        [Description("Выравнивание текста внутри кнопки")]
        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set
            {
                if (_textAlign != value)
                {
                    _textAlign = value;
                    UpdateStringFormat(); // Обновляем настройки StringFormat
                    Refresh(); // Перерисовываем элемент
                }
            }
        }

        private void UpdateStringFormat()
        {
            SF.Alignment = StringAlignment.Near; // По умолчанию
            SF.LineAlignment = StringAlignment.Near;

            switch (_textAlign)
            {
                case ContentAlignment.TopLeft:
                    SF.Alignment = StringAlignment.Near;
                    SF.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopCenter:
                    SF.Alignment = StringAlignment.Center;
                    SF.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopRight:
                    SF.Alignment = StringAlignment.Far;
                    SF.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.MiddleLeft:
                    SF.Alignment = StringAlignment.Near;
                    SF.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.MiddleCenter:
                    SF.Alignment = StringAlignment.Center;
                    SF.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.MiddleRight:
                    SF.Alignment = StringAlignment.Far;
                    SF.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.BottomLeft:
                    SF.Alignment = StringAlignment.Near;
                    SF.LineAlignment = StringAlignment.Far;
                    break;

                case ContentAlignment.BottomCenter:
                    SF.Alignment = StringAlignment.Center;
                    SF.LineAlignment = StringAlignment.Far;
                    break;

                case ContentAlignment.BottomRight:
                    SF.Alignment = StringAlignment.Far;
                    SF.LineAlignment = StringAlignment.Far;
                    break;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------

        private StringFormat SF = new StringFormat();

        private bool MouseEntered = false;
        private bool MousePressed = false;


        private bool roundingEnable = false;
        [Description("Вкл/Выкл закругления объекта")]
        public bool RoundingEnable
        {
            get => roundingEnable;
            set
            {
                roundingEnable = value;
                Refresh();
            }
        }

        private int roundingPercent = 100;
        [DisplayName("Rounding [%]")]
        [DefaultValue(100)]
        [Description("Указывает радиус закругления объекта в процентном соотношении")]
        public int RoundingPercent
        {
            get => roundingPercent;
            set
            {
                if (value >=0 && value <= 100)
                {
                    roundingPercent = value;
                    Refresh();
                }
            }
        }

        public ButtonRounded()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
             ControlStyles.OptimizedDoubleBuffer |
             ControlStyles.ResizeRedraw |
             ControlStyles.SupportsTransparentBackColor |
             ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            this.Size = new Size(150, 30);

            BackColor = Color.Tomato;
            ForeColor = Color.White; // Установите цвет текста по умолчанию
            Text = "Кнопка"; // Текст по умолчанию

            // Инициализация StringFormat
            SF = new StringFormat();
            UpdateStringFormat(); // Настройка начального выравнивания
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            // Настройка качества рендеринга
            graph.SmoothingMode = SmoothingMode.AntiAlias;
            graph.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.CompositingQuality = CompositingQuality.HighQuality;

            // Очищаем область рисования
            graph.Clear(Parent.BackColor);

            // Создаем путь с закругленными углами
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            float roundingValue = Math.Min(Width, Height) / 100F * roundingPercent; // Пример: 50% закругления
            GraphicsPath roundedPath = null;
            if (RoundingEnable)
            {
                roundedPath = Drawer.RoundedRectengle(rect, roundingValue);
            }
            else
            {
                // Если закругление отключено, используем прямоугольник без закругления
                roundedPath = new GraphicsPath();
                roundedPath.AddRectangle(rect);
            }

            // Рисуем фоновое изображение или цвет фона
            if (_backgroundImage != null)
            {
                // Вычисляем соотношение сторон для масштабирования
                float ratioX = (float)rect.Width / _backgroundImage.Width;
                float ratioY = (float)rect.Height / _backgroundImage.Height;
                float ratio = Math.Max(ratioX, ratioY); // Сохраняем пропорции
                int newWidth = (int)(_backgroundImage.Width * ratio);
                int newHeight = (int)(_backgroundImage.Height * ratio);

                // Центрируем изображение
                int posX = (Width - newWidth) / 2;
                int posY = (Height - newHeight) / 2;

                // Создаем временное изображение для масштабирования
                using (Bitmap scaledImage = new Bitmap(newWidth, newHeight))
                {
                    using (Graphics tempGraph = Graphics.FromImage(scaledImage))
                    {
                        tempGraph.SmoothingMode = SmoothingMode.AntiAlias;
                        tempGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        tempGraph.DrawImage(_backgroundImage, new Rectangle(0, 0, newWidth, newHeight));
                    }

                    // Используем TextureBrush для рисования изображения внутри закругленного прямоугольника
                    using (TextureBrush textureBrush = new TextureBrush(scaledImage))
                    {
                        textureBrush.TranslateTransform(posX, posY);
                        graph.FillPath(textureBrush, roundedPath);
                    }
                }
            }
            else
            {
                // Если изображение не установлено, используем цвет фона
                graph.FillPath(new SolidBrush(BackColor), roundedPath);
            }

            // Добавляем полупрозрачную границу
            using (Pen borderPen = new Pen(Color.FromArgb(30, Color.Black), 1))
            {
                graph.DrawPath(borderPen, roundedPath);
            }

            // Эффекты при наведении и нажатии
            if (MouseEntered)
            {
                graph.DrawPath(new Pen(Color.FromArgb(60, Color.White)), roundedPath);
                graph.FillPath(new SolidBrush(Color.FromArgb(60, Color.White)), roundedPath);
            }
            if (MousePressed)
            {
                graph.DrawPath(new Pen(Color.FromArgb(30, Color.Black)), roundedPath);
                graph.FillPath(new SolidBrush(Color.FromArgb(30, Color.Black)), roundedPath);
            }

            // Рисуем текст
            //graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);
            if (!string.IsNullOrEmpty(Text))
            {
                using (SolidBrush brush = new SolidBrush(ForeColor))
                {
                    graph.DrawString(Text, Font, brush, rect, SF);
                }
            }
        }

        private string _text = "Кнопка"; // Поле для хранения текста

        public override string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    Invalidate(); // Перерисовываем элемент при изменении текста
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;


            //ButtonCurtainAction();

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            //ButtonCurtainAction();

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = false;
            Invalidate();
        }
    }
}
