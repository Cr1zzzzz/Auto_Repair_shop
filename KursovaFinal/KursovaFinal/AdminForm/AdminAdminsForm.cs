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
using System.Xml.Serialization;


namespace KursovaFinal
{
    public partial class AdminAdminsForm : Form
    {
        List<Admin> admins = new List<Admin>();

        public AdminAdminsForm()
        {
            InitializeComponent();
        }

        public void TableUpdate()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < admins.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = admins[i].ReturnID();
                dataGridView1.Rows[i].Cells[1].Value = admins[i].ReturnName().ToString();
                dataGridView1.Rows[i].Cells[2].Value = admins[i].ReturnPassword().ToString();
                dataGridView1.Rows[i].Cells[3].Value = admins[i].ReturnNumberPhone().ToString();
                dataGridView1.Rows[i].Cells[4].Value = admins[i].ReturnExperience();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "")
            {
                if (int.TryParse(textBox4.Text.ToString(), out int number))
                {
                    int id = FindTheHighestID.ReturnIDofAdmin(admins);
                    Admin admin = new Admin(textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), id + 1, Int32.Parse(textBox4.Text));
                    admins.Add(admin);

                    File.Delete("Admins.xml");
                    XML_Save_Loader.SaveAdmin("Admins.xml", admins);
                    TableUpdate();
                    TextboxClear();
                }
                else MessageBox.Show("You entered wrong value in field \"Experience\"", "Error", MessageBoxButtons.OK ,MessageBoxIcon.Warning);
            }
            else MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void AdminAdminsForm_Load(object sender, EventArgs e)
        {
            try
            {
                admins = XML_Save_Loader.LoadAdmin("Admins.xml");
                if (admins.Count != 0)
                {
                    TableUpdate();
                } 
                else MessageBox.Show("Admins database is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch 
            {
                MessageBox.Show("Failed to load admins database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            TextboxClear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Select();
                int i = dataGridView1.CurrentRow.Index;
                admins.RemoveAt(i);
                File.Delete("Admins.xml");
                XML_Save_Loader.SaveAdmin("Admins.xml", admins);
                TableUpdate();
                TextboxClear();
                
            }
            catch
            {
                MessageBox.Show("Empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                        admins[i].SetName(textBox1.Text.ToString());
                        admins[i].SetPassword(textBox2.Text.ToString());
                        admins[i].SetNumberPhone(textBox3.Text.ToString());
                        admins[i].SetExperience(Int32.Parse(textBox4.Text));
                        File.Delete("Admins.xml");
                        XML_Save_Loader.SaveAdmin("Admins.xml", admins);
                        TableUpdate();
                        TextboxClear();
                    }
                    catch
                    {
                        MessageBox.Show("You're trying to edit empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else MessageBox.Show("You entered wrong value in field \"Experience\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
            else MessageBox.Show("You're trying to edit empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                textBox1.Text = admins[i].ReturnName();
                textBox2.Text = admins[i].ReturnPassword();
                textBox3.Text = admins[i].ReturnNumberPhone();
                textBox4.Text = admins[i].ReturnExperience().ToString();
            }
            catch
            {
                MessageBox.Show("You choosed empty line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            AdminServiceFinalChecksForm adminServiceFinalChecks = new AdminServiceFinalChecksForm();
            adminServiceFinalChecks.Show();
            this.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            AdminClientsForm adminClientsForm = new AdminClientsForm();
            adminClientsForm.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AdminWorkersForm adminWorkersForm = new AdminWorkersForm();
            adminWorkersForm.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            AdminServicesPricesForm adminServicesPricesForm = new AdminServicesPricesForm();
            adminServicesPricesForm.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To delete an admin, select an admin from the table and click the delete button\n2) To edit information about an admin, select the desired admin, the information about him will be displayed in the corresponding fields, change and correct the information about the admin, and click the edit button\n3) To add an admin , clear the fields, enter the necessary information about the admin in the fields, and click the add admin button\n4) To delete an admin, click on the required admin in the table and click the delete button", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            foreach (var admin in admins)
                            {
                                if (admin.ReturnID().ToString() == textBox5.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = admins[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = admins[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = admins[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = admins[counter].ReturnNumberPhone().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[4].Value = admins[counter].ReturnExperience();
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
                            foreach (var admin in admins)
                            {
                                if (admin.ReturnName().ToString() == textBox5.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = admins[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = admins[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = admins[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = admins[counter].ReturnNumberPhone().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[4].Value = admins[counter].ReturnExperience();
                                    rowsCounter++;
                                    //Немає Break бо ПІБ може повторюватись
                                }
                                counter++;
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong Name/Surname", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            foreach (var admin in admins)
                            {
                                if (admin.ReturnNumberPhone().ToString() == textBox5.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = admins[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = admins[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = admins[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = admins[counter].ReturnNumberPhone().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[4].Value = admins[counter].ReturnExperience();
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
                            foreach (var admin in admins)
                            {
                                if (admin.ReturnExperience().ToString() == textBox5.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[rowsCounter].Cells[0].Value = admins[counter].ReturnID();
                                    dataGridView1.Rows[rowsCounter].Cells[1].Value = admins[counter].ReturnName().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[2].Value = admins[counter].ReturnPassword().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[3].Value = admins[counter].ReturnNumberPhone().ToString();
                                    dataGridView1.Rows[rowsCounter].Cells[4].Value = admins[counter].ReturnExperience();
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

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
