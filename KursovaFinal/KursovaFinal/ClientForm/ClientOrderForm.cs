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
    public partial class ClientOrderForm : Form
    {
        public ClientOrderForm()
        {
            InitializeComponent();
        }

        List<ServiceBasePrice> serviceBasePrices = new List<ServiceBasePrice>();
        public Client client;
        private void ClientOrderForm_Load(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TableUpdate();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
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
                                                isFounded = true;
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
                                MessageBox.Show("Wrong format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void label1_Click(object sender, EventArgs e)
        {
            ClientServiceForm clientService = new ClientServiceForm();
            clientService.client = client;
            clientService.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
