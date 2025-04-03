using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuTest
{
    public partial class Form1 : Form
    {
        private CustomVerticalScrollBar customScrollBar;
        private Panel contentPanel;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomScrollBar();
            InitializeContentPanel();
        }

        private void InitializeCustomScrollBar()
        {
            customScrollBar = new CustomVerticalScrollBar
            {
                Dock = DockStyle.Right,
                Maximum = 1000 // Пример максимального значения
            };
            customScrollBar.ValueChanged += (s, e) => UpdatePanelPosition();
            Controls.Add(customScrollBar);
        }

        private void InitializeContentPanel()
        {
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // Добавляем длинный контент (пример)
            Label longContent = new Label
            {
                Text = "Длинный контент...\n" + new string('\n', 50),
                AutoSize = true
            };
            contentPanel.Controls.Add(longContent);

            Controls.Add(contentPanel);
        }

        private void UpdatePanelPosition()
        {
            // Смещаем контент в панели на основе значения скроллбара
            if (contentPanel.Controls.Count > 0)
            {
                contentPanel.Controls[0].Top = -customScrollBar.Value;
            }
        }
    }
}
