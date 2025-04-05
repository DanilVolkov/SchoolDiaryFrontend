using System;
using System.Collections.Generic;
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
                // В режиме выполнения ничего не рисуем (убираем рамку и текст)
                // Если нужно нарисовать что-то другое, это можно сделать здесь
            }
        }
    }
}
