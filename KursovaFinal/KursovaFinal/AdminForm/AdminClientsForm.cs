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
    public partial class AdminClientsForm : Form
    {
        List<Client> clients = new List<Client>();
        public AdminClientsForm()
        {
            InitializeComponent();
        }
        public void TableUpdate()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < clients.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = clients[i].ReturnID();
                dataGridView1.Rows[i].Cells[1].Value = clients[i].ReturnName().ToString();
                dataGridView1.Rows[i].Cells[2].Value = clients[i].ReturnPassword().ToString();
                dataGridView1.Rows[i].Cells[3].Value = clients[i].ReturnNumberPhone().ToString();
            }
        }

        public void TextboxClear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "")
            {                
                try
                {
                    int i = dataGridView1.CurrentRow.Index;
                    clients[i].SetName(textBox1.Text.ToString());
                    clients[i].SetPassword(textBox2.Text.ToString());
                    clients[i].SetNumberPhone(textBox3.Text.ToString());                    
                    File.Delete("Clients.xml");
                    XML_Save_Loader.SaveClient("Clients.xml", clients);
                    TableUpdate();
                    TextboxClear();
                }
                catch
                {
                    MessageBox.Show("Empty line editing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }               
            }
            else MessageBox.Show("Empty lines editing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextboxClear();
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "")
            {
                int id = FindTheHighestID.ReturnIDofClient(clients);
                Client client = new Client(textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), id + 1);
                clients.Add(client);

                File.Delete("Clients.xml");
                XML_Save_Loader.SaveClient("Clients.xml", clients);
                TableUpdate();
                TextboxClear();
            }
            else MessageBox.Show("Пусті рядки, на які Ви хочете додати", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Select();
                int i = dataGridView1.CurrentRow.Index;
                clients.RemoveAt(i);
                File.Delete("Clients.xml");
                XML_Save_Loader.SaveClient("Clients.xml", clients);
                TableUpdate();
                TextboxClear();

            }
            catch
            {
                MessageBox.Show("You've choose an empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminClientsForm_Load(object sender, EventArgs e)
        {
            try
            {
                clients = XML_Save_Loader.LoadClient("Clients.xml");
                if (clients.Count != 0)
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


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                textBox1.Text = clients[i].ReturnName();
                textBox2.Text = clients[i].ReturnPassword();
                textBox3.Text = clients[i].ReturnNumberPhone();               
            }
            catch
            {
                MessageBox.Show("You've choose an empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            AdminServiceFinalChecksForm adminServiceFinalChecks = new AdminServiceFinalChecksForm();
            adminServiceFinalChecks.Show();
            this.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            AdminWorkersForm adminWorkersForm = new AdminWorkersForm();
            adminWorkersForm.Show();
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) To delete a customer, select a customer from the table and click the delete button\n2) To edit the customer information, select the desired customer, its information will be displayed in the corresponding fields, change and correct the customer information, and click the edit button\n3) To add a client, clear the fields, enter the necessary information about the client in the field, and click the add client button\n4) To delete a client, click on the required client in the table and click the delete button", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            foreach (var client in clients)
                            {
                                if (client.ReturnID().ToString() == textBox4.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = clients[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = clients[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = clients[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = clients[counter].ReturnNumberPhone().ToString();
                                    rowsCounter++;
                                    break;
                                }
                                counter++;
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox4.Text = "";
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
                            foreach (var client in clients)
                            {
                                if (client.ReturnName().ToString() == textBox4.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = clients[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = clients[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = clients[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = clients[counter].ReturnNumberPhone().ToString();
                                    rowsCounter++;
                                    //Немає Break бо ПІБ може повторюватись
                                }
                                counter++;
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong name/surname", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox4.Text = "";
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
                            foreach (var client in clients)
                            {
                                if (client.ReturnNumberPhone().ToString() == textBox4.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = clients[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = clients[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = clients[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = clients[counter].ReturnNumberPhone().ToString();
                                    rowsCounter++;
                                    break;
                                }
                                counter++;
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong phone", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox4.Text = "";
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
