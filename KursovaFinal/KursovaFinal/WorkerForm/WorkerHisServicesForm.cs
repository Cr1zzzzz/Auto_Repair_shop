using KursovaFinal.AdminForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovaFinal.WorkerForm
{
    public partial class WorkerHisServicesForm : Form
    {
        public Worker worker;        
        List<ServiceFinalCheck> finalChecks = new List<ServiceFinalCheck>();
        List<ServiceFinalCheck> workerChecks = new List<ServiceFinalCheck>();
        public WorkerHisServicesForm()
        {
            InitializeComponent();
        }

        public void TableUpdate()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < workerChecks.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = workerChecks[i].ReturnID();
                dataGridView1.Rows[i].Cells[1].Value = workerChecks[i].client.ReturnID();
                dataGridView1.Rows[i].Cells[2].Value = workerChecks[i].ReturnDate().ToShortDateString();
                dataGridView1.Rows[i].Cells[3].Value = workerChecks[i].ReturnFinalPrice();
                if (workerChecks[i].ReturnIsEnded())
                {
                    dataGridView1.Rows[i].Cells[4].Value = "Done";
                } 
                else dataGridView1.Rows[i].Cells[4].Value = "In process";
            }
            dataGridView1.Rows[workerChecks.Count].Selected = true;
        }
     

        private void WorkerHisServicesForm_Load(object sender, EventArgs e)
        {
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
                TableUpdate();
            }
            catch
            {
                MessageBox.Show("Failed to load workers receipt list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBox5.Visible = true;
            textBox5.Enabled = true;
            dateTimePicker1.Visible = false;
            dateTimePicker1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int counter = 0;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < workerChecks.Count; i++)
            {
                if (workerChecks[i].ReturnIsEnded())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[counter].Cells[0].Value = workerChecks[i].ReturnID();
                    dataGridView1.Rows[counter].Cells[1].Value = workerChecks[i].client.ReturnID();
                    dataGridView1.Rows[counter].Cells[2].Value = workerChecks[i].ReturnDate().ToShortDateString();
                    dataGridView1.Rows[counter].Cells[3].Value = workerChecks[i].ReturnFinalPrice();
                    dataGridView1.Rows[counter].Cells[4].Value = "Done";
                    counter++;
                }                                             
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int counter = 0;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < workerChecks.Count; i++)
            {
                if (!workerChecks[i].ReturnIsEnded())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[counter].Cells[0].Value = workerChecks[i].ReturnID();
                    dataGridView1.Rows[counter].Cells[1].Value = workerChecks[i].client.ReturnID();
                    dataGridView1.Rows[counter].Cells[2].Value = workerChecks[i].ReturnDate().ToShortDateString();
                    dataGridView1.Rows[counter].Cells[3].Value = workerChecks[i].ReturnFinalPrice();
                    dataGridView1.Rows[counter].Cells[4].Value = "In process";
                    counter++;                
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            WorkerClientsForm workerClientsForm = new WorkerClientsForm();
            workerClientsForm.worker = worker;
            workerClientsForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int tempI = dataGridView1.CurrentRow.Index;
                if (dataGridView1.Rows[tempI].Cells[4].Value != "Done")
                {
                    int i = 0;
                    for (int q = 0; q < workerChecks.Count; q++)
                    {
                        if (workerChecks[q].ID == Int32.Parse(dataGridView1.Rows[tempI].Cells[0].Value.ToString()))
                        {
                            i = q;
                            break;
                        }
                    }
                    workerChecks[i].isEnded = true;
                    dataGridView1.Rows[tempI].Cells[4].Value = "Done";
                    for (int j = 0; j < finalChecks.Count; j++)
                    {
                        if (finalChecks[j].ID == workerChecks[i].ID)
                        {
                            finalChecks[j].isEnded = true;
                            break;
                        }
                    }
                    File.Delete("ServiceFinalChecks.xml");
                    XML_Save_Loader.SaveServiceFinals("ServiceFinalChecks.xml", finalChecks);
                    File.Delete("ListOfWorkerID" + worker.ID + ".xml");
                    XML_Save_Loader.SaveServiceFinals("ListOfWorkerID" + worker.ID + ".xml", workerChecks);
                }
                else MessageBox.Show("This order is done", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { MessageBox.Show("Line is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                                          
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TableUpdate();
            textBox5.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CheckForm checkForm = new CheckForm();
            checkForm.worker = worker;
            int i = dataGridView1.CurrentRow.Index;
            foreach (var m in workerChecks)
            {
                if (m.ReturnID().ToString() == dataGridView1.Rows[i].Cells[0].Value.ToString())
                {
                    checkForm.serviceFinalCheck = m;
                    break;
                }
            }
            checkForm.Checker = 1;
            checkForm.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            TableUpdate();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void button6_Click(object sender, EventArgs e)
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
                                    if (m.ReturnID().ToString() == textBox5.Text.ToString() && m.worker.ReturnID() == this.worker.ReturnID())
                                    {
                                        dataGridView1.Rows.Add();
                                        dataGridView1.Rows[0].Cells[0].Value = m.ReturnID();
                                        dataGridView1.Rows[0].Cells[1].Value = m.client.ReturnID();
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
                                    MessageBox.Show("There is no any order with this ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    textBox5.Text = "";
                                    TableUpdate();
                                }
                            }
                            else MessageBox.Show("Enter ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    if (m.client.ReturnID().ToString() == textBox5.Text.ToString() && m.worker.ReturnID() == this.worker.ReturnID())
                                    {
                                        dataGridView1.Rows.Add();
                                        dataGridView1.Rows[counter].Cells[0].Value = m.ReturnID();
                                        dataGridView1.Rows[counter].Cells[1].Value = m.client.ReturnID();
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
                                    MessageBox.Show("Wrong client ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    textBox5.Text = "";
                                    TableUpdate();
                                }
                            }
                            else MessageBox.Show("Enter ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                if (finalCheck.ReturnDate().ToShortDateString() == dateTimePicker1.Value.ToShortDateString() && finalCheck.worker.ReturnID() == this.worker.ReturnID())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = finalChecks[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = finalChecks[counter].client.ReturnID();
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
                                MessageBox.Show("No any orders on this date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                        if (finalCheck.ReturnDate().Year.ToString() == textBox5.Text.ToString() && finalCheck.worker.ReturnID() == this.worker.ReturnID())
                                        {
                                            isFounded = true;
                                            dataGridView1.Rows.Add();
                                            dataGridView1.Rows[rowsCounter].Cells[0].Value = finalChecks[counter].ReturnID();
                                            dataGridView1.Rows[rowsCounter].Cells[1].Value = finalChecks[counter].client.ReturnID();
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
                                        MessageBox.Show("No any orders on this date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void WorkerHisServicesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            textBox5.Visible = true;
            textBox5.Enabled = true;
            dateTimePicker1.Visible = false;
            dateTimePicker1.Enabled = false;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) To complete an order, click on the required order, click the button complete the order\n2) To find an order by specification, enter information in the search field, and click on the corresponding search button\n3) To update the table, click on the arrow next to the search line \n4) To display completed/incomplete orders, click on the corresponding button", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
