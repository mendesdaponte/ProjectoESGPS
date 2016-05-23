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
    public partial class ManagePatient : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        string snsPaciente = ProjectoESGPS.Properties.Settings.Default.SNS;

        public ManagePatient()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            Patient paciente = context.PatientSet.Where(i => i.SNS == snsPaciente).FirstOrDefault();
            lb_sns.Text = paciente.SNS.ToString();
            lb_name.Text = paciente.Fname + " " + paciente.Lname;


            if (utilizador.Tipo == "T")
            {
                button6.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchPatient search = new SearchPatient();
            search.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdatePatient updatePatient = new UpdatePatient();
            updatePatient.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que deseja eliminar o paciente?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Patient paciente = context.PatientSet.Where(i => i.SNS == snsPaciente).FirstOrDefault();

                context.PatientSet.Remove(paciente);
                context.SaveChanges();

                MessageBox.Show("Paciente Eleminado com Sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CreateAppointment createAppointment = new CreateAppointment();
            createAppointment.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sns = ProjectoESGPS.Properties.Settings.Default.SNS;
            if (sns != null)
            {
                UpdateClinicalData updateClinicalData = new UpdateClinicalData();
                updateClinicalData.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Primeiro tem de criar um paciente para fazer o upload", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        
        }
    }
}
