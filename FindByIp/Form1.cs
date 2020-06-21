using System.Windows.Forms;

namespace FindByIp
{
    public partial class Form1 : Form
    {
        bool visibleOrNot;
        int defaultWidthOfPanel;

        public Form1()
        {
            InitializeComponent();

            defaultWidthOfPanel = panelForInformation.Width;
            panelForInformation.Width += 400;
            visibleOrNot = false;
        }

        /*Плавное появление формы*/
        void TimerToSmoothlyRunForm_Tick(object sender, System.EventArgs e)
        {
            if (Opacity == 1)
                timerToSmoothlyRunForm.Stop();
            else
                Opacity += .04;
        }

        void Button1_Click(object sender, System.EventArgs e)
        {
            timerForSlidingPanelInformation.Start();
        }

        /* По дефолту panelForInformation во всю форму, а значит panelForMap с картой не видна (visibleOrNot = false)
         * От сих и первое действие - уменьшение значения ширины на 10*/
        void TimerForSlidingPanelInformation_Tick(object sender, System.EventArgs e)
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

        void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
        }

        void CloseToolStripMenuItem_Click(object sender, System.EventArgs e) => Close();
    }
}
