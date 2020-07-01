/*
Планы:
1. Добавить перерисовку карты, если пользователь передвинул карту в сторону, только после того, как нажмет на кнопку "Раскрыть карту";
2. Добавить: если интернет есть, и fileNameToSave не равно нулю, то сохраняем с названием: Город + Широта + Долгота. Иначе, дефолтное название;
3. Продумать возможность избавления от webBrowser, и замену его на скриншот области с карты, с выводом оного в отдельную панель$
4. Подумать над counterForFileName$
5. Блокровать возможность сохранения файла, если в maskTextBox не изменился ip-адрес. Цвет текста в ContextMenuStrip заменить на красный, и сам текст поменять на "повторное сохранение невозможно";
6. Починить ошибку, которая позволяла юзеру вводить больше ограниченного числа символов в maskTextBox.
*/

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindByIp
{
    public partial class Form1 : Form
    {
        bool visibleOrNot, counterTrueOrFalse;
        byte digitsInTheFileName;
        short defaultWidthOfPanel;
        string[] filesInFolder;
        string maskTextBoxValue;
        const string PATH_TO_REGISTRY_FOLDER = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
        IPStatus status;
        SaveFileDialog saveFileDialog;

        public Form1()
        {
            Registry.CurrentUser.OpenSubKey(PATH_TO_REGISTRY_FOLDER, true).SetValue("FindByIp.exe", "69632", RegistryValueKind.DWord);

            InitializeComponent();

            counterTrueOrFalse = false;
            defaultWidthOfPanel = Convert.ToInt16(panelForInformation.Width);
            panelForInformation.Width += 400;
            webBrowser1.Width -= 400;
            visibleOrNot = false;
            maskTextBoxValue = "";
            status = IPStatus.TimedOut;
            webBrowser1.ScriptErrorsSuppressed = true;

            saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = $@"C:\Users\{Environment.UserName}\Desktop\",
                FileName = $"Screenshot №1",
                CheckPathExists = true,
                OverwritePrompt = true,
            };
        }

        async void TimerToSmoothlyRunForm_Tick(object sender, EventArgs e)
        {
            /*Плавное появление формы*/
            if (!counterTrueOrFalse)
            {
                await Task.Delay(150);
                counterTrueOrFalse = true;
            }

            if (Opacity == 1)
                timerToSmoothlyRunForm.Stop();
            else
                Opacity += .04;
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

            else
                textBox1.Text = "";
        }

        void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{maskedTextBox1.Text.Length}");

            if (maskTextBoxValue != maskedTextBox1.Text)
            {
                if (IPAdressExists(maskedTextBox1.Text))
                {
                    maskTextBoxValue = maskedTextBox1.Text;

                    try
                    {
                        Ping ping = new Ping();
                        PingReply reply = ping.Send(@"ya.ru");
                        status = reply.Status;

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

                            webBrowser1.Url = new Uri($"https://www.google.com/maps/@?api=1&map_action=map&center={match.Groups[7].Value},{match.Groups[8].Value}", UriKind.Absolute);

                            textBox1.Text = "Континент: " + match.Groups[1].Value + "\r\n" + "Страна: " + match.Groups[3].Value + "\r\n"
                                + "Регион: " + match.Groups[5].Value + "\r\n" + "Город: " + match.Groups[6].Value + "\r\n"
                                + "Широта: " + match.Groups[7].Value + "\r\n" + "Долгота: " + match.Groups[8].Value + "\r\n" + "Часовой пояс: " + match.Groups[10].Value;

                            saveFileDialog.FileName = $"{match.Groups[6].Value}, {match.Groups[7].Value}, {match.Groups[8].Value}";
                            timerForSlidingPanelInformation.Start();
                        }
                    }

                    catch (Exception)
                    {
                        if (linkLabel1.Visible)
                            linkLabel1.Visible = false;

                        label1.Visible = true;
                        button2.Visible = true;
                    }
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
                {
                    try
                    {
                        Ping ping = new Ping();
                        PingReply reply = ping.Send(@"ya.ru");
                        status = reply.Status;

                        webBrowser1.Visible = true;
                        timerForSlidingPanelInformation.Start();
                    }

                    catch (Exception)
                    {
                        if (linkLabel1.Visible)
                            linkLabel1.Visible = false;
                        label1.Visible = true;
                        button2.Visible = true;
                    }
                }
            }
        }

        bool IPAdressExists(string ipForComparison) =>
            IPAddress.TryParse(ipForComparison, out IPAddress address);

        void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelForInformation.BackColor = SystemColors.GradientActiveCaption;
            maskedTextBox1.Clear();
            linkLabel1.Visible = false;
            textBox1.Clear();

            Process.Start("https://ru.wikipedia.org/wiki/IPv4#%D0%9A%D0%BB%D0%B0%D1%81%D1%81%D0%BE%D0%B2%D0%B0%D1%8F_%D0%B0%D0%B4%D1%80%D0%B5%D1%81%D0%B0%D1%86%D0%B8%D1%8F");
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
            Bitmap screenshot = new Bitmap(Width - 16, Height - 9);
            Graphics graphics = Graphics.FromImage(screenshot);
            graphics.CopyFromScreen(Location.X + 8, Location.Y + 1, 0, 0, screenshot.Size); /*обрезаем тень по краям формы*/

            saveFileDialog.Title = "Сохранение скриншота";
            saveFileDialog.Filter = "JPEG (*.jpg; *.jpeg; *.jpe) | *.jpg; *jpeg; *.jpe|PNG (*.png) | *.png|BMP (*.bmp) | *.bmp";

            filesInFolder = Directory.GetFiles(saveFileDialog.InitialDirectory, "Screenshot №*");

            if (filesInFolder.Any())
            {
                filesInFolder[filesInFolder.Length - 1] = filesInFolder[filesInFolder.Length - 1].Substring(filesInFolder[filesInFolder.Length - 1].IndexOf('№') + 1);
                digitsInTheFileName = Convert.ToByte(filesInFolder[filesInFolder.Length - 1].Remove(filesInFolder[filesInFolder.Length - 1].IndexOf('.')));
                saveFileDialog.FileName = $"Screenshot №{++digitsInTheFileName}";
            }

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

        void Button2_Click(object sender, EventArgs e) =>
            Process.Start("control.exe", "netconnections");

        void CloseToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        void Form1_FormClosing(object sender, FormClosingEventArgs e) =>
            Registry.CurrentUser.OpenSubKey(PATH_TO_REGISTRY_FOLDER, true).DeleteValue("FindByIp.exe");
    }
}
