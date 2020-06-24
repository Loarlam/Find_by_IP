using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindByIp
{
    public partial class Form1 : Form
    {
        bool visibleOrNot;
        int defaultWidthOfPanel;
        SaveFileDialog saveFileDialog;
        int counter;

        public Form1()
        {
            InitializeComponent();

            counter = 0;
            defaultWidthOfPanel = panelForInformation.Width;
            panelForInformation.Width += 400;
            webBrowser1.Width -= 400;
            visibleOrNot = false;
            saveFileDialog = new SaveFileDialog
            {

                InitialDirectory = $@"C:\Users\{Environment.UserName}\Desktop\",
                FileName = "Screenshot",
                CheckPathExists = true,
                OverwritePrompt = true,
            };

            webBrowser1.Url = new Uri("https://ya.ru", UriKind.Absolute);
        }

        /*Плавное появление формы*/
        async void TimerToSmoothlyRunForm_Tick(object sender, EventArgs e)
        {
            if (counter == 0)
            {
                await Task.Delay(150);
                counter++;
            }

            if (Opacity == 1)
                timerToSmoothlyRunForm.Stop();
            else
                Opacity += .04;

        }

        void Button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Visible = true;

            using (WebClient wc = new WebClient())
            {
                Match match = Regex.Match(wc.DownloadString($"http://free.ipwhois.io/json/{maskedTextBox1.Text}"), 
                    "\"country\":\"(.*?)\",(.*?)\"timezone_gmt\":\"(.*?)\"");
                textBox1.Text = match.Groups[1].Value + Environment.NewLine + match.Groups[3].Value;
            }

            timerForSlidingPanelInformation.Start();
        }

        void TimerForSlidingPanelInformation_Tick(object sender, EventArgs e)
        {
            if (!visibleOrNot)
            {
                maskedTextBox1.Enabled = false;
                textBox1.Enabled = false;
                button1.Text = "Скрыть карту";
                panelForInformation.Width -= 10;
                webBrowser1.Width += 10;

                if (panelForInformation.Width <= defaultWidthOfPanel)
                {
                    timerForSlidingPanelInformation.Stop();
                    visibleOrNot = true;
                }
            }

            else
            {
                maskedTextBox1.Enabled = true;
                textBox1.Enabled = true;
                button1.Text = "Развернуть карту";
                panelForInformation.Width += 10;
                webBrowser1.Width -= 10;
                if (panelForInformation.Width >= 800)
                {
                    webBrowser1.Visible = false;
                    timerForSlidingPanelInformation.Stop();
                    visibleOrNot = false;
                }
            }
        }

        void ScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap screenshot = new Bitmap(Width-16, Height-9);
            Graphics graphics = Graphics.FromImage(screenshot);
            /*обрезаем тень по краям формы*/
            graphics.CopyFromScreen(Location.X+8, Location.Y+1, 0, 0, screenshot.Size); 

            saveFileDialog.Title = "Сохранение скриншота";
            saveFileDialog.Filter = "JPEG (*.jpg; *.jpeg; *.jpe) | *.jpg; *jpeg; *.jpe|PNG (*.png) | *.png|BMP (*.bmp) | *.bmp";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                screenshot.Save(saveFileDialog.FileName);
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                Process.Start(saveFileDialog.InitialDirectory);
            }
        }

        void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
        }

        void CloseToolStripMenuItem_Click(object sender, EventArgs e) => Close();
    }
}
