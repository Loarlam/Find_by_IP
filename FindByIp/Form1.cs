/*
 Правки:
1. перевести строку ip-адреса в текстовую переменную, и уже в текстовой переменной убрать пробелы.
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
        const string PATH_TO_REGISTRY_FOLDER = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                    REGULAR_EXPRESSION = "\"ip\":\"(.*?)\",(.*?)\"continent\":\"(.*?)\",(.*?)\"country\":\"(.*?)\",(.*?)\"country_phone\":\"(.*?)\",(.*?)\"region\":\"(.*?)\",\"city\":\"(.*?)\",\"latitude\":\"(.*?)\",\"longitude\":\"(.*?)\",(.*?)\"isp\":\"(.*?)\",(.*?)\"timezone_gmt\":\"(.*?)\",\"currency\":\"(.*?)\"";
        bool IsWebBrowserVisible, counterTrueOrFalse;
        byte digitsInTheFileName;
        short defaultWidthOfPanel;
        string[] filesInFolder;
        string maskedTextBoxValue, previousTextBoxValue, ipWithoutSpaces;
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
            IsWebBrowserVisible = false;
            maskedTextBoxValue = "";
            previousTextBoxValue = "";
            ipWithoutSpaces = "";
            status = IPStatus.TimedOut;
            webBrowser1.ScriptErrorsSuppressed = true;

            saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = $@"C:\Users\{Environment.UserName}\Desktop\",
                CheckPathExists = true,
                OverwritePrompt = true
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
                e.Handled = true;
                Button1_Click(sender, e);
            }

            else if (linkLabel1.Visible)
            {
                linkLabel1.Visible = false;
                panelForInformation.BackColor = SystemColors.GradientActiveCaption;
            }

            else if (textBox1.Text != "")
            {
                previousTextBoxValue = textBox1.Text;
                textBox1.Text = "";
            }
        }

        void Button1_Click(object sender, EventArgs e)
        {
            ipWithoutSpaces = maskedTextBox1.Text.Replace(" ", "");

            if (!(label1.Visible && button2.Visible))
            {
                if (!maskedTextBoxValue.Contains(ipWithoutSpaces) && IPAddressExists(ipWithoutSpaces) && !label1.Visible)
                {
                    maskedTextBoxValue = ipWithoutSpaces;

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
                            Match match = Regex.Match(wc.DownloadString($"http://free.ipwhois.io/json/{ipWithoutSpaces}"), REGULAR_EXPRESSION);

                            webBrowser1.Url = new Uri($"https://www.google.com/maps/@?api=1&map_action=map&center={match.Groups[11].Value},{match.Groups[12].Value}&zoom=13", UriKind.Absolute);

                            textBox1.Text = $"\r\n\r\n\r\nIP-адрес: {match.Groups[1].Value}\r\nКонтинент: {match.Groups[3].Value}\r\nСтрана: {match.Groups[5].Value}\r\n" +
                                $"Телефонный код страны: {match.Groups[7].Value}\r\nРегион: {match.Groups[9].Value}\r\nГород: {match.Groups[10].Value}\r\nШирота: {match.Groups[11].Value}" +
                                $"\r\nДолгота: {match.Groups[12].Value}\r\nПровайдер: {match.Groups[14].Value}\r\nЧасовой пояс: {match.Groups[16].Value}\r\nВалюта: {match.Groups[17].Value}";

                            saveFileDialog.FileName = $"{match.Groups[10].Value}, {match.Groups[11].Value}, {match.Groups[12].Value}";
                            timerForSlidingPanelInformation.Start();
                        }
                    }

                    catch (Exception)
                    {
                        panelForInformation.BackColor = Color.IndianRed;
                        maskedTextBox1.Enabled = false;
                        maskedTextBox1.Clear();
                        maskedTextBoxValue = previousTextBoxValue = "";
                        textBox1.Clear();

                        if (linkLabel1.Visible)
                            linkLabel1.Visible = false;

                        label1.Visible = button2.Visible = true;

                        while (panelForInformation.Width < 800)
                        {
                            panelForInformation.Width += 10;
                            webBrowser1.Width -= 10;
                        }

                        webBrowser1.Visible = IsWebBrowserVisible = false;
                        button1.Text = "Развернуть карту";
                        button2.Focus();
                    }
                }

                else if (maskedTextBoxValue.Contains(ipWithoutSpaces) && !label1.Visible)
                {
                    try
                    {
                        Ping ping = new Ping();
                        PingReply reply = ping.Send(@"ya.ru");
                        status = reply.Status;

                        if (!webBrowser1.Visible)
                        {
                            textBox1.Text = previousTextBoxValue;
                            webBrowser1.Visible = true;
                        }

                        timerForSlidingPanelInformation.Start();
                    }

                    catch (Exception)
                    {
                        panelForInformation.BackColor = Color.IndianRed;
                        maskedTextBox1.Enabled = false;
                        maskedTextBox1.Clear();
                        maskedTextBoxValue = previousTextBoxValue = "";
                        textBox1.Clear();

                        if (linkLabel1.Visible)
                            linkLabel1.Visible = false;

                        label1.Visible = button2.Visible = true;

                        while (panelForInformation.Width < 800)
                        {
                            panelForInformation.Width += 10;
                            webBrowser1.Width -= 10;
                        }

                        webBrowser1.Visible = IsWebBrowserVisible = false;
                        button1.Text = "Развернуть карту";
                        button2.Focus();
                    }
                }

                else
                {
                    try
                    {
                        Ping ping = new Ping();
                        PingReply reply = ping.Send(@"ya.ru");
                        status = reply.Status;

                        panelForInformation.BackColor = Color.IndianRed;
                        maskedTextBox1.Clear();
                        textBox1.Clear();
                        linkLabel1.Visible = true;
                        maskedTextBox1.Focus();
                    }

                    catch (Exception)
                    {
                        panelForInformation.BackColor = Color.IndianRed;
                        maskedTextBox1.Enabled = false;
                        maskedTextBox1.Clear();
                        maskedTextBoxValue = previousTextBoxValue = "";
                        textBox1.Clear();

                        if (linkLabel1.Visible)
                            linkLabel1.Visible = false;

                        label1.Visible = button2.Visible = true;

                        while (panelForInformation.Width < 800)
                        {
                            panelForInformation.Width += 10;
                            webBrowser1.Width -= 10;
                        }

                        webBrowser1.Visible = IsWebBrowserVisible = false;
                        button1.Text = "Развернуть карту";
                        button2.Focus();
                    }
                }
            }

            else
            {
                try
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(@"ya.ru");
                    status = reply.Status;

                    if (panelForInformation.BackColor != SystemColors.GradientActiveCaption)
                    {
                        label1.Visible = button2.Visible = false;
                        maskedTextBox1.Enabled = true;
                        panelForInformation.BackColor = SystemColors.GradientActiveCaption;
                        maskedTextBox1.Focus();
                    }
                }

                catch (Exception)
                {
                    return;
                }
            }
        }

        bool IPAddressExists(string ipForComparison) =>
            IPAddress.TryParse(ipForComparison, out IPAddress address);

        void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Visible = false;
            panelForInformation.BackColor = SystemColors.GradientActiveCaption;
            maskedTextBox1.Clear();
            textBox1.Clear();
            Process.Start("https://ru.wikipedia.org/wiki/IPv4#%D0%9A%D0%BB%D0%B0%D1%81%D1%81%D0%BE%D0%B2%D0%B0%D1%8F_%D0%B0%D0%B4%D1%80%D0%B5%D1%81%D0%B0%D1%86%D0%B8%D1%8F");
        }

        void TimerForSlidingPanelInformation_Tick(object sender, EventArgs e)
        {
            if (!IsWebBrowserVisible)
            {
                if (maskedTextBox1.Enabled)
                {
                    maskedTextBox1.Enabled = textBox1.Enabled = false;
                    button1.Text = "Скрыть карту";
                }

                panelForInformation.Width -= 10;
                webBrowser1.Width += 10;

                if (panelForInformation.Width <= defaultWidthOfPanel)
                {
                    timerForSlidingPanelInformation.Stop();
                    IsWebBrowserVisible = true;
                }
            }

            else
            {
                if (!maskedTextBox1.Enabled)
                {
                    maskedTextBox1.Clear();
                    maskedTextBox1.Enabled = textBox1.Enabled = true;
                    button1.Text = "Развернуть карту";
                }

                panelForInformation.Width += 10;
                webBrowser1.Width -= 10;

                if (panelForInformation.Width >= 800)
                {
                    webBrowser1.Visible = IsWebBrowserVisible = false;
                    timerForSlidingPanelInformation.Stop();
                    maskedTextBox1.Focus();
                }
            }
        }

        void ScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap screenshot = new Bitmap(Width - 16, Height - 9);
            Graphics graphics = Graphics.FromImage(screenshot);
            graphics.CopyFromScreen(Location.X + 8, Location.Y + 1, 0, 0, screenshot.Size); /*обрезаем тень по краям формы для точноты отображения в скриншоте*/

            saveFileDialog.Title = "Сохранение скриншота";
            saveFileDialog.Filter = "JPEG (*.jpg; *.jpeg; *.jpe) | *.jpg; *jpeg; *.jpe|PNG (*.png) | *.png|BMP (*.bmp) | *.bmp";

            if (textBox1.Text == "")
            {
                filesInFolder = Directory.GetFiles(saveFileDialog.InitialDirectory, "Screenshot №*");

                if (filesInFolder.Any())
                {
                    filesInFolder[filesInFolder.Length - 1] = filesInFolder[filesInFolder.Length - 1].Substring(filesInFolder[filesInFolder.Length - 1].IndexOf('№') + 1);
                    digitsInTheFileName = Convert.ToByte(filesInFolder[filesInFolder.Length - 1].Remove(filesInFolder[filesInFolder.Length - 1].IndexOf('.')));
                    saveFileDialog.FileName = $"Screenshot №{++digitsInTheFileName}";
                }

                else
                    saveFileDialog.FileName = $"Screenshot №1";
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
