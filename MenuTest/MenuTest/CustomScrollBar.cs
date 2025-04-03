using System;
using System.Drawing;
using System.Windows.Forms;

namespace MenuTest
{
    public class CustomVerticalScrollBar : Control
    {
        private int _value;
        private int _minimum = 0;
        private int _maximum = 100;
        private int _thumbSize = 50;
        private bool _isThumbDragging;
        private Point _dragStartPosition;

        public event EventHandler ValueChanged;

        public int Minimum
        {
            get => _minimum;
            set { _minimum = value; Invalidate(); }
        }

        public int Maximum
        {
            get => _maximum;
            set { _maximum = value; Invalidate(); }
        }

        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Max(_minimum, Math.Min(value, _maximum));
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public CustomVerticalScrollBar()
        {
            DoubleBuffered = true;
            Width = 20; // Ширина скроллбара
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Рисуем фон
            e.Graphics.FillRectangle(Brushes.LightCyan, ClientRectangle);

            // Рассчитываем позицию и размер бегунка
            int trackHeight = Height - 2;
            float thumbPosition = ((float)(Value - Minimum) / (Maximum - Minimum)) * trackHeight;
            int thumbHeight = Math.Max(20, _thumbSize); // Минимальная высота бегунка

            // Рисуем бегунок
            Rectangle thumbRect = new Rectangle(1, (int)thumbPosition + 1, Width - 2, thumbHeight);
            e.Graphics.FillRectangle(Brushes.Green, thumbRect);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                // Проверяем, кликнули ли на бегунок
                Rectangle thumbRect = GetThumbRect();
                if (thumbRect.Contains(e.Location))
                {
                    _isThumbDragging = true;
                    _dragStartPosition = new Point(e.X, e.Y);
                }
                else
                {
                    // Прыжок к позиции клика
                    int newValue = (int)((e.Y / (float)Height) * (Maximum - Minimum)) + Minimum;
                    Value = newValue;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isThumbDragging)
            {
                // Вычисляем смещение и обновляем значение
                int delta = e.Y - _dragStartPosition.Y;
                float scale = (Maximum - Minimum) / (float)(Height - GetThumbRect().Height);
                Value += (int)(delta * scale);
                _dragStartPosition = e.Location;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isThumbDragging = false;
        }

        private Rectangle GetThumbRect()
        {
            int trackHeight = Height - 2;
            float thumbPosition = ((float)(Value - Minimum) / (Maximum - Minimum)) * trackHeight;
            int thumbHeight = Math.Max(20, _thumbSize);
            return new Rectangle(1, (int)thumbPosition + 1, Width - 2, thumbHeight);
        }
    }
}