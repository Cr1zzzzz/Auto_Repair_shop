using KursovaFinal.WorkerForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovaFinal.ClientForm
{
    public partial class ClientServiceForm : Form
    {
        public Client client;
        List<ServiceFinalCheck> finalChecks = new List<ServiceFinalCheck>();
        

        public ClientServiceForm()
        {
            InitializeComponent();
        }

        public void TableUpdate()
        {
            int counter = 0;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < finalChecks.Count; i++)
            {
                if (finalChecks[i].client.ReturnID() == this.client.ReturnID())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[counter].Cells[0].Value = finalChecks[i].ReturnID();
                    dataGridView1.Rows[counter].Cells[1].Value = finalChecks[i].worker.ReturnID();
                    dataGridView1.Rows[counter].Cells[2].Value = finalChecks[i].ReturnDate().ToShortDateString();
                    dataGridView1.Rows[counter].Cells[3].Value = finalChecks[i].ReturnFinalPrice();
                    if (finalChecks[i].ReturnIsEnded())
                    {
                        dataGridView1.Rows[counter].Cells[4].Value = "Done";
                    }
                    else dataGridView1.Rows[counter].Cells[4].Value = "In process";
                    counter++;
                }
                
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int counter = 0;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < finalChecks.Count; i++)
            {
                if (finalChecks[i].ReturnIsEnded() && finalChecks[i].client.ReturnID() == this.client.ReturnID())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[counter].Cells[0].Value = finalChecks[i].ReturnID();
                    dataGridView1.Rows[counter].Cells[1].Value = finalChecks[i].worker.ReturnID();
                    dataGridView1.Rows[counter].Cells[2].Value = finalChecks[i].ReturnDate().ToShortDateString();
                    dataGridView1.Rows[counter].Cells[3].Value = finalChecks[i].ReturnFinalPrice();
                    dataGridView1.Rows[counter].Cells[4].Value = "Done";
                    counter++;
                }
            }
        }

        private void ClientServiceForm_Load(object sender, EventArgs e)
        {
            try
            {
                finalChecks = XML_Save_Loader.LoadServiceFinals("ServiceFinalChecks.xml");
                TableUpdate();
            }
            catch
            {
                MessageBox.Show("Failed to load receipt");
            }
            textBox5.Visible = true;
            textBox5.Enabled = true;
            dateTimePicker1.Visible = false;
            dateTimePicker1.Enabled = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TableUpdate();
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int counter = 0;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < finalChecks.Count; i++)
            {
                if (!finalChecks[i].ReturnIsEnded() && finalChecks[i].client.ReturnID() == this.client.ReturnID())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[counter].Cells[0].Value = finalChecks[i].ReturnID();
                    dataGridView1.Rows[counter].Cells[1].Value = finalChecks[i].worker.ReturnID();
                    dataGridView1.Rows[counter].Cells[2].Value = finalChecks[i].ReturnDate().ToShortDateString();
                    dataGridView1.Rows[counter].Cells[3].Value = finalChecks[i].ReturnFinalPrice();
                    dataGridView1.Rows[counter].Cells[4].Value = "In process";
                    counter++;
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CheckForm checkForm = new CheckForm();
                int i = dataGridView1.CurrentRow.Index;
                int finalCheckID = Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                foreach (var m in finalChecks)
                {
                    if (m.ReturnID() == finalCheckID)
                    {
                        checkForm.serviceFinalCheck = m;
                        break;
                    }
                }


                checkForm.worker = checkForm.serviceFinalCheck.ReturnWorker();

                checkForm.Checker = 1;
                checkForm.Show();
            }
            catch { MessageBox.Show("This line is empty"); }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            TableUpdate();
            textBox5.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                int k = comboBox1.SelectedIndex;
                switch (k)
                {
                    case 0:
                        {
                            bool isFounded = false;
                            dataGridView1.Rows.Clear();
                            if (textBox5.Text != "")
                            {
                                foreach (var m in finalChecks)
                                {
                                    if (m.ReturnID().ToString() == textBox5.Text.ToString() && m.client.ReturnID() == this.client.ReturnID())
                                    {
                                        dataGridView1.Rows.Add();
                                        dataGridView1.Rows[0].Cells[0].Value = m.ReturnID();
                                        dataGridView1.Rows[0].Cells[1].Value = m.worker.ReturnID();
                                        dataGridView1.Rows[0].Cells[2].Value = m.ReturnDate().ToShortDateString();
                                        dataGridView1.Rows[0].Cells[3].Value = m.ReturnFinalPrice();
                                        if (m.ReturnIsEnded())
                                        {
                                            dataGridView1.Rows[0].Cells[4].Value = "Done";
                                        }
                                        else dataGridView1.Rows[0].Cells[4].Value = "In process";
                                        isFounded = true;
                                        break;
                                    }

                                }
                                if (isFounded == false)
                                {
                                    MessageBox.Show("No orders with chosen ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    textBox5.Text = "";
                                    TableUpdate();
                                }
                            }
                            else MessageBox.Show("ID is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case 1:
                        {
                            bool isFounded = false;
                            dataGridView1.Rows.Clear();
                            int counter = 0;
                            if (textBox5.Text != "")
                            {
                                foreach (var m in finalChecks)
                                {
                                    if (m.worker.ReturnID().ToString() == textBox5.Text.ToString() && m.client.ReturnID() == this.client.ReturnID())
                                    {
                                        dataGridView1.Rows.Add();
                                        dataGridView1.Rows[counter].Cells[0].Value = m.ReturnID();
                                        dataGridView1.Rows[counter].Cells[1].Value = m.worker.ReturnID();
                                        dataGridView1.Rows[counter].Cells[2].Value = m.ReturnDate().ToShortDateString();
                                        dataGridView1.Rows[counter].Cells[3].Value = m.ReturnFinalPrice();
                                        if (m.ReturnIsEnded())
                                        {
                                            dataGridView1.Rows[counter].Cells[4].Value = "Done";
                                        }
                                        else dataGridView1.Rows[counter].Cells[4].Value = "In process";
                                        isFounded = true;
                                        counter++;
                                    }

                                }
                                if (isFounded == false)
                                {
                                    MessageBox.Show("Can not find worker by ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    textBox5.Text = "";
                                    TableUpdate();
                                }
                            }
                            else MessageBox.Show("ID field is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case 2:
                        {
                            bool isFounded = false;
                            int counter = 0;
                            int rowsCounter = 0;
                            dataGridView1.Rows.Clear();
                            foreach (var finalCheck in finalChecks)
                            {
                                if (finalCheck.ReturnDate().ToShortDateString() == dateTimePicker1.Value.ToShortDateString() && finalCheck.client.ReturnID() == this.client.ReturnID())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = finalChecks[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = finalChecks[counter].worker.ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = finalChecks[counter].ReturnDate().ToShortDateString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = finalChecks[counter].ReturnFinalPrice();
                                    if (finalChecks[counter].ReturnIsEnded())
                                    {
                                        dataGridView1.Rows[rowsCounter].Cells[4].Value = "Done";
                                    }
                                    else dataGridView1.Rows[rowsCounter].Cells[4].Value = "In process";

                                    rowsCounter++;

                                }
                                counter++;
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Can not find order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox5.Text = "";
                                TableUpdate();

                            }
                        }
                        break;
                    case 3:
                        {
                            bool isFounded = false;
                            int counter = 0;
                            int rowsCounter = 0;
                            dataGridView1.Rows.Clear();
                            int textBoxInput = 0;

                            Int32.TryParse(textBox5.Text.ToString(), out textBoxInput);
                            if (textBoxInput != 0)
                            {
                                if (textBoxInput > 0)
                                {
                                    foreach (var finalCheck in finalChecks)
                                    {
                                        if (finalCheck.ReturnDate().Year.ToString() == textBox5.Text.ToString() && finalCheck.client.ReturnID() == this.client.ReturnID())
                                        {
                                            isFounded = true;
                                            dataGridView1.Rows.Add();
                                            dataGridView1.Rows[rowsCounter].Cells[0].Value = finalChecks[counter].ReturnID();
                                            dataGridView1.Rows[rowsCounter].Cells[1].Value = finalChecks[counter].worker.ReturnID();
                                            dataGridView1.Rows[rowsCounter].Cells[2].Value = finalChecks[counter].ReturnDate().ToShortDateString();
                                            dataGridView1.Rows[rowsCounter].Cells[3].Value = finalChecks[counter].ReturnFinalPrice();
                                            if (finalChecks[counter].ReturnIsEnded())
                                            {
                                                dataGridView1.Rows[rowsCounter].Cells[4].Value = "Done";
                                            }
                                            else dataGridView1.Rows[rowsCounter].Cells[4].Value = "In process";

                                            rowsCounter++;

                                        }
                                        counter++;
                                    }
                                    if (!isFounded)
                                    {
                                        MessageBox.Show("Can not find order by this year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        TableUpdate();

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Negative number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    textBox5.Clear();
                                    TableUpdate();
                                }

                            }
                            else
                            {
                                MessageBox.Show("Wrong format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox5.Clear();
                                TableUpdate();
                            }

                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Choose search settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2)
            {
                textBox5.Visible = false;
                textBox5.Enabled = false;

                dateTimePicker1.Visible = true;
                dateTimePicker1.Enabled = true;

            }
            else
            {
                textBox5.Visible = true;
                textBox5.Enabled = true;

                dateTimePicker1.Visible = false;
                dateTimePicker1.Enabled = false;
            }
        }

        private void ClientServiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            textBox5.Visible = true;
            textBox5.Enabled = true;
            dateTimePicker1.Visible = false;
            dateTimePicker1.Enabled = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ClientOrderForm clientOrder = new ClientOrderForm();
            clientOrder.client = client;
            clientOrder.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
