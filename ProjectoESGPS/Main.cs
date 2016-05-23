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
    public partial class Main : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        public Main()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;

            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();
            if (utilizador.Tipo == "T")
            {
                button3.Hide();
            }

            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchPatient searchPatient = new SearchPatient();
            searchPatient.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AppointmentList appointmentList = new AppointmentList();
            appointmentList.Show();
            this.Hide();
        }
    }
}
