using KursovaFinal.AdminForm;
using KursovaFinal.BaseClasses;
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
    public partial class WorkerServiceCreationForm : Form
    {
        List<ServiceBasePrice> servicePrices = new List<ServiceBasePrice>(); //Ціни на послуги
        List<ServiceBasePrice> check = new List<ServiceBasePrice>(); //Чек
        List<ServiceFinalCheck> finalChecks = new List<ServiceFinalCheck>(); //Оформлення чеку
        List<ServiceFinalCheck> workerChecks = new List<ServiceFinalCheck>(); //Чеки чеки робітника

        public Worker worker;
        public Client client;

        public WorkerServiceCreationForm()
        {
            InitializeComponent();
        }

        public void TableUpdate()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < servicePrices.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = servicePrices[i].ReturnPosluga().ToString();
                dataGridView1.Rows[i].Cells[1].Value = servicePrices[i].ReturnPrice();
            }
        }

        public void TableUpdate1()
        {
            dataGridView3.Rows.Clear();
            for (int i = 0; i < check.Count; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = check[i].ReturnPosluga().ToString();
                dataGridView3.Rows[i].Cells[1].Value = check[i].ReturnPrice();
            }
        }

        public void TextboxClear()
        {

            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox2.Text != "")
            {
                try
                {
                    ServiceBasePrice servprc = new ServiceBasePrice(textBox3.Text.ToString(), Int32.Parse(textBox2.Text.ToString()));
                    check.Add(servprc);
                    label9.Text = (Int32.Parse(label9.Text.ToString()) + servprc.ReturnPrice()).ToString();

                    TextboxClear();
                    TableUpdate1();
                }
                catch
                {
                    MessageBox.Show("Wrong format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = "";
                }
            }
            else MessageBox.Show("Wrong input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                check.Add(servicePrices[i]);
                label9.Text = (Int32.Parse(label9.Text.ToString()) + servicePrices[i].Price).ToString();
                TableUpdate1();
                servicePrices.RemoveAt(i);
                TableUpdate();
                //pictureBox4.Location = new Point(420, 260);
                pictureBox4.Visible = false;
            }
            catch
            {
                MessageBox.Show("Empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView3.CurrentRow.Index;
                label9.Text = (Int32.Parse(label9.Text.ToString()) - check[i].Price).ToString();
                servicePrices.Add(check[i]);
                TableUpdate();
                check.RemoveAt(i);
                TableUpdate1();

                //pictureBox4.Location = new Point(420, 260);
                pictureBox5.Visible = false;
            }
            catch
            {
                MessageBox.Show("Empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < check.Count; i++)
            {
                servicePrices.Add(check[i]);
            }
            check.Clear();
            TableUpdate();
            TableUpdate1();
            label9.Text = "0";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) To return to the previous page, click on the arrow in the upper left corner \n2) To add a service to the check, click on it, then click on the arrow that appears\n3) To remove the service from the check, click on it, then click on the basket which will appear", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WorkerServiceCreationForm_Load(object sender, EventArgs e)
        {
            pictureBox4.Location = new Point(420, 260);
            
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            try
            {
                servicePrices = XML_Save_Loader.LoadServicePrices("ServiceBasePrices.xml");
                TableUpdate();
            }
            catch 
            {
                MessageBox.Show("Failed to load pricelist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                finalChecks = XML_Save_Loader.LoadServiceFinals("ServiceFinalChecks.xml");
            }
            catch 
            {
                MessageBox.Show("Failed to load receipt list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                workerChecks = XML_Save_Loader.LoadServiceFinals("ListOfWorkerID" + worker.ID + ".xml");
            }
            catch 
            {
                MessageBox.Show("Failed to load workers receipt list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WorkerClientsForm workerClients = new WorkerClientsForm();
            workerClients.worker = worker;
            workerClients.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check.Count != 0 )
            {
                int id = FindTheHighestID.ReturnIDofCheck(finalChecks);
                ServiceFinalCheck servicefinal = new ServiceFinalCheck(worker, client, textBox1.Text.ToString(), check, DateTime.Now, id + 1, Int32.Parse(label9.Text.ToString()), false);
                finalChecks.Add(servicefinal);
                XML_Save_Loader.SaveServiceFinals("ServiceFinalChecks.xml", finalChecks);
                workerChecks.Add(servicefinal);
                XML_Save_Loader.SaveServiceFinals("ListOfWorkerID" + worker.ID + ".xml", workerChecks);
                CheckForm checkForm = new CheckForm();
                checkForm.serviceFinalCheck = servicefinal;
                checkForm.worker = worker;
                checkForm.Checker = 0;
                checkForm.Show();
                this.Close();
            }
            else MessageBox.Show("There is no services in receipt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // int i = dataGridView1.CurrentRow.Index;         
            // pictureBox4.Location = new Point(460, dataGridView1.ColumnHeadersHeight + dataGridView1.CurrentCell.Size.Height * i + 290);
            if (!(Cursor.Position.Y < 437 && Cursor.Position.Y > 412 && Cursor.Position.X > 296 && Cursor.Position.Y < 729))
            {
                pictureBox4.Location = new Point(460, Cursor.Position.Y - 130);
                pictureBox4.Visible = true;
                pictureBox5.Visible = false;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(Cursor.Position.Y < 335 && Cursor.Position.Y > 311 && Cursor.Position.X > 790 && Cursor.Position.Y < 1239))
            {
                pictureBox5.Location = new Point(460, Cursor.Position.Y - 140);
                pictureBox5.Visible = true;
                pictureBox4.Visible = false;
            }
        }
    }
}
