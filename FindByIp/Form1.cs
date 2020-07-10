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
        string previousIpv4, previousInformationAboutIpAddress, ipv4WithoutSpaces;
        SaveFileDialog saveFileDialog;

        public Form1()
        {
            /*До запуска формы добавлет ключ в реестр*/
            Registry.CurrentUser.OpenSubKey(PATH_TO_REGISTRY_FOLDER, true).SetValue("FindByIp.exe", "69632", RegistryValueKind.DWord);

            InitializeComponent();

            panelForInformation.Width += 400;
            webBrowserWithMap.Width -= 400;
            IsWebBrowserVisible = false;
            previousIpv4 = "";
            previousInformationAboutIpAddress = "";
            ipv4WithoutSpaces = "";
            webBrowserWithMap.ScriptErrorsSuppressed = true;

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
                maskedTextBoxIpv4Field.Focus();
            }

            catch (Exception)
            {
                RemovingTransparency();
                buttonWithMapAndPingCheck.Text = "Проверить подключение";
                panelForInformation.BackColor = Color.IndianRed;
                maskedTextBoxIpv4Field.Enabled = false;
                labelErrorNoIntenterConnection.Visible = buttonWithNetworkConnectons.Visible = true;
                buttonWithNetworkConnectons.Focus();
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
            ping.Send(@"ya.ru");
        }

        /*Действия при отключении от интернета*/
        void TheInternetHasFallen()
        {
            buttonWithMapAndPingCheck.Text = "Проверить подключение";
            panelForInformation.BackColor = Color.IndianRed;
            maskedTextBoxIpv4Field.Enabled = false;
            maskedTextBoxIpv4Field.Clear();
            previousIpv4 = previousInformationAboutIpAddress = "";
            textBoxWithInformationAboutIpAddress.Clear();

            if (linkLabelErrorWithWiki.Visible)
                linkLabelErrorWithWiki.Visible = false;

            labelErrorNoIntenterConnection.Visible = buttonWithNetworkConnectons.Visible = true;

            while (panelForInformation.Width < 800)
            {
                panelForInformation.Width += 10;
                webBrowserWithMap.Width -= 10;
            }

            webBrowserWithMap.Visible = IsWebBrowserVisible = false;
            buttonWithNetworkConnectons.Focus();
        }

        /*Отлавливает клавишу Enter; 
         * убирает linkLabel о проверке IPv4-адреса; 
         * копирует в переменную текст из textBox*/
        void MaskedTextBoxIpField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                ButtonWithMapAndPingCheck_Click(sender, e);
            }

            else if (linkLabelErrorWithWiki.Visible)
            {
                linkLabelErrorWithWiki.Visible = false;
                panelForInformation.BackColor = SystemColors.GradientActiveCaption;
            }

            else if (textBoxWithInformationAboutIpAddress.Text != "")
            {
                previousInformationAboutIpAddress = textBoxWithInformationAboutIpAddress.Text;
                textBoxWithInformationAboutIpAddress.Text = "";
            }
        }

        /*копирует ip-адрес из maskedTextBox в переменную с заменой пробелов и пропусков;
         * проверка пинга для принятия решение: имеется ли интернет или нет;
         * подготавливает отображение google maps в откне webBrowser;        
         * уменьшает время повторной подгрузки карты;
         * меняет цвет фона, если имеются неточности в ip-адресе или же отсутствует подключение к интенету*/
        void ButtonWithMapAndPingCheck_Click(object sender, EventArgs e)
        {
            ipv4WithoutSpaces = maskedTextBoxIpv4Field.Text.Replace(" ", "");

            if (!(labelErrorNoIntenterConnection.Visible && buttonWithNetworkConnectons.Visible))
            {
                if (!previousIpv4.Contains(ipv4WithoutSpaces) && IPAddressExists(ipv4WithoutSpaces) && !labelErrorNoIntenterConnection.Visible)
                {
                    previousIpv4 = ipv4WithoutSpaces;

                    try
                    {
                        CheckingPing();

                        if (panelForInformation.BackColor != SystemColors.GradientActiveCaption)
                        {
                            linkLabelErrorWithWiki.Visible = false;
                            panelForInformation.BackColor = SystemColors.GradientActiveCaption;
                        }

                        webBrowserWithMap.Visible = true;

                        using (WebClient wc = new WebClient())
                        {
                            Match match = Regex.Match(wc.DownloadString($"http://free.ipwhois.io/json/{ipv4WithoutSpaces}"), REGULAR_EXPRESSION);

                            textBoxWithInformationAboutIpAddress.Text = $"\r\n\r\n\r\nIP-адрес: {match.Groups[1].Value}\r\nКонтинент: {match.Groups[3].Value}\r\nСтрана: {match.Groups[5].Value}\r\n" +
                               $"Телефонный код страны: {match.Groups[7].Value}\r\nРегион: {match.Groups[9].Value}\r\nГород: {match.Groups[10].Value}\r\nШирота: {match.Groups[11].Value}" +
                               $"\r\nДолгота: {match.Groups[12].Value}\r\nЧасовой пояс: {match.Groups[14].Value}\r\nВалюта: {match.Groups[15].Value}";

                            webBrowserWithMap.Url = new Uri($"https://www.google.com/maps/@?api=1&map_action=map&center={match.Groups[11].Value},{match.Groups[12].Value}&zoom=13", UriKind.Absolute);

                            saveFileDialog.FileName = $"{match.Groups[10].Value}, {match.Groups[11].Value}, {match.Groups[12].Value}";
                            timerForSlidingPanelInformation.Start();
                        }
                    }

                    catch (Exception)
                    {
                        TheInternetHasFallen();
                    }
                }

                else if (previousIpv4.Contains(ipv4WithoutSpaces) && !labelErrorNoIntenterConnection.Visible)
                {
                    try
                    {
                        CheckingPing();

                        if (!webBrowserWithMap.Visible)
                        {
                            textBoxWithInformationAboutIpAddress.Text = previousInformationAboutIpAddress;
                            webBrowserWithMap.Visible = true;
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
                        maskedTextBoxIpv4Field.Clear();
                        textBoxWithInformationAboutIpAddress.Clear();
                        linkLabelErrorWithWiki.Visible = true;
                        maskedTextBoxIpv4Field.Focus();
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
                        buttonWithMapAndPingCheck.Text = "Развернуть карту";
                        labelErrorNoIntenterConnection.Visible = buttonWithNetworkConnectons.Visible = false;
                        maskedTextBoxIpv4Field.Enabled = true;
                        panelForInformation.BackColor = SystemColors.GradientActiveCaption;
                        maskedTextBoxIpv4Field.Focus();
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
                if (maskedTextBoxIpv4Field.Enabled)
                {
                    maskedTextBoxIpv4Field.Enabled = false;
                    buttonWithMapAndPingCheck.Text = "Скрыть карту";
                }

                panelForInformation.Width -= 10;
                webBrowserWithMap.Width += 10;

                if (panelForInformation.Width <= 400)
                {
                    timerForSlidingPanelInformation.Stop();
                    IsWebBrowserVisible = true;
                }
            }

            else
            {
                if (!maskedTextBoxIpv4Field.Enabled)
                {
                    maskedTextBoxIpv4Field.Clear();
                    maskedTextBoxIpv4Field.Enabled = true;
                    buttonWithMapAndPingCheck.Text = "Развернуть карту";
                }

                panelForInformation.Width += 10;
                webBrowserWithMap.Width -= 10;

                if (panelForInformation.Width >= 800)
                {
                    webBrowserWithMap.Visible = IsWebBrowserVisible = false;
                    timerForSlidingPanelInformation.Stop();
                    maskedTextBoxIpv4Field.Focus();
                }
            }
        }

        /*Скрывает linkLabel после нажатия юзером по ссылке в нём; 
         * заменяем цвет фона на голубенький; 
         * очищает textBox и maskedTextBox*/
        void LinkLabelErrorWithWiki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabelErrorWithWiki.Visible = false;
            panelForInformation.BackColor = SystemColors.GradientActiveCaption;
            maskedTextBoxIpv4Field.Clear();
            textBoxWithInformationAboutIpAddress.Clear();
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

            if (textBoxWithInformationAboutIpAddress.Text == "")
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
        void NotifyIconInTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
        }

        /*Клик по кнопке запускает окно сетевых подключений для возможности саморучно переподключиться к интернету*/
        void ButtonWithNetworkConnections_Click(object sender, EventArgs e) =>
            Process.Start("control.exe", "netconnections");

        /*Клик правой кнопкой мыши по значку приложения в трей предоставит меню с единственным параметром - Close*/
        void CloseToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        /*Перед закрытием формы удаляет ключ из реестра*/
        void Form1_FormClosing(object sender, FormClosingEventArgs e) =>
            Registry.CurrentUser.OpenSubKey(PATH_TO_REGISTRY_FOLDER, true).DeleteValue("FindByIp.exe");
    }
}
