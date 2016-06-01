using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectoESGPS
{
    public partial class UpdatePatient : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        public UpdatePatient()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();

            lb_username.TextAlign = ContentAlignment.MiddleRight;
            lb_username.AutoSize = false;
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            bt_logout.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lb_username.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            string sns = ProjectoESGPS.Properties.Settings.Default.SNS;
            Patient paciente = context.PatientSet.Where(i => i.SNS == sns).FirstOrDefault();

            if (sns != null)
            {
                tb_sns.Text = sns.ToString();
                tb_fname.Text = paciente.Fname;
                tb_lname.Text = paciente.Lname;
                tb_address.Text = paciente.Address;
                tb_phone.Text = paciente.Phone.ToString();
                comboBox1.Text = paciente.Gender;
                dateTimePicker1.Value = paciente.DateBirth;
            }
        }

        private static bool IsNumeric(string data)
        {
            bool isnumeric = true;
            char[] datachars = data.ToCharArray();

            foreach (var datachar in datachars)
                if(isnumeric == true)
                    isnumeric = Char.IsDigit(datachar);

            return isnumeric;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String sns = tb_sns.Text;
            String fname = tb_fname.Text;
            String lname = tb_lname.Text;
            String address = tb_address.Text;
            String phone = tb_phone.Text;
            String gender = comboBox1.SelectedItem.ToString();
            DateTime birthdate = dateTimePicker1.Value;

            if (tb_lname.Text == "" || tb_fname.Text == "" || tb_address.Text == "" || tb_sns.Text == "" || tb_phone.Text == "")
            {
                MessageBox.Show("Tem de preencher todos os campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (tb_sns.Text.Length != 9 || !IsNumeric(sns))
            {
                MessageBox.Show("SNS em formato errado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (tb_phone.Text.Length != 9 || !IsNumeric(phone))
            {
                MessageBox.Show("Numero de Telefone formato errado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (fname.Length < 2)
            {
                MessageBox.Show("First Name tem de ter 2 ou mais caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (lname.Length < 2)
            {
                MessageBox.Show("Last Name tem de ter 2 ou mais caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (address.Length < 2)
            {
                MessageBox.Show("Morada tem de ter 2 ou mais caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (birthdate > DateTime.Today)
            {
                MessageBox.Show("Tem de selecionar Data Nascimento Valida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                List<Patient> listaP = new List<Patient>();

                listaP = context.PatientSet.Where(i => i.SNS == sns).ToList();

                if (listaP.Count() == 0)
                {
                    Patient paciente = new Patient();

                    paciente.SNS = sns;
                    paciente.Fname = fname;
                    paciente.Lname = lname;
                    paciente.Address = address;
                    paciente.Phone = phone;
                    paciente.Gender = gender;
                    paciente.DateBirth = birthdate;

                    context.PatientSet.Add(paciente);
                    ProjectoESGPS.Properties.Settings.Default.SNS = sns;
                    context.SaveChanges();

                    MessageBox.Show("Paciente criado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    Patient patient = context.PatientSet.Where(i => i.SNS == sns).FirstOrDefault();

                    patient.SNS = sns;
                    patient.Fname = fname;
                    patient.Lname = lname;
                    patient.Address = address;
                    patient.Phone = phone;
                    patient.Gender = gender;
                    patient.DateBirth = birthdate;
                    
                    context.SaveChanges();

                    MessageBox.Show("Paciente alterado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchPatient search = new SearchPatient();
            search.Show();
            this.Hide();
        }

        private void bt_logout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void tb_sns_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
