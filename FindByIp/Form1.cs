using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FindByIp
{
    public partial class Form1 : Form
    {
        const string PATH_TO_REGISTRY_FOLDER = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                     REGULAR_EXPRESSION = "\"ip\":\"(.*?)\",(.*?)\"continent\":\"(.*?)\",(.*?)\"country\":\"(.*?)\",(.*?)\"country_phone\":\"(.*?)\",(.*?)\"region\":\"(.*?)\",\"city\":\"(.*?)\",\"latitude\":\"(.*?)\",\"longitude\":\"(.*?)\",(.*?)\"timezone_gmt\":\"(.*?)\",\"currency\":\"(.*?)\"";
        bool IsWebBrowserVisible;
        byte digitsInTheFileName;
        string[] filesInFolder;
        string maskedTextBoxValue, previousTextBoxValue, ipWithoutSpaces;
        SaveFileDialog saveFileDialog;

        public Form1()
        {
            /*До запуска формы добавлет ключ в реестр*/
            Registry.CurrentUser.OpenSubKey(PATH_TO_REGISTRY_FOLDER, true).SetValue("FindByIp.exe", "69632", RegistryValueKind.DWord);

            InitializeComponent();

            panelForInformation.Width += 400;
            webBrowser1.Width -= 400;
            IsWebBrowserVisible = false;
            maskedTextBoxValue = "";
            previousTextBoxValue = "";
            ipWithoutSpaces = "";
            webBrowser1.ScriptErrorsSuppressed = true;

            saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = $@"C:\Users\{Environment.UserName}\Desktop\",
                CheckPathExists = true,
                OverwritePrompt = true
            };
        }

        /*Плавное появление формы;
         * проверка пинга для принятия решение: имеется ли интернет или нет*/
        void TimerToSmoothlyRunForm_Tick(object sender, EventArgs e)
        {
            try
            {
                CheckingPing();
                RemovingTransparency();
                maskedTextBox1.Focus();
            }

            catch (Exception)
            {
                RemovingTransparency();
                button1.Text = "Проверить подключение";
                panelForInformation.BackColor = Color.IndianRed;
                maskedTextBox1.Enabled = false;
                label1.Visible = button2.Visible = true;
                button2.Focus();
            }          
        }

        /*Переход от прозрачности к непрозрачности*/
        void RemovingTransparency()
        {
            if (Opacity == 1)
                timerToSmoothlyRunForm.Stop();
            else
                Opacity += .04;
        }

        /*Пингует до серверов яндекса*/
        void CheckingPing()
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(@"ya.ru");
        }

        /*Действия при отключении от интернета*/
        void TheInternetHasFallen()
        {
            button1.Text = "Проверить подключение";
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
            button2.Focus();
        }

        /*Отлавливает клавишу Enter; 
         * убирает linkLabel о проверке IPv4-адреса; 
         * копирует в переменную текст из textBox*/
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

        /*копирует ip-адрес из maskedTextBox в переменную с заменой пробелов и пропусков;
         * проверка пинга для принятия решение: имеется ли интернет или нет;
         * подготавливает отображение google maps в откне webBrowser;        
         * уменьшает время повторной подгрузки карты;
         * меняет цвет фона, если имеются неточности в ip-адресе или же отсутствует подключение к интенету*/
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
                        CheckingPing();

                        if (panelForInformation.BackColor != SystemColors.GradientActiveCaption)
                        {
                            linkLabel1.Visible = false;
                            panelForInformation.BackColor = SystemColors.GradientActiveCaption;
                        }

                        webBrowser1.Visible = true;

                        using (WebClient wc = new WebClient())
                        {
                            Match match = Regex.Match(wc.DownloadString($"http://free.ipwhois.io/json/{ipWithoutSpaces}"), REGULAR_EXPRESSION);

                            textBox1.Text = $"\r\n\r\n\r\nIP-адрес: {match.Groups[1].Value}\r\nКонтинент: {match.Groups[3].Value}\r\nСтрана: {match.Groups[5].Value}\r\n" +
                               $"Телефонный код страны: {match.Groups[7].Value}\r\nРегион: {match.Groups[9].Value}\r\nГород: {match.Groups[10].Value}\r\nШирота: {match.Groups[11].Value}" +
                               $"\r\nДолгота: {match.Groups[12].Value}\r\nЧасовой пояс: {match.Groups[14].Value}\r\nВалюта: {match.Groups[15].Value}";

                            webBrowser1.Url = new Uri($"https://www.google.com/maps/@?api=1&map_action=map&center={match.Groups[11].Value},{match.Groups[12].Value}&zoom=13", UriKind.Absolute);

                            saveFileDialog.FileName = $"{match.Groups[10].Value}, {match.Groups[11].Value}, {match.Groups[12].Value}";
                            timerForSlidingPanelInformation.Start();
                        }
                    }

                    catch (Exception)
                    {
                        TheInternetHasFallen();
                    }
                }

                else if (maskedTextBoxValue.Contains(ipWithoutSpaces) && !label1.Visible)
                {
                    try
                    {
                        CheckingPing();

                        if (!webBrowser1.Visible)
                        {
                            textBox1.Text = previousTextBoxValue;
                            webBrowser1.Visible = true;
                        }

                        timerForSlidingPanelInformation.Start();
                    }

                    catch (Exception)
                    {
                        TheInternetHasFallen();
                    }
                }

                else
                {
                    try
                    {
                        CheckingPing();
                        panelForInformation.BackColor = Color.IndianRed;
                        maskedTextBox1.Clear();
                        textBox1.Clear();
                        linkLabel1.Visible = true;
                        maskedTextBox1.Focus();
                    }

                    catch (Exception)
                    {
                        TheInternetHasFallen();
                    }
                }
            }

            else
            {
                try
                {
                    CheckingPing();

                    if (panelForInformation.BackColor != SystemColors.GradientActiveCaption)
                    {
                        button1.Text = "Развернуть карту";
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

        /*Проверяет ip-адрес на корректность ввода*/
        bool IPAddressExists(string ipForComparison) =>
            IPAddress.TryParse(ipForComparison, out IPAddress address);

        /*Скрывает и раскрывает карту; 
         * при раскрытии карты блокирует возможность ввода в maskedTextBox*/
        void TimerForSlidingPanelInformation_Tick(object sender, EventArgs e)
        {
            if (!IsWebBrowserVisible)
            {
                if (maskedTextBox1.Enabled)
                {
                    maskedTextBox1.Enabled = false;
                    button1.Text = "Скрыть карту";
                }

                panelForInformation.Width -= 10;
                webBrowser1.Width += 10;

                if (panelForInformation.Width <= 400)
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
                    maskedTextBox1.Enabled = true;
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

        /*Скрывает linkLabel после нажатия юзером по ссылке в нём; 
         * заменяем цвет фона на голубенький; 
         * очищает textBox и maskedTextBox*/
        void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Visible = false;
            panelForInformation.BackColor = SystemColors.GradientActiveCaption;
            maskedTextBox1.Clear();
            textBox1.Clear();
            Process.Start("https://ru.wikipedia.org/wiki/IPv4#%D0%9A%D0%BB%D0%B0%D1%81%D1%81%D0%BE%D0%B2%D0%B0%D1%8F_%D0%B0%D0%B4%D1%80%D0%B5%D1%81%D0%B0%D1%86%D0%B8%D1%8F");
        }

        /*Делает скриншот с формы; 
         * предлагает сохранить в форматах: jpg, png, bmp; 
         * проверяет файлы в папке для сохранения на наличие уже имеющихся с названием Screenshot №;
         * cохраняет скриншот с обрезкой тени по краям формы в выбранную папку;
         * запоминает путь сохранения для последующих сохранений по этому же пути;
         * после сохранения открывает папку для просмотра*/
        void ScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap screenshot = new Bitmap(Width - 16, Height - 9);
            Graphics graphics = Graphics.FromImage(screenshot);
            /*обрезает тень по краям формы для скриншота*/
            graphics.CopyFromScreen(Location.X + 8, Location.Y + 1, 0, 0, screenshot.Size); 

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

        /*При двойном клике по значку приложения в трей, приложение разворачивается из свернутого положения */
        void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
        }

        /*Клик по кнопке запускает окно сетевых подключений для возможности саморучно переподключиться к интернету*/
        void Button2_Click(object sender, EventArgs e) =>
            Process.Start("control.exe", "netconnections");

        /*Клик правой кнопкой мыши по значку приложения в трей предоставит меню с единственным параметром - Close*/
        void CloseToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        /*Перед закрытием формы удаляет ключ из реестра*/
        void Form1_FormClosing(object sender, FormClosingEventArgs e) =>
            Registry.CurrentUser.OpenSubKey(PATH_TO_REGISTRY_FOLDER, true).DeleteValue("FindByIp.exe");
    }
}
