using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FindByIp
{
    public partial class Form1 : Form
    {
        bool visibleOrNot;
        int defaultWidthOfPanel;
        SaveFileDialog saveFileDialog;

        public Form1()
        {
            InitializeComponent();

            panelForMap.Hide();
            defaultWidthOfPanel = panelForInformation.Width;
            panelForInformation.Width += 400;
            visibleOrNot = false;
            saveFileDialog = new SaveFileDialog { InitialDirectory = $@"C:\Users\{Environment.UserName}\Desktop\" };
            saveFileDialog.FileName = "Screenshot";
        }

        /*Плавное появление формы*/
        void TimerToSmoothlyRunForm_Tick(object sender, EventArgs e)
        {
            if (Opacity == 1)
                timerToSmoothlyRunForm.Stop();
            else
                Opacity += .04;
        }

        void Button1_Click(object sender, EventArgs e)
        {
            panelForMap.Show();
            timerForSlidingPanelInformation.Start();
        }

        /* По дефолту panelForInformation во всю форму, а значит panelForMap с картой не видна (visibleOrNot = false)
         * От сих и первое действие - уменьшение значения ширины на 10*/
        void TimerForSlidingPanelInformation_Tick(object sender, EventArgs e)
        {
            if (!visibleOrNot)
            {
                panelForInformation.Width -= 10;

                if (panelForInformation.Width <= defaultWidthOfPanel)
                {
                    timerForSlidingPanelInformation.Stop();
                    visibleOrNot = true;
                }
            }

            else
            {
                panelForInformation.Width += 10;
                if (panelForInformation.Width >= 800)
                {
                    timerForSlidingPanelInformation.Stop();
                    visibleOrNot = false;
                }
            }
        }

        void ScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Сохранение скриншота";
            saveFileDialog.Filter = "JPEG (*.jpg; *.jpeg; *.jpe) | *.jpg; *jpeg; *.jpe|PNG (*.png) | *.png|BMP (*.bmp) | *.bmp";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.CheckPathExists = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap screenshot = new Bitmap(panelForScreenshot.Width, panelForScreenshot.Height);
                panelForScreenshot.DrawToBitmap(screenshot, new Rectangle(0, 0, screenshot.Width, screenshot.Height));
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
