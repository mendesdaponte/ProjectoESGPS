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
    public partial class AddUser : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        public AddUser()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            comboBox1.SelectedIndex = 2;

            button4.Hide();

            String userAlterar = ProjectoESGPS.Properties.Settings.Default.UserAlterar;
            if (userAlterar != "a")
            {
                User utilizadorAlterar = context.UserSet.Where(i => i.Username == userAlterar).FirstOrDefault();
                tb_user.Text = utilizadorAlterar.Username;
                tb_fn.Text = utilizadorAlterar.Fname;
                tb_ln.Text = utilizadorAlterar.Lname;
                tb_pw.Text = utilizadorAlterar.Pw;
                tb_email.Text = utilizadorAlterar.Email;
                comboBox1.Text = utilizadorAlterar.Tipo;

                button4.Show();
                button3.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainAdmin mainAdmin = new MainAdmin();
            mainAdmin.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String user = tb_user.Text;
            String pw = tb_pw.Text;
            String email = tb_email.Text;
            String fn = tb_fn.Text;
            String ln = tb_ln.Text;
            String tipo = comboBox1.SelectedItem.ToString();
            bool aux = IsValidEmail(email);

            if (tb_user.Text == "" || tb_pw.Text == "" || tb_email.Text == "" || tb_ln.Text == "" || tb_ln.Text == "")
            {
                MessageBox.Show("Tem de preencher todos os campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (user.Length < 5)
            {
                MessageBox.Show("Username tem de ter 5 ou mais caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (pw.Length < 2)
            {
                MessageBox.Show("Password tem de ter 2 ou mais caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (aux == false)
            {
                MessageBox.Show("Email em formato invalido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else {
                List<User> listaU = new List<User>();

                listaU = context.UserSet.Where(i => i.Username == user).ToList();

                if (listaU.Count() != 0)
                {
                    MessageBox.Show("Utilizador já existente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    char tipoFinal = CheckType(tipo);
                    User utilizador = new User();

                    utilizador.Username = user;
                    utilizador.Pw = pw;
                    utilizador.Email = email;
                    utilizador.Fname = fn;
                    utilizador.Lname = ln;
                    utilizador.Tipo = tipoFinal.ToString();

                    context.UserSet.Add(utilizador);
                    context.SaveChanges();

                    MessageBox.Show("Utilizador criado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private char CheckType(String tipo)
        {
            if(tipo == "Doctor")
            {
                return 'D';
            }
            else if(tipo == "Nurse")
            {
                return 'N';
            }
            else
            {
                return 'T';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String user = tb_user.Text;
            String pw = tb_pw.Text;
            String email = tb_email.Text;
            String fn = tb_fn.Text;
            String ln = tb_ln.Text;
            String tipo = comboBox1.SelectedItem.ToString();
            bool aux = IsValidEmail(email);

            if (tb_user.Text == "" || tb_pw.Text == "" || tb_email.Text == "" || tb_ln.Text == "" || tb_ln.Text == "")
            {
                MessageBox.Show("Tem de preencher todos os campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (user.Length < 5)
            {
                MessageBox.Show("Username tem de ter 5 ou mais caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (pw.Length < 2)
            {
                MessageBox.Show("Password tem de ter 2 ou mais caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (aux == false)
            {
                MessageBox.Show("Email em formato invalido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                char tipoFinal = CheckType(tipo);
                User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();

                utilizador.Username = user;
                utilizador.Pw = pw;
                utilizador.Email = email;
                utilizador.Fname = fn;
                utilizador.Lname = ln;
                utilizador.Tipo = tipoFinal.ToString();

                context.SaveChanges();

                MessageBox.Show("Utilizador Alterado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}