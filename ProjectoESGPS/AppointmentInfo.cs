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
    public partial class AppointmentInfo : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();       

        String snsPaciente = ProjectoESGPS.Properties.Settings.Default.SNS;
        int idAppointment = ProjectoESGPS.Properties.Settings.Default.Appointement;

        public AppointmentInfo()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();

            lb_username.TextAlign = ContentAlignment.MiddleRight;
            lb_username.AutoSize = false;
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            bt_logout.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lb_username.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            if (utilizador.Tipo == "N")
            {
                lb_diagnostic.Hide();
                rtb_diagnostic.Hide();
                rtb_medication.ReadOnly = true;
                rtb_obs.ReadOnly = true;
            }

            Patient paciente = context.PatientSet.Where(i => i.SNS == snsPaciente).FirstOrDefault();
            lb_sns.Text = paciente.SNS.ToString();
            lb_name.Text = paciente.Fname + " " + paciente.Lname;

            Appointement appointement = context.AppointementSet.Where(i => i.Id == idAppointment).FirstOrDefault();
            lb_appointment.Text = appointement.Id.ToString();    
            
            if(appointement.Medication != null)
            {
                rtb_medication.Text = appointement.Medication;
            }
            if (appointement.Diagnosis != null)
            {
                rtb_diagnostic.Text = appointement.Medication;
            }
            if (appointement.Obs != null)
            {
                rtb_obs.Text = appointement.Obs;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AppointmentList appointmentList = new AppointmentList();
            appointmentList.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Appointement appointement = context.AppointementSet.Where(i => i.Id == idAppointment).FirstOrDefault();

            appointement.Diagnosis = rtb_diagnostic.Text;
            appointement.Medication = rtb_medication.Text;
            appointement.Obs = rtb_obs.Text;

            context.SaveChanges();

            MessageBox.Show("Appointment Registado", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void bt_logout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
