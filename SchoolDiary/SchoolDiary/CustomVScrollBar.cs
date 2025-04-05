using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolDiary
{
    public class CustomVScrollBar : Control
    {
        // Свойства для цветов
        public Color TrackColor { get; set; } = Color.Transparent;
        public Color ThumbColor { get; set; } = Colors.C_7DCCDE;
        public Color ThumbActiveColor { get; set; } = Colors.C_4EB4D0;
        public Color ArrowButtonColor { get; set; } = Colors.C_5AB0D8;
        public Color ArrowColor { get; set; } = Color.White;

        // Поля для состояния
        private Rectangle _upArrowRect;
        private Rectangle _downArrowRect;
        private Rectangle _thumbRect;
        private bool _isThumbDragging = false;
        private int _thumbPosition = 0;
        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 0;
        private int _smallChange = 1;
        private int _largeChange = 10;

        public CustomVScrollBar()
        {
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Size = new Size(20, 200); // Размер по умолчанию
        }

        // Свойства для значений
        public int Minimum
        {
            get => _minimum;
            set
            {
                if (value < 0) throw new ArgumentException("Minimum value cannot be less than 0.");
                _minimum = value;
                Invalidate();
            }
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value <= _minimum) throw new ArgumentException("Maximum value must be greater than minimum.");
                _maximum = value;
                Invalidate();
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                if (value < _minimum || value > _maximum) throw new ArgumentOutOfRangeException(nameof(value));
                _value = value;
                UpdateThumbPosition();
                OnValueChanged(EventArgs.Empty);
            }
        }

        public int SmallChange
        {
            get => _smallChange;
            set => _smallChange = Math.Max(1, value);
        }

        public int LargeChange
        {
            get => _largeChange;
            set => _largeChange = Math.Max(1, value);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.Clear(this.BackColor);

            // Включаем антиалиасинг
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Рисуем стрелки
            DrawArrow(g, _upArrowRect, ArrowDirection.Up);
            DrawArrow(g, _downArrowRect, ArrowDirection.Down);

            // Рисуем трек
            using (Brush trackBrush = new SolidBrush(TrackColor))
            {
                g.FillRectangle(trackBrush, 0, _upArrowRect.Bottom, this.Width, this.Height - _upArrowRect.Height - _downArrowRect.Height);
            }

            // Выбираем цвет ползунка в зависимости от состояния
            Color thumbColor = _isThumbDragging ? ThumbActiveColor : ThumbColor;

            // Рисуем закруглённый ползунок
            using (Brush thumbBrush = new SolidBrush(thumbColor))
            {
                int cornerRadius = Math.Min(10, _thumbRect.Height / 2); // Ограничиваем радиус закругления
                GraphicsPath roundedThumb = GetRoundedRectangle(_thumbRect, cornerRadius);
                g.FillPath(thumbBrush, roundedThumb);
            }
        }

        private void UpdateThumbPosition()
        {
            int trackHeight = this.Height - _upArrowRect.Height - _downArrowRect.Height;
            int thumbHeight = Math.Max(20, (int)(trackHeight * ((double)LargeChange / (_maximum - _minimum))));
            int maxThumbPos = trackHeight - thumbHeight;

            if (_maximum != _minimum)
            {
                _thumbPosition = (int)Math.Round(((_value - _minimum) / (double)(_maximum - _minimum)) * maxThumbPos);
            }
            else
            {
                _thumbPosition = 0;
            }

            _thumbRect = new Rectangle(
                0,
                _upArrowRect.Bottom + _thumbPosition,
                this.Width,
                thumbHeight
            );

            Invalidate();
        }









        // Рисование стрелок
        private void DrawArrow(Graphics g, Rectangle rect, ArrowDirection direction)
        {
            using (Brush buttonBrush = new SolidBrush(ArrowButtonColor))
            {
                g.FillRectangle(buttonBrush, rect);
            }

            Point[] arrowPoints = GetArrowPoints(rect, direction);
            using (Brush arrowBrush = new SolidBrush(ArrowColor))
            {
                g.FillPolygon(arrowBrush, arrowPoints);
            }
        }

        private Point[] GetArrowPoints(Rectangle rect, ArrowDirection direction)
        {
            int centerX = rect.Left + rect.Width / 2;
            int centerY = rect.Top + rect.Height / 2;

            if (direction == ArrowDirection.Up)
            {
                return new Point[]
                {
                new Point(centerX, rect.Top + rect.Height / 3),
                new Point(rect.Left + rect.Width / 3, rect.Bottom - rect.Height / 3),
                new Point(rect.Right - rect.Width / 3, rect.Bottom - rect.Height / 3)
                };
            }
            else if (direction == ArrowDirection.Down)
            {
                return new Point[]
                {
                new Point(rect.Left + rect.Width / 3, rect.Top + rect.Height / 3),
                new Point(rect.Right - rect.Width / 3, rect.Top + rect.Height / 3),
                new Point(centerX, rect.Bottom - rect.Height / 3)
                };
            }
            else
            {
                throw new ArgumentException("Invalid arrow direction.");
            }
        }

        // Вспомогательный метод для создания закруглённого прямоугольника
        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            // Верхняя левая четверть круга
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);

            // Верхняя правая четверть круга
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);

            // Нижняя правая четверть круга
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);

            // Нижняя левая четверть круга
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure(); // Закрываем путь
            return path;
        }

        // Обработка событий мыши
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_upArrowRect.Contains(e.Location))
            {
                Value = Math.Max(_minimum, _value - _smallChange);
            }
            else if (_downArrowRect.Contains(e.Location))
            {
                Value = Math.Min(_maximum, _value + _smallChange);
            }
            else if (_thumbRect.Contains(e.Location))
            {
                _isThumbDragging = true;
                Invalidate(); // Перерисовываем контроль
            }
            else
            {
                int trackHeight = this.Height - _upArrowRect.Height - _downArrowRect.Height;
                int clickPosition = e.Y - _upArrowRect.Bottom;

                if (clickPosition < _thumbPosition)
                {
                    Value = Math.Max(_minimum, _value - _largeChange);
                }
                else
                {
                    Value = Math.Min(_maximum, _value + _largeChange);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isThumbDragging)
            {
                int trackHeight = this.Height - _upArrowRect.Height - _downArrowRect.Height;
                int maxThumbPos = trackHeight - _thumbRect.Height;
                int newThumbPosition = Math.Max(0, Math.Min(maxThumbPos, e.Y - _upArrowRect.Bottom - _thumbRect.Height / 2));

                if (_maximum != _minimum)
                {
                    Value = _minimum + (int)((newThumbPosition / (double)maxThumbPos) * (_maximum - _minimum));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_isThumbDragging)
            {
                _isThumbDragging = false;
                Invalidate(); // Перерисовываем контроль
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _upArrowRect = new Rectangle(0, 0, this.Width, this.Width);
            _downArrowRect = new Rectangle(0, this.Height - this.Width, this.Width, this.Width);
            UpdateThumbPosition();
        }

        // Событие изменения значения
        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        // Вспомогательный enum для направления стрелок
        private enum ArrowDirection
        {
            Up,
            Down
        }
    }
}
