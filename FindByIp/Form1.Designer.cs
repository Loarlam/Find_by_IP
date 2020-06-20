namespace FindByIp
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelForInformation = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelForMaps = new System.Windows.Forms.Panel();
            this.panelForInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForInformation
            // 
            this.panelForInformation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelForInformation.Controls.Add(this.button1);
            this.panelForInformation.Controls.Add(this.maskedTextBox1);
            this.panelForInformation.Location = new System.Drawing.Point(0, 0);
            this.panelForInformation.Margin = new System.Windows.Forms.Padding(0);
            this.panelForInformation.Name = "panelForInformation";
            this.panelForInformation.Size = new System.Drawing.Size(400, 561);
            this.panelForInformation.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(142, 372);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "Узнать";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.maskedTextBox1.Location = new System.Drawing.Point(142, 152);
            this.maskedTextBox1.Mask = "000.000.000.000";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(116, 24);
            this.maskedTextBox1.TabIndex = 0;
            this.maskedTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox1.ValidatingType = typeof(int);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // panelForMaps
            // 
            this.panelForMaps.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelForMaps.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelForMaps.Location = new System.Drawing.Point(400, 0);
            this.panelForMaps.Margin = new System.Windows.Forms.Padding(0);
            this.panelForMaps.Name = "panelForMaps";
            this.panelForMaps.Size = new System.Drawing.Size(400, 561);
            this.panelForMaps.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.panelForInformation);
            this.Controls.Add(this.panelForMaps);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Информация об IP-адресе";
            this.panelForInformation.ResumeLayout(false);
            this.panelForInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelForInformation;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelForMaps;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button button1;
    }
}

