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
    public partial class AdminServicesPricesForm : Form
    {
        List<ServiceBasePrice> serviceBasePrices = new List<ServiceBasePrice>();
        public AdminServicesPricesForm()
        {
            InitializeComponent();
        }

        public void TableUpdate()
        {
            dataGridView3.Rows.Clear();
            for (int i = 0; i < serviceBasePrices.Count; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = serviceBasePrices[i].ReturnPosluga().ToString();
                dataGridView3.Rows[i].Cells[1].Value = serviceBasePrices[i].ReturnPrice();
                
            }
        }

        public void TextboxClear()
        {
            textBox1.Text = "";
            textBox2.Text = "";           
        }
        private void AdminServicesPricesForm_Load(object sender, EventArgs e)
        {
            try
            {
                serviceBasePrices = XML_Save_Loader.LoadServicePrices("ServiceBasePrices.xml");
                if (serviceBasePrices.Count != 0)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                if (int.TryParse(textBox2.Text.ToString(), out int number))
                {
                    try
                    {
                        int i = dataGridView3.CurrentRow.Index;
                        serviceBasePrices[i].SetPosluga(textBox1.Text.ToString());
                        serviceBasePrices[i].SetPrice(Int32.Parse(textBox2.Text.ToString()));                       
                        File.Delete("ServiceBasePrices.xml");
                        XML_Save_Loader.SaveServicePrices("ServiceBasePrices.xml", serviceBasePrices);
                        TableUpdate();
                        TextboxClear();
                    }
                    catch
                    {
                        MessageBox.Show("Empty line editing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else MessageBox.Show("Wrong format in field \"Price\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else MessageBox.Show("Empty lines editing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextboxClear();
            textBox5.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                if (int.TryParse(textBox2.Text.ToString(), out int number))
                {
                    if (Int32.Parse(textBox2.Text.ToString()) > 0)
                    {
                        ServiceBasePrice serviceBasePrice = new ServiceBasePrice(textBox1.Text.ToString(), Int32.Parse(textBox2.Text.ToString()));
                        serviceBasePrices.Add(serviceBasePrice);

                        File.Delete("ServiceBasePrices.xml");
                        XML_Save_Loader.SaveServicePrices("ServiceBasePrices.xml", serviceBasePrices);
                        TableUpdate();
                        TextboxClear();
                    }
                    else
                    {
                        MessageBox.Show("Price cant be negative", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox2.Text = "";
                    }    
                }
                else MessageBox.Show("Wrong format in field \"Price\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else MessageBox.Show("Empty lines adding", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Select();
                int i = dataGridView3.CurrentRow.Index;
                serviceBasePrices.RemoveAt(i);
                File.Delete("ServiceBasePrices.xml");
                XML_Save_Loader.SaveServicePrices("ServiceBasePrices.xml", serviceBasePrices);
                TableUpdate();
                TextboxClear();

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

        private void label6_Click(object sender, EventArgs e)
        {
            AdminAdminsForm adminAdminsForm = new AdminAdminsForm();
            adminAdminsForm.Show();
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
            MessageBox.Show("1) To add a service, enter the data in the text fields and click the \"Add service\" button\n2) To edit the message, select the required service from the table by clicking on it, and change the information in the text fields, then click the button \"Edit\"\n3) To delete a service, click on the required service in the table, and click the button \"Delete Service\"", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            dataGridView3.Rows.Clear();
                            foreach (var serviceBasePrice in serviceBasePrices)
                            {
                                if (serviceBasePrice.ReturnPosluga().ToString() == textBox5.Text.ToString())
                                {
                                    isFounded = true;
                                    dataGridView3.Rows.Add();
                                    dataGridView3.Rows[rowsCounter].Cells[0].Value = serviceBasePrices[counter].ReturnPosluga().ToString();
                                    dataGridView3.Rows[rowsCounter].Cells[1].Value = serviceBasePrices[counter].ReturnPrice();                
                                    rowsCounter++;
                                    break;
                                }                      
                                counter++;
                            }
                            //Ми шукали повне речення


                            if (!isFounded)
                            {

                                bool tempIsFounded = false;
                                rowsCounter = 0;
                                counter = 0;
                                string[] searchedWords = textBox5.Text.ToString().Split(' ');
                                foreach (var serviceBasePrice in serviceBasePrices)
                                {
                                    
                                    string[] words = serviceBasePrice.ReturnPosluga().Split(' ');
                                    
                                    foreach (var word in words)
                                    {
                                        foreach (var sWord in searchedWords)
                                        {
                                            if (word == sWord)
                                            {
                                                isFounded=true;
                                                tempIsFounded = true;
                                                dataGridView3.Rows.Add();
                                                dataGridView3.Rows[rowsCounter].Cells[0].Value = serviceBasePrices[counter].ReturnPosluga().ToString();
                                                dataGridView3.Rows[rowsCounter].Cells[1].Value = serviceBasePrices[counter].ReturnPrice();
                                                rowsCounter++;
                                                break;
                                            }
                                        }
                                        if (tempIsFounded)
                                        {
                                            tempIsFounded = false;
                                            break;                                  
                                        }
                                       
                                    }
                                    counter++;
                                }

                                if (!isFounded)
                                { 
                                MessageBox.Show("Wrong service name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox5.Text = "";
                                    TableUpdate();
                                }
                            }
                        }
                        break;
                    case 1:
                        {
                            bool isFounded = false;
                            int counter = 0;
                            int rowsCounter = 0;
                            int textBoxInput = 0;
                            
                            Int32.TryParse(textBox5.Text.ToString(), out textBoxInput);
                            if (textBoxInput != 0)
                            {
                                if (textBoxInput > 0)
                                {
                                    dataGridView3.Rows.Clear();
                                    foreach (var serviceBasePrice in serviceBasePrices)
                                    {
                                        if (Math.Abs(serviceBasePrice.ReturnPrice() - textBoxInput) <= 50 || serviceBasePrice.ReturnPrice() == textBoxInput)
                                        {
                                            isFounded = true;
                                            dataGridView3.Rows.Add();
                                            dataGridView3.Rows[rowsCounter].Cells[0].Value = serviceBasePrices[counter].ReturnPosluga().ToString();
                                            dataGridView3.Rows[rowsCounter].Cells[1].Value = serviceBasePrices[counter].ReturnPrice();
                                            rowsCounter++;
                                            
                                        }
                                        counter++;
                                    }
                                    if (!isFounded)
                                    {
                                        TableUpdate();
                                        MessageBox.Show("Wrong service price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        textBox5.Text = "";
                                        TableUpdate();
                                    }
                                }
                                else 
                                {
                                    MessageBox.Show("Negative number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    textBox5.Clear();
                                }
                                
                            }
                            else 
                            {
                                MessageBox.Show("Wrong formate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox5.Clear();
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
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
        }
    }
}
