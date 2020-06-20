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

        void Timer1_Tick(object sender, System.EventArgs e)
        {
            if (!visibleOrNot)
            {
                panelForInformation.Width -= 10;

                if (panelForInformation.Width <= defaultWidthOfPanel)
                {
                    timer1.Stop();
                    visibleOrNot = true;
                }
            }

            else
            {
                panelForInformation.Width += 10;
                if (panelForInformation.Width >= 800)
                {
                    timer1.Stop();
                    visibleOrNot = false;
                }
            }
            //if (visibleOrNot)
            //{
            //    panelForInformation.Width += 10;
            //    if (panelForInformation.Width > 0)
            //    {
            //        timer1.Stop();
            //        visibleOrNot = false;
            //    }
            //}

            //else
            //{
            //    panelForInformation.Visible = true;
            //    panelForInformation.Width -= 10;
            //    if (panelForInformation.Width >= panelForMapsDefaultWidth)
            //    {
            //        timer1.Stop();
            //        visibleOrNot = true;
            //    }
            //}
        }

        void Button1_Click(object sender, System.EventArgs e)
        {
            timer1.Start();
        }
    }
}
