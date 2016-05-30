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
    public partial class AppointmentList : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        public AppointmentList()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();

            lb_username.TextAlign = ContentAlignment.MiddleRight;
            lb_username.AutoSize = false;
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            bt_logout.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lb_username.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            List<Appointement> allListaConsultas = context.AppointementSet.ToList();

            foreach (Appointement item in allListaConsultas)
            {
                ListViewItem linha = new ListViewItem(item.Id.ToString());
                linha.SubItems.Add(item.Patient.SNS.ToString());
                linha.SubItems.Add(item.Patient.Fname + " " + item.Patient.Lname);
                linha.SubItems.Add(item.Doctor);
                linha.SubItems.Add(item.Date.ToShortDateString());

                

                if(item.Diagnosis == null || item.Medication == null || item.Obs == null)
                {
                    listView1.Items.Add(linha).BackColor = Color.Green;
                }
                else
                {
                    listView1.Items.Add(linha).BackColor = Color.Blue;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void ActualizarLista()
        {
            listView1.Items.Clear();

            List<Appointement> listaConsultas = new List<Appointement>();

            char utilizadorTipo = ProjectoESGPS.Properties.Settings.Default.Tipo;
            string utilizadorName = ProjectoESGPS.Properties.Settings.Default.User;

            if (utilizadorTipo == 'D')
            {
                listaConsultas = context.AppointementSet.Where(i => i.Doctor == utilizadorName && i.Date == dateTimePicker1.Value.Date).ToList();

                foreach (Appointement item in listaConsultas)
                {
                    ListViewItem linha = new ListViewItem(item.Id.ToString());
                    linha.SubItems.Add(item.Patient.SNS.ToString());
                    linha.SubItems.Add(item.Patient.Fname + " " + item.Patient.Lname);
                    linha.SubItems.Add(item.Doctor);
                    linha.SubItems.Add(item.Id.ToString());

                    listView1.Items.Add(linha);
                }
            }
            else
            {
                List<Appointement> allListaConsultas = context.AppointementSet.Where(i => i.Date == dateTimePicker1.Value.Date).ToList();                

                foreach (Appointement item in allListaConsultas)
                {
                    ListViewItem linha = new ListViewItem(item.Id.ToString());
                    linha.SubItems.Add(item.Patient.SNS.ToString());
                    linha.SubItems.Add(item.Patient.Fname + " " + item.Patient.Lname);
                    linha.SubItems.Add(item.Doctor);

                    listView1.Items.Add(linha);
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ActualizarLista();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            int index = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);            

            Appointement appointement = context.AppointementSet.Where(i => i.Id == index).FirstOrDefault();

            ProjectoESGPS.Properties.Settings.Default.Appointement = index;
            ProjectoESGPS.Properties.Settings.Default.SNS = appointement.Patient.SNS;
            ProjectoESGPS.Properties.Settings.Default.Save();            

            AppointmentInfo infoConsulta = new AppointmentInfo();
            infoConsulta.Show();
            this.Hide();
        }

        private void bt_logout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
