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
    public partial class SearchPatient : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        public SearchPatient()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            ActualizarLista();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void ActualizarLista()
        {
            String varPesquisa = tb_pesq.Text;          

            List<Patient> listPatients = new List<Patient>();
            
            listPatients = context.PatientSet.ToList();

            listView1.Items.Clear();

            foreach (Patient item in listPatients)
            {
                ListViewItem linha = new ListViewItem(item.SNS.ToString());
                linha.SubItems.Add(item.Fname);
                linha.SubItems.Add(item.Lname);
                linha.SubItems.Add(item.Address);
                linha.SubItems.Add(item.Phone.ToString());
                linha.SubItems.Add(item.Gender);
                linha.SubItems.Add(item.DateBirth.ToShortDateString());

                listView1.Items.Add(linha);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private static bool IsNumeric(string data)
        {
            bool isnumeric = true;
            char[] datachars = data.ToCharArray();

            foreach (var datachar in datachars)
                if (isnumeric == true)
                    isnumeric = Char.IsDigit(datachar);

            return isnumeric;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProjectoESGPS.Properties.Settings.Default.SNS = null;
            ProjectoESGPS.Properties.Settings.Default.Save();

            UpdatePatient updatePatient = new UpdatePatient();
            updatePatient.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String varPesquisa = tb_pesq.Text;
            DateTime aux;

            List<Patient> listPatients = new List<Patient>();

            if (tb_pesq.Text == "")
            {
                listPatients = context.PatientSet.ToList();
            }
            else
            {
                if (DateTime.TryParse(varPesquisa, out aux))
                {
                    listPatients = context.PatientSet.Where(i => i.DateBirth == Convert.ToDateTime(varPesquisa)).ToList();
                }
                else
                {
                    listPatients = context.PatientSet.Where(i => i.SNS.Contains(varPesquisa) || i.Fname.Contains(varPesquisa) || i.Lname.Contains(varPesquisa) || i.Address.Contains(varPesquisa) || i.Gender.Contains(varPesquisa) || i.Phone.Contains(varPesquisa)).ToList();
                }
            }

            listView1.Items.Clear();            

            foreach (Patient item in listPatients)
            {
                ListViewItem linha = new ListViewItem(item.SNS.ToString());
                linha.SubItems.Add(item.Fname);
                linha.SubItems.Add(item.Lname);
                linha.SubItems.Add(item.Address);
                linha.SubItems.Add(item.Phone.ToString());
                linha.SubItems.Add(item.Gender);
                linha.SubItems.Add(item.DateBirth.ToShortDateString());

                listView1.Items.Add(linha);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string index = listView1.SelectedItems[0].SubItems[0].Text;

            ProjectoESGPS.Properties.Settings.Default.SNS = index;
            ProjectoESGPS.Properties.Settings.Default.Save();

            ManagePatient managePatient = new ManagePatient();
            managePatient.Show();
            this.Hide();
        }
    }
}
