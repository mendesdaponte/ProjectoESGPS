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
    public partial class Permitions : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        public Permitions()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();

            lb_username.TextAlign = ContentAlignment.MiddleRight;
            lb_username.AutoSize = false;
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            bt_logout.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lb_username.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            ActualizarLista();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int indexComboBox = comboBox1.SelectedIndex;
            string indexUser = listView1.SelectedItems[0].Text;

            User utilizador = context.UserSet.Where(i => i.Username == indexUser).FirstOrDefault();

            if (indexComboBox == 0)
            {
                utilizador.Tipo = "D"; MessageBox.Show("Premicoes alteradas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (indexComboBox == 1)
            {
                utilizador.Tipo = "N"; MessageBox.Show("Premicoes alteradas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                utilizador.Tipo = "T"; MessageBox.Show("Premicoes alteradas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            context.SaveChanges();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string indexUser = listView1.SelectedItems[0].Text;

            User utilizador = context.UserSet.Where(i => i.Username == indexUser).FirstOrDefault();

            if(utilizador.Tipo == "D")
            {
                comboBox1.SelectedIndex = 0;
            }
            else if(utilizador.Tipo == "N")
            {
                comboBox1.SelectedIndex = 1;
            }
            else
            {
                comboBox1.SelectedIndex = 2;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Tem a certeza que deseja eliminar o user?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                string indexUser = listView1.SelectedItems[0].Text;

                User utilizador = context.UserSet.Where(i => i.Username == indexUser).FirstOrDefault();

                context.UserSet.Remove(utilizador);
                context.SaveChanges();

                MessageBox.Show("Utilizador Eleminado com Sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                ActualizarLista();
            }
        }

        private void ActualizarLista()
        {
            listView1.Clear();
            List<User> listUsers = context.UserSet.Where(i => i.Tipo != "A").ToList();

            foreach (var item in listUsers)
            {
                listView1.Items.Add(item.Username);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainAdmin mainAdmin = new MainAdmin();
            mainAdmin.Show();
            this.Hide();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string indexUser = listView1.SelectedItems[0].Text;

            User utilizadorAlterar = context.UserSet.Where(i => i.Username == indexUser).FirstOrDefault();

            ProjectoESGPS.Properties.Settings.Default.UserAlterar = utilizadorAlterar.Username;
            ProjectoESGPS.Properties.Settings.Default.Save();

            AddUser addUser = new AddUser();
            addUser.Show();
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
