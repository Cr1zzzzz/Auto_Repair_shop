using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovaFinal.WorkerForm
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            panel5.Size = new Size(0, 20);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel5.Width == 280)
            {
                timer1.Stop();
                LoginForm login = new LoginForm();
                Hide();
                login.Show();
            }

            panel5.Width++;
            if (panel5.Width < 70)
                label1.Text = "Loading .";
            else if (panel5.Width < 160)
                label1.Text = "Loading ..";
            else label1.Text = "Loading ...";
        }
    }
}
