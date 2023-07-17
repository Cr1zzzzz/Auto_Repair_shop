using KursovaFinal.AdminForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovaFinal.WorkerForm
{
    public partial class CheckForm : Form
    {
        public Worker worker;
        public CheckForm()
        {
            InitializeComponent();
        }

        public int Checker;

        public void TableUpdate()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < serviceFinalCheck.check.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = serviceFinalCheck.check[i].ReturnPosluga().ToString();
                dataGridView1.Rows[i].Cells[1].Value = serviceFinalCheck.check[i].ReturnPrice().ToString();
            }
        }

        public ServiceFinalCheck serviceFinalCheck;
        private void CheckForm_Load(object sender, EventArgs e)
        {
            label3.Text = serviceFinalCheck.ReturnDate().ToShortDateString();
            label4.Text = serviceFinalCheck.worker.ReturnName().ToString();
            label6.Text = serviceFinalCheck.worker.ReturnNumberPhone().ToString();
            label8.Text = serviceFinalCheck.client.ReturnName().ToString();
            label10.Text = serviceFinalCheck.ReturnID().ToString();
            richTextBox1.Text = serviceFinalCheck.ReturnDescription();
            label12.Text = serviceFinalCheck.ReturnFinalPrice().ToString();
            TableUpdate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Print(this.panel1);
        }

        private void Print(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            getprintarea(panel1);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.ShowDialog();

        }

        private Bitmap memorying;

        private void getprintarea(Panel pnl)
        {
            memorying = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(memorying, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(memorying, (pagearea.Width / 2) - (this.panel1.Width / 2), this.panel1.Location.Y);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            switch (Checker)
            {
                case 0: 
                    {
                        WorkerClientsForm workerClients = new WorkerClientsForm();
                        workerClients.worker = worker;
                        workerClients.Show();
                        this.Close();
                    } 
                    break;
                case 1:
                    {
                        this.Close();                        
                    }
                    break;
            }
            
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {

        }
    }
}
