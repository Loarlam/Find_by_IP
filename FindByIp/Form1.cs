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
        int defaultWidthOfPanel, counter;
        string maskTextBoxValue;
        SaveFileDialog saveFileDialog;

        public Form1()
        {
            InitializeComponent();

            counter = 0;
            defaultWidthOfPanel = panelForInformation.Width;
            panelForInformation.Width += 400;
            webBrowser1.Width -= 400;
            visibleOrNot = false;
            maskTextBoxValue = "";
            saveFileDialog = new SaveFileDialog
            {

                InitialDirectory = $@"C:\Users\{Environment.UserName}\Desktop\",
                FileName = "Screenshot",
                CheckPathExists = true,
                OverwritePrompt = true,
            };

            webBrowser1.ScriptErrorsSuppressed = true;            

            webBrowser1.Url = new Uri("https://www.google.com/maps/@?api=1&map_action=map", UriKind.Absolute);

            //Перевести фокус на maskTextBox при запуске формы
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
            if (maskTextBoxValue != maskedTextBox1.Text)
            {
                if (IPAdressExists(maskedTextBox1.Text))
                {
                    maskTextBoxValue = maskedTextBox1.Text;

                    if (panelForInformation.BackColor != SystemColors.GradientActiveCaption)
                    {
                        linkLabel1.Visible = false;
                        panelForInformation.BackColor = SystemColors.GradientActiveCaption;
                    }

                    webBrowser1.Visible = true;

                    using (WebClient wc = new WebClient())
                    {
                        Match match = Regex.Match(wc.DownloadString($"http://free.ipwhois.io/json/{maskedTextBox1.Text}"),
                            "\"continent\":\"(.*?)\",(.*?)\"country\":\"(.*?)\",(.*?)\"region\":\"(.*?)\",\"city\":\"(.*?)\",\"latitude\":\"(.*?)\",\"longitude\":\"(.*?)\",(.*?)\"timezone_gmt\":\"(.*?)\"");
                        //webBrowser1.Url = new Uri($"https://www.google.com/maps/search/?api=1&query={match.Groups[7].Value},{match.Groups[8].Value}", UriKind.Absolute);                        
                        webBrowser1.Url = new Uri($"https://www.google.com/maps/@?api=1&map_action=map&center={match.Groups[7].Value},{match.Groups[8].Value}", UriKind.Absolute);
                        textBox1.Text = "Континент: " + match.Groups[1].Value + "\r\n" + "Страна: " + match.Groups[3].Value + "\r\n"
                            + "Регион: " + match.Groups[5].Value + "\r\n" + "Город: " + match.Groups[6].Value + "\r\n" 
                            + "Широта: " + match.Groups[7].Value + "\r\n" + "Долгота: " + match.Groups[8].Value + "\r\n" + "Часовой пояс: " + match.Groups[10].Value;
                    }

                    timerForSlidingPanelInformation.Start();
                }

                else
                {
                    panelForInformation.BackColor = Color.IndianRed;
                    maskedTextBox1.Clear();
                    textBox1.Clear();
                    linkLabel1.Visible = true;
                }
            }

            else
            {
                if (IPAdressExists(maskedTextBox1.Text))
                    webBrowser1.Visible = true;

                timerForSlidingPanelInformation.Start();
            }
        }

        bool IPAdressExists(string ipForComparison) => IPAddress.TryParse(ipForComparison, out IPAddress address);

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
            Bitmap screenshot = new Bitmap(Width - 16, Height - 9);
            Graphics graphics = Graphics.FromImage(screenshot);
            /*обрезаем тень по краям формы*/
            graphics.CopyFromScreen(Location.X + 8, Location.Y + 1, 0, 0, screenshot.Size);

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

        void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelForInformation.BackColor = SystemColors.GradientActiveCaption;
            maskedTextBox1.Clear();
            linkLabel1.Visible = false;
            textBox1.Clear();

            Process.Start("https://ru.wikipedia.org/wiki/IPv4#%D0%9A%D0%BB%D0%B0%D1%81%D1%81%D0%BE%D0%B2%D0%B0%D1%8F_%D0%B0%D0%B4%D1%80%D0%B5%D1%81%D0%B0%D1%86%D0%B8%D1%8F");
        }

        void MaskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text))
                {
                    e.SuppressKeyPress = true;
                    Button1_Click(sender, e);
                }
            }
        }
    }
}
