using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolDiary
{
    public class CustomGroupBox : GroupBox
    {
        // Конструктор
        public CustomGroupBox()
        {
            // Устанавливаем текст для режима дизайна
            this.Text = "CustomGroupBox";
        }

        // Свойство для управления видимостью обводки
        private bool _showBorder = false;
        public bool ShowBorder
        {
            get { return _showBorder; }
            set
            {
                _showBorder = value;
                this.Invalidate(); // Перерисовываем элемент при изменении свойства
            }
        }

        // Свойство для установки цвета обводки
        private Color _borderColor = Color.Black; // Цвет по умолчанию
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                this.Invalidate(); // Перерисовываем элемент при изменении свойства
            }
        }



        // Свойство для управления видимостью горизонтальной линии
        private bool _showHorizontalLine = false;
        public bool ShowHorizontalLine
        {
            get { return _showHorizontalLine; }
            set
            {
                _showHorizontalLine = value;
                this.Invalidate(); // Перерисовываем элемент при изменении свойства
            }
        }

        // Свойство для установки цвета горизонтальной линии
        private Color _horizontalLineColor = Color.Black; // Цвет по умолчанию
        public Color HorizontalLineColor
        {
            get { return _horizontalLineColor; }
            set
            {
                _horizontalLineColor = value;
                this.Invalidate(); // Перерисовываем элемент при изменении свойства
            }
        }

        // Свойство для управления толщиной горизонтальной линии
        private float _horizontalLineThickness = 1f; // Толщина по умолчанию
        public float HorizontalLineThickness
        {
            get { return _horizontalLineThickness; }
            set
            {
                if (value > 0) // Толщина должна быть положительной
                {
                    _horizontalLineThickness = value;
                    this.Invalidate(); // Перерисовываем элемент при изменении свойства
                }
                else
                {
                    throw new ArgumentException("Толщина линии должна быть больше 0.");
                }
            }
        }


        // Переопределяем метод OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            // Проверяем, находится ли элемент в режиме дизайна
            if (this.DesignMode)
            {
                // В режиме дизайна рисуем рамку и текст как обычно
                base.OnPaint(e);
            }
            else
            {
                // В режиме выполнения рисуем только если ShowBorder == true
                if (ShowBorder)
                {
                    using (Pen borderPen = new Pen(BorderColor))
                    {
                        // Рисуем рамку вокруг GroupBox
                        Rectangle rect = new Rectangle(0, 0, this.Width -1, this.Height -1);
                        e.Graphics.DrawRectangle(borderPen, rect);
                    }
                }

                // Рисуем горизонтальную линию, если она включена
                if (ShowHorizontalLine)
                {
                    using (Pen linePen = new Pen(HorizontalLineColor, HorizontalLineThickness))
                    {
                        // Расчёт позиции горизонтальной линии
                        int lineHeight = this.Height / 2; // Линия посередине
                        e.Graphics.DrawLine(linePen, 7, lineHeight, this.Width - 7, lineHeight);
                    }
                }
            }
        }





    }
}
