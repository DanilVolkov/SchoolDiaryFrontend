namespace SchoolDiary
{
    partial class Uspevaemost
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.customGroupBox1 = new SchoolDiary.CustomGroupBox();
            this.customGroupBox2 = new SchoolDiary.CustomGroupBox();
            this.customGroupBox3 = new SchoolDiary.CustomGroupBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // customGroupBox1
            // 
            this.customGroupBox1.Location = new System.Drawing.Point(4, 4);
            this.customGroupBox1.Name = "customGroupBox1";
            this.customGroupBox1.Size = new System.Drawing.Size(1286, 100);
            this.customGroupBox1.TabIndex = 0;
            this.customGroupBox1.TabStop = false;
            this.customGroupBox1.Text = "customGroupBox1";
            // 
            // customGroupBox2
            // 
            this.customGroupBox2.Location = new System.Drawing.Point(4, 111);
            this.customGroupBox2.Name = "customGroupBox2";
            this.customGroupBox2.Size = new System.Drawing.Size(1286, 100);
            this.customGroupBox2.TabIndex = 1;
            this.customGroupBox2.TabStop = false;
            this.customGroupBox2.Text = "customGroupBox2";
            // 
            // customGroupBox3
            // 
            this.customGroupBox3.Location = new System.Drawing.Point(4, 218);
            this.customGroupBox3.Name = "customGroupBox3";
            this.customGroupBox3.Size = new System.Drawing.Size(200, 859);
            this.customGroupBox3.TabIndex = 2;
            this.customGroupBox3.TabStop = false;
            this.customGroupBox3.Text = "customGroupBox3";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(1861, 218);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(59, 859);
            this.vScrollBar1.TabIndex = 3;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll_1);
            // 
            // Uspevaemost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.customGroupBox3);
            this.Controls.Add(this.customGroupBox2);
            this.Controls.Add(this.customGroupBox1);
            this.Name = "Uspevaemost";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.Load += new System.EventHandler(this.Uspevaemost_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomGroupBox customGroupBox1;
        private CustomGroupBox customGroupBox2;
        private CustomGroupBox customGroupBox3;
        private System.Windows.Forms.VScrollBar vScrollBar1;
    }
}
