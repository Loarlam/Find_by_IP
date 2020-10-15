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
            this.notifyIconInTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripClose = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripScreenshotOrText = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.screenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelForScreenshot = new System.Windows.Forms.Panel();
            this.panelForInformation = new System.Windows.Forms.Panel();
            this.buttonWithNetworkConnectons = new System.Windows.Forms.Button();
            this.labelErrorNoIntenterConnection = new System.Windows.Forms.Label();
            this.linkLabelErrorWithWiki = new System.Windows.Forms.LinkLabel();
            this.textBoxWithInformationAboutIpAddress = new System.Windows.Forms.TextBox();
            this.buttonWithMapAndPingCheck = new System.Windows.Forms.Button();
            this.maskedTextBoxIpv4Field = new System.Windows.Forms.MaskedTextBox();
            this.webBrowserWithMap = new System.Windows.Forms.WebBrowser();
            this.contextMenuStripClose.SuspendLayout();
            this.contextMenuStripScreenshotOrText.SuspendLayout();
            this.panelForScreenshot.SuspendLayout();
            this.panelForInformation.SuspendLayout();
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
            // notifyIconInTray
            // 
            this.notifyIconInTray.ContextMenuStrip = this.contextMenuStripClose;
            this.notifyIconInTray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconInTray.Icon")));
            this.notifyIconInTray.Text = "Информация об IP-адресе";
            this.notifyIconInTray.Visible = true;
            this.notifyIconInTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIconInTray_MouseDoubleClick);
            // 
            // contextMenuStripClose
            // 
            this.contextMenuStripClose.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.contextMenuStripClose.Name = "contextMenuStrip1";
            this.contextMenuStripClose.Size = new System.Drawing.Size(104, 26);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // contextMenuStripScreenshotOrText
            // 
            this.contextMenuStripScreenshotOrText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.screenshotToolStripMenuItem,
            this.saveTextToolStripMenuItem});
            this.contextMenuStripScreenshotOrText.Name = "contextMenuStrip2";
            this.contextMenuStripScreenshotOrText.Size = new System.Drawing.Size(181, 70);
            // 
            // screenshotToolStripMenuItem
            // 
            this.screenshotToolStripMenuItem.Name = "screenshotToolStripMenuItem";
            this.screenshotToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.screenshotToolStripMenuItem.Text = "Screenshot";
            this.screenshotToolStripMenuItem.Click += new System.EventHandler(this.ScreenshotToolStripMenuItem_Click);
            // 
            // saveTextToolStripMenuItem
            // 
            this.saveTextToolStripMenuItem.Name = "saveTextToolStripMenuItem";
            this.saveTextToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveTextToolStripMenuItem.Text = "Save IPv4 info";
            this.saveTextToolStripMenuItem.Visible = false;
            this.saveTextToolStripMenuItem.Click += new System.EventHandler(this.SaveTextToolStripMenuItem_Click);
            // 
            // panelForScreenshot
            // 
            this.panelForScreenshot.Controls.Add(this.panelForInformation);
            this.panelForScreenshot.Controls.Add(this.webBrowserWithMap);
            this.panelForScreenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForScreenshot.Location = new System.Drawing.Point(0, 0);
            this.panelForScreenshot.Name = "panelForScreenshot";
            this.panelForScreenshot.Size = new System.Drawing.Size(800, 561);
            this.panelForScreenshot.TabIndex = 6;
            // 
            // panelForInformation
            // 
            this.panelForInformation.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelForInformation.ContextMenuStrip = this.contextMenuStripScreenshotOrText;
            this.panelForInformation.Controls.Add(this.buttonWithNetworkConnectons);
            this.panelForInformation.Controls.Add(this.labelErrorNoIntenterConnection);
            this.panelForInformation.Controls.Add(this.linkLabelErrorWithWiki);
            this.panelForInformation.Controls.Add(this.textBoxWithInformationAboutIpAddress);
            this.panelForInformation.Controls.Add(this.buttonWithMapAndPingCheck);
            this.panelForInformation.Controls.Add(this.maskedTextBoxIpv4Field);
            this.panelForInformation.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelForInformation.Location = new System.Drawing.Point(0, 0);
            this.panelForInformation.Margin = new System.Windows.Forms.Padding(0);
            this.panelForInformation.Name = "panelForInformation";
            this.panelForInformation.Size = new System.Drawing.Size(400, 561);
            this.panelForInformation.TabIndex = 4;
            this.panelForInformation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelForInformation_MouseMove);
            // 
            // buttonWithNetworkConnectons
            // 
            this.buttonWithNetworkConnectons.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonWithNetworkConnectons.BackColor = System.Drawing.SystemColors.Window;
            this.buttonWithNetworkConnectons.ContextMenuStrip = this.contextMenuStripScreenshotOrText;
            this.buttonWithNetworkConnectons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWithNetworkConnectons.Location = new System.Drawing.Point(89, 280);
            this.buttonWithNetworkConnectons.Margin = new System.Windows.Forms.Padding(0);
            this.buttonWithNetworkConnectons.Name = "buttonWithNetworkConnectons";
            this.buttonWithNetworkConnectons.Size = new System.Drawing.Size(223, 36);
            this.buttonWithNetworkConnectons.TabIndex = 7;
            this.buttonWithNetworkConnectons.Text = "Сетевые подключения";
            this.buttonWithNetworkConnectons.UseVisualStyleBackColor = false;
            this.buttonWithNetworkConnectons.Visible = false;
            this.buttonWithNetworkConnectons.Click += new System.EventHandler(this.ButtonWithNetworkConnections_Click);
            // 
            // labelErrorNoIntenterConnection
            // 
            this.labelErrorNoIntenterConnection.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelErrorNoIntenterConnection.AutoSize = true;
            this.labelErrorNoIntenterConnection.BackColor = System.Drawing.SystemColors.Window;
            this.labelErrorNoIntenterConnection.ContextMenuStrip = this.contextMenuStripScreenshotOrText;
            this.labelErrorNoIntenterConnection.ForeColor = System.Drawing.Color.IndianRed;
            this.labelErrorNoIntenterConnection.Location = new System.Drawing.Point(60, 244);
            this.labelErrorNoIntenterConnection.Name = "labelErrorNoIntenterConnection";
            this.labelErrorNoIntenterConnection.Size = new System.Drawing.Size(280, 18);
            this.labelErrorNoIntenterConnection.TabIndex = 6;
            this.labelErrorNoIntenterConnection.Text = "Отсутствует подключение к интернету!\r\n";
            this.labelErrorNoIntenterConnection.Visible = false;
            // 
            // linkLabelErrorWithWiki
            // 
            this.linkLabelErrorWithWiki.ActiveLinkColor = System.Drawing.Color.Blue;
            this.linkLabelErrorWithWiki.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLabelErrorWithWiki.BackColor = System.Drawing.SystemColors.Window;
            this.linkLabelErrorWithWiki.ContextMenuStrip = this.contextMenuStripScreenshotOrText;
            this.linkLabelErrorWithWiki.DisabledLinkColor = System.Drawing.Color.Blue;
            this.linkLabelErrorWithWiki.ForeColor = System.Drawing.Color.IndianRed;
            this.linkLabelErrorWithWiki.LinkArea = new System.Windows.Forms.LinkArea(37, 26);
            this.linkLabelErrorWithWiki.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelErrorWithWiki.Location = new System.Drawing.Point(62, 252);
            this.linkLabelErrorWithWiki.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabelErrorWithWiki.Name = "linkLabelErrorWithWiki";
            this.linkLabelErrorWithWiki.Size = new System.Drawing.Size(276, 36);
            this.linkLabelErrorWithWiki.TabIndex = 3;
            this.linkLabelErrorWithWiki.TabStop = true;
            this.linkLabelErrorWithWiki.Text = "IP-адрес неверного формата!\r\nЧитать: IPv4 (классовая адресация)";
            this.linkLabelErrorWithWiki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelErrorWithWiki.UseCompatibleTextRendering = true;
            this.linkLabelErrorWithWiki.UseMnemonic = false;
            this.linkLabelErrorWithWiki.Visible = false;
            this.linkLabelErrorWithWiki.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabelErrorWithWiki.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelErrorWithWiki_LinkClicked);
            // 
            // textBoxWithInformationAboutIpAddress
            // 
            this.textBoxWithInformationAboutIpAddress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxWithInformationAboutIpAddress.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxWithInformationAboutIpAddress.Enabled = false;
            this.textBoxWithInformationAboutIpAddress.Location = new System.Drawing.Point(39, 111);
            this.textBoxWithInformationAboutIpAddress.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxWithInformationAboutIpAddress.Multiline = true;
            this.textBoxWithInformationAboutIpAddress.Name = "textBoxWithInformationAboutIpAddress";
            this.textBoxWithInformationAboutIpAddress.ReadOnly = true;
            this.textBoxWithInformationAboutIpAddress.ShortcutsEnabled = false;
            this.textBoxWithInformationAboutIpAddress.Size = new System.Drawing.Size(323, 320);
            this.textBoxWithInformationAboutIpAddress.TabIndex = 2;
            this.textBoxWithInformationAboutIpAddress.TabStop = false;
            this.textBoxWithInformationAboutIpAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonWithMapAndPingCheck
            // 
            this.buttonWithMapAndPingCheck.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonWithMapAndPingCheck.BackColor = System.Drawing.SystemColors.Window;
            this.buttonWithMapAndPingCheck.ContextMenuStrip = this.contextMenuStripScreenshotOrText;
            this.buttonWithMapAndPingCheck.FlatAppearance.BorderSize = 0;
            this.buttonWithMapAndPingCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWithMapAndPingCheck.Location = new System.Drawing.Point(89, 464);
            this.buttonWithMapAndPingCheck.Margin = new System.Windows.Forms.Padding(0);
            this.buttonWithMapAndPingCheck.Name = "buttonWithMapAndPingCheck";
            this.buttonWithMapAndPingCheck.Size = new System.Drawing.Size(223, 36);
            this.buttonWithMapAndPingCheck.TabIndex = 1;
            this.buttonWithMapAndPingCheck.Text = "Развернуть карту";
            this.buttonWithMapAndPingCheck.UseVisualStyleBackColor = false;
            this.buttonWithMapAndPingCheck.Click += new System.EventHandler(this.ButtonWithMapAndPingCheck_Click);
            // 
            // maskedTextBoxIpv4Field
            // 
            this.maskedTextBoxIpv4Field.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.maskedTextBoxIpv4Field.Location = new System.Drawing.Point(142, 60);
            this.maskedTextBoxIpv4Field.Margin = new System.Windows.Forms.Padding(0);
            this.maskedTextBoxIpv4Field.Mask = "999\\.999\\.999\\.999";
            this.maskedTextBoxIpv4Field.Name = "maskedTextBoxIpv4Field";
            this.maskedTextBoxIpv4Field.Size = new System.Drawing.Size(116, 24);
            this.maskedTextBoxIpv4Field.TabIndex = 0;
            this.maskedTextBoxIpv4Field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBoxIpv4Field.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaskedTextBoxIpField_KeyDown);
            // 
            // webBrowserWithMap
            // 
            this.webBrowserWithMap.ContextMenuStrip = this.contextMenuStripScreenshotOrText;
            this.webBrowserWithMap.Dock = System.Windows.Forms.DockStyle.Right;
            this.webBrowserWithMap.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserWithMap.Location = new System.Drawing.Point(400, 0);
            this.webBrowserWithMap.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowserWithMap.Name = "webBrowserWithMap";
            this.webBrowserWithMap.ScrollBarsEnabled = false;
            this.webBrowserWithMap.Size = new System.Drawing.Size(400, 561);
            this.webBrowserWithMap.TabIndex = 5;
            this.webBrowserWithMap.TabStop = false;
            this.webBrowserWithMap.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowserWithMap.Visible = false;
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.contextMenuStripClose.ResumeLayout(false);
            this.contextMenuStripScreenshotOrText.ResumeLayout(false);
            this.panelForScreenshot.ResumeLayout(false);
            this.panelForInformation.ResumeLayout(false);
            this.panelForInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerForSlidingPanelInformation;
        private System.Windows.Forms.Timer timerToSmoothlyRunForm;
        private System.Windows.Forms.NotifyIcon notifyIconInTray;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripClose;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripScreenshotOrText;
        private System.Windows.Forms.ToolStripMenuItem screenshotToolStripMenuItem;
        private System.Windows.Forms.Panel panelForScreenshot;
        private System.Windows.Forms.Panel panelForInformation;
        private System.Windows.Forms.Button buttonWithMapAndPingCheck;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxIpv4Field;
        private System.Windows.Forms.WebBrowser webBrowserWithMap;
        private System.Windows.Forms.TextBox textBoxWithInformationAboutIpAddress;
        private System.Windows.Forms.LinkLabel linkLabelErrorWithWiki;
        private System.Windows.Forms.Button buttonWithNetworkConnectons;
        private System.Windows.Forms.Label labelErrorNoIntenterConnection;
        private System.Windows.Forms.ToolStripMenuItem saveTextToolStripMenuItem;
    }
}

