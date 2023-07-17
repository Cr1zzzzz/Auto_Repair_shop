using KursovaFinal.AdminForm;
using KursovaFinal.BaseClasses;
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

namespace KursovaFinal
{
    public partial class AdminWorkersForm : Form
    {
        List<Worker> workers = new List<Worker>();
        public AdminWorkersForm()
        {
            InitializeComponent();
        }

        public void TableUpdate()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < workers.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = workers[i].ReturnID();
                dataGridView1.Rows[i].Cells[1].Value = workers[i].ReturnName().ToString();
                dataGridView1.Rows[i].Cells[2].Value = workers[i].ReturnPassword().ToString();
                dataGridView1.Rows[i].Cells[3].Value = workers[i].ReturnNumberPhone().ToString();
                dataGridView1.Rows[i].Cells[4].Value = workers[i].ReturnExperience();
            }
        }

        public void TextboxClear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextboxClear();
            comboBox1.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "")
            {
                if (int.TryParse(textBox4.Text.ToString(), out int number))
                {
                    try
                    {
                        int i = dataGridView1.CurrentRow.Index;
                        workers[i].SetName(textBox1.Text.ToString());
                        workers[i].SetPassword(textBox2.Text.ToString());
                        workers[i].SetNumberPhone(textBox3.Text.ToString());
                        workers[i].SetExperience(Int32.Parse(textBox4.Text));
                        File.Delete("Workers.xml");
                        XML_Save_Loader.SaveWorker("Workers.xml", workers);
                        TableUpdate();
                        TextboxClear();
                    }
                    catch
                    {
                        MessageBox.Show("Empty line editing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else MessageBox.Show("Wrong \"Experience\" format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else MessageBox.Show("Empty lines editing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "")
            {
                if (int.TryParse(textBox4.Text.ToString(), out int number))
            {
                int id = FindTheHighestID.ReturnIDofWorker(workers);
                Worker worker = new Worker(textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), id + 1, Int32.Parse(textBox4.Text));
                workers.Add(worker);
                
                File.Delete("Workers.xml");
                XML_Save_Loader.SaveWorker("Workers.xml", workers);
                TableUpdate();
                TextboxClear();
            }
                else MessageBox.Show("Wrong \"Experience\" format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else MessageBox.Show("Empty lines adding", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Select();
                int i = dataGridView1.CurrentRow.Index;
                workers.RemoveAt(i);
                File.Delete("Workers.xml");
                XML_Save_Loader.SaveWorker("Workers.xml", workers);
                TableUpdate();
                TextboxClear();

            }
            catch
            {
                MessageBox.Show("You've choose an empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                textBox1.Text = workers[i].ReturnName();
                textBox2.Text = workers[i].ReturnPassword();
                textBox3.Text = workers[i].ReturnNumberPhone();
                textBox4.Text = workers[i].ReturnExperience().ToString();
            }
            catch
            {
                MessageBox.Show("You've choose an empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminWorkersForm_Load(object sender, EventArgs e)
        {
            try
            {
                workers = XML_Save_Loader.LoadWorker("Workers.xml");
                if (workers.Count != 0)
                {
                    TableUpdate();
                }
                else MessageBox.Show("File is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch
            {
                MessageBox.Show("Failed to load", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AdminClientsForm adminClientsForm = new AdminClientsForm();
            adminClientsForm.Show();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            AdminAdminsForm adminAdminsForm = new AdminAdminsForm();
            adminAdminsForm.Show();
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            AdminServicesPricesForm adminServicesPricesForm = new AdminServicesPricesForm();
            adminServicesPricesForm.Show();
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            AdminServiceFinalChecksForm adminServiceFinalChecks = new AdminServiceFinalChecksForm();
            adminServiceFinalChecks.Show();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) To delete a worker, select the worker from the table and click the delete button\n2) To edit the information about the worker, select the required worker, display the information about him in the corresponding fields, change and correct the information about the worker, and click the edit button\n3) To add a worker, clear the field, enter the necessary information about the worker in the field, and click the add worker button\n4) To delete a worker, click on the required worker in the table and click the delete button", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                int k = comboBox1.SelectedIndex;
                switch (k)
                {
                    case 0:
                        {
                            bool isFounded = false;
                            int counter = 0;
                            int rowsCounter = 0;
                            dataGridView1.Rows.Clear();
                            foreach (var worker in workers)
                            {
                                if (worker.ReturnID().ToString() == textBox5.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = workers[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = workers[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = workers[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = workers[counter].ReturnNumberPhone().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[4].Value = workers[counter].ReturnExperience();
                                    rowsCounter++;
                                    break;
                                }
                                counter++;
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox5.Text = "";
                                TableUpdate();
                            }
                        }
                        break;
                    case 1:
                        {
                            bool isFounded = false;
                            int counter = 0;
                            int rowsCounter = 0;
                            dataGridView1.Rows.Clear();
                            foreach (var worker in workers)
                            {
                                if (worker.ReturnName().ToString() == textBox5.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = workers[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = workers[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = workers[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = workers[counter].ReturnNumberPhone().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[4].Value = workers[counter].ReturnExperience();
                                    rowsCounter++;
                                    //Немає Break бо ПІБ може повторюватись
                                }
                                counter++;
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong name/surname", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox5.Text = "";
                                TableUpdate();
                            }
                        }
                        break;
                    case 2:
                        {
                            bool isFounded = false;
                            int counter = 0;
                            int rowsCounter = 0;
                            dataGridView1.Rows.Clear();
                            foreach (var worker in workers)
                            {
                                if (worker.ReturnNumberPhone().ToString() == textBox5.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = workers[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = workers[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = workers[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = workers[counter].ReturnNumberPhone().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[4].Value = workers[counter].ReturnExperience();
                                    rowsCounter++;
                                    break;
                                }
                                counter++;
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong phone", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            foreach (var worker in workers)
                            {
                                if (worker.ReturnExperience().ToString() == textBox5.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = workers[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = workers[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = workers[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = workers[counter].ReturnNumberPhone().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[4].Value = workers[counter].ReturnExperience();
                                    rowsCounter++;
                                    break;
                                }
                                counter++;
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong experience", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox5.Text = "";
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TableUpdate();
            TextboxClear();
            comboBox1.SelectedIndex = -1;
        }
    }
}
