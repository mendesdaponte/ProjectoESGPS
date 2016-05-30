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
    public partial class MainAdmin : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        public MainAdmin()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();

            lb_username.TextAlign = ContentAlignment.MiddleRight;
            lb_username.AutoSize = false;
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            bt_logout.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lb_username.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProjectoESGPS.Properties.Settings.Default.UserAlterar = "a";

            AddUser addUser = new AddUser();
            addUser.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Permitions permitions = new Permitions();
            permitions.Show();
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
