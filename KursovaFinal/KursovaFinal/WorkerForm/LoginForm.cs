using KursovaFinal.AdminForm;
using KursovaFinal.ClientForm;
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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
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
                            List<Admin> Admins = XML_Save_Loader.LoadAdmin("Admins.xml");
                            foreach (var admin in Admins)
                            {
                                if (admin.ReturnID().ToString() == textBox1.Text.ToString() && admin.ReturnPassword().ToString() == textBox2.Text.ToString())
                                {
                                    isFounded = true;
                                    AdminWorkersForm adminWorkers = new AdminWorkersForm();
                                    adminWorkers.Show();
                                    this.Close();
                                    break;
                                }
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong ID or password");
                                textBox1.Text = "";
                                textBox2.Text = "";
                                comboBox1.SelectedIndex = -1;
                            }
                        }
                        break;
                    case 1:
                        {
                            bool isFounded = false;
                            List<Worker> Workers = XML_Save_Loader.LoadWorker("Workers.xml");
                            foreach (var worker in Workers)
                            {
                                if (worker.ReturnID().ToString() == textBox1.Text.ToString() && worker.ReturnPassword().ToString() == textBox2.Text.ToString())
                                {
                                    isFounded = true;
                                    WorkerClientsForm workerClients = new WorkerClientsForm();
                                    workerClients.worker = worker;
                                    workerClients.Show();
                                    this.Close();
                                    break;
                                }
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong ID or password");
                                textBox1.Text = "";
                                textBox2.Text = "";
                                comboBox1.SelectedIndex = -1;
                            }
                        }
                        break;
                    case 2:
                        {
                            bool isFounded = false;
                            List<Client> Clients = XML_Save_Loader.LoadClient("Clients.xml");
                            foreach (var client in Clients)
                            {
                                if (client.ReturnID().ToString() == textBox1.Text.ToString() && client.ReturnPassword().ToString() == textBox2.Text.ToString())
                                {
                                    isFounded = true;
                                    ClientServiceForm clientService = new ClientServiceForm();
                                    clientService.client = client;
                                    clientService.Show();
                                    this.Close();
                                    break;
                                }
                            }
                            if (!isFounded)
                            {
                                MessageBox.Show("Wrong ID or password");
                                textBox1.Text = "";
                                textBox2.Text = "";
                                comboBox1.SelectedIndex = -1;
                            }

                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            }
            else MessageBox.Show("Choose an option");

        }
    }
}
