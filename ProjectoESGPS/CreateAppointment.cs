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
    public partial class CreateAppointment : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        string snsPaciente = ProjectoESGPS.Properties.Settings.Default.SNS;

        public CreateAppointment()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            Patient paciente = context.PatientSet.Where(i => i.SNS == snsPaciente).FirstOrDefault();
            lb_sns.Text = paciente.SNS.ToString();
            lb_name.Text = paciente.Fname + " " + paciente.Lname;

            List<User> listDoctors = context.UserSet.Where(i => i.Tipo == "D").ToList();
            foreach (var item in listDoctors)
            {
                comboBox1.Items.Add(item.Username);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManagePatient managePatient = new ManagePatient();
            managePatient.Show();
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
            if (dateTimePicker1.Value >= DateTime.Today)
            {
                Appointement consulta = new Appointement();

                consulta.Patient = context.PatientSet.Where(i => i.SNS == snsPaciente).FirstOrDefault();
                consulta.Doctor = comboBox1.Text;
                consulta.Date = dateTimePicker1.Value.Date;

                context.AppointementSet.Add(consulta);
                context.SaveChanges();

                MessageBox.Show("Consulta criada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Data invalida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
