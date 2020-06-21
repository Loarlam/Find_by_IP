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
            this.timerForSlidingPanelInformation = new System.Windows.Forms.Timer(this.components);
            this.timerToSmoothlyRunForm = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelForMap = new System.Windows.Forms.Panel();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panelForInformation = new System.Windows.Forms.Panel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.screenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelForScreenshot = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.contextMenuStrip1.SuspendLayout();
            this.panelForMap.SuspendLayout();
            this.panelForInformation.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panelForScreenshot.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerForSlidingPanelInformation
            // 
            this.timerForSlidingPanelInformation.Interval = 1;
            this.timerForSlidingPanelInformation.Tick += new System.EventHandler(this.TimerForSlidingPanelInformation_Tick);
            // 
            // timerToSmoothlyRunForm
            // 
            this.timerToSmoothlyRunForm.Enabled = true;
            this.timerToSmoothlyRunForm.Interval = 1;
            this.timerToSmoothlyRunForm.Tick += new System.EventHandler(this.TimerToSmoothlyRunForm_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Информация об IP-адресе";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 26);
            // 
            // panelForMap
            // 
            this.panelForMap.BackColor = System.Drawing.SystemColors.Window;
            this.panelForMap.Controls.Add(this.webBrowser1);
            this.panelForMap.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelForMap.Location = new System.Drawing.Point(400, 0);
            this.panelForMap.Margin = new System.Windows.Forms.Padding(0);
            this.panelForMap.Name = "panelForMap";
            this.panelForMap.Size = new System.Drawing.Size(400, 561);
            this.panelForMap.TabIndex = 1;
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
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.BackColor = System.Drawing.SystemColors.Window;
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
            // panelForInformation
            // 
            this.panelForInformation.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelForInformation.ContextMenuStrip = this.contextMenuStrip2;
            this.panelForInformation.Controls.Add(this.button1);
            this.panelForInformation.Controls.Add(this.maskedTextBox1);
            this.panelForInformation.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelForInformation.Location = new System.Drawing.Point(0, 0);
            this.panelForInformation.Margin = new System.Windows.Forms.Padding(0);
            this.panelForInformation.Name = "panelForInformation";
            this.panelForInformation.Size = new System.Drawing.Size(400, 561);
            this.panelForInformation.TabIndex = 0;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.screenshotToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(133, 26);
            // 
            // screenshotToolStripMenuItem
            // 
            this.screenshotToolStripMenuItem.Name = "screenshotToolStripMenuItem";
            this.screenshotToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.screenshotToolStripMenuItem.Text = "Screenshot";
            this.screenshotToolStripMenuItem.Click += new System.EventHandler(this.ScreenshotToolStripMenuItem_Click);
            // 
            // panelForScreenshot
            // 
            this.panelForScreenshot.Controls.Add(this.panelForInformation);
            this.panelForScreenshot.Controls.Add(this.panelForMap);
            this.panelForScreenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForScreenshot.Location = new System.Drawing.Point(0, 0);
            this.panelForScreenshot.Name = "panelForScreenshot";
            this.panelForScreenshot.Size = new System.Drawing.Size(800, 561);
            this.panelForScreenshot.TabIndex = 4;
            // 
            // webBrowser1
            // 
            this.webBrowser1.ContextMenuStrip = this.contextMenuStrip2;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(400, 561);
            this.webBrowser1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.panelForScreenshot);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Информация об IP-адресе";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelForMap.ResumeLayout(false);
            this.panelForInformation.ResumeLayout(false);
            this.panelForInformation.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.panelForScreenshot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerForSlidingPanelInformation;
        private System.Windows.Forms.Timer timerToSmoothlyRunForm;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Panel panelForMap;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelForInformation;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem screenshotToolStripMenuItem;
        private System.Windows.Forms.Panel panelForScreenshot;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

