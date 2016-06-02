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
    public partial class Login : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();
        public Login()
        {
            InitializeComponent();

            tb_pw.PasswordChar = '*';

            tb_user.Text = ProjectoESGPS.Properties.Settings.Default.User;
            tb_pw.Text = ProjectoESGPS.Properties.Settings.Default.Pw;
        }

        private void bt1_Click(object sender, EventArgs e)
        {            

            String user = tb_user.Text;
            String pw = tb_pw.Text;
            bool aux = false; 
            char tipo;

            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();

            if(utilizador == null)
            {
                MessageBox.Show("utilizador não existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                aux = VerificarPW(utilizador, pw);
                if(aux == false)
                {
                    MessageBox.Show("Credenciais erradas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    tipo = VerificarTipo(utilizador);
                    ProjectoESGPS.Properties.Settings.Default.User = user;
                    ProjectoESGPS.Properties.Settings.Default.Pw = pw;
                    ProjectoESGPS.Properties.Settings.Default.Tipo = tipo;
                    ProjectoESGPS.Properties.Settings.Default.Save();

                    if (tipo == 'A')
                    {
                        MainAdmin mainAdmin = new MainAdmin();
                        mainAdmin.Show();
                        this.Hide();
                    }
                    else
                    {
                        Main main = new Main();
                        main.Show();
                        this.Hide();
                    }
                }
            }
        }

        private bool VerificarPW(User utilizador, String pw)
        {
            if(utilizador.Pw == pw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Char VerificarTipo(User utilizador)
        {
            if(utilizador.Tipo == "A")
            {
                return 'A';
            }
            else if (utilizador.Tipo == "D")
            {
                return 'D';
            }
            else if (utilizador.Tipo == "N")
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
            User user = new User();

            user.Username = "Admin";
            user.Fname = "Hugo";
            user.Lname = "Daurte";
            user.Email = "mendesdaponte@gmail.com";
            user.Pw = "pw";
            user.Tipo = "A";

            context.UserSet.Add(user);

            User user1 = new User();

            user1.Username = "Doctor";
            user1.Fname = "Pedro";
            user1.Lname = "Casqueiro";
            user1.Email = "doctor@gmail.com";
            user1.Pw = "pw";
            user1.Tipo = "D";

            context.UserSet.Add(user1);

            User user2 = new User();

            user2.Username = "nurse";
            user2.Fname = "Ricardo";
            user2.Lname = "Oliveira";
            user2.Email = "nuser@gmail.com";
            user2.Pw = "pw";
            user2.Tipo = "N";

            context.UserSet.Add(user2);

            User user3 = new User();

            user3.Username = "attendant";
            user3.Fname = "Diogo";
            user3.Lname = "Carreira";
            user3.Email = "attendi@gmail.com";
            user3.Pw = "pw";
            user3.Tipo = "T";

            context.UserSet.Add(user3);
            context.SaveChanges();

            MessageBox.Show("Users criados com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Patient paciente = new Patient();

            paciente.SNS = "100000000";
            paciente.Fname = "Ricardo";
            paciente.Lname = "Oliveira";
            paciente.Address = "Rua Qualquer coisa e cenas";
            paciente.Phone = "910000000";
            paciente.Gender = "M";
            paciente.DateBirth = new DateTime(1993, 6, 10);

            context.PatientSet.Add(paciente);

            Patient paciente1 = new Patient();

            paciente1.SNS = "100000003";
            paciente1.Fname = "Hugo";
            paciente1.Lname = "Duarte";
            paciente1.Address = "Rua Qualquer coisa e cenas";
            paciente1.Phone = "965373072";
            paciente1.Gender = "M";
            paciente1.DateBirth = new DateTime(1992, 3, 24);

            context.PatientSet.Add(paciente1);

            Patient paciente2 = new Patient();

            paciente2.SNS = "100000001";
            paciente2.Fname = "Diogo";
            paciente2.Lname = "Carreira";
            paciente2.Address = "Rua Qualquer coisa e cenas";
            paciente2.Phone = "910000001";
            paciente2.Gender = "F";
            paciente2.DateBirth = new DateTime(1994, 6, 10);

            context.PatientSet.Add(paciente2);

            Patient paciente3 = new Patient();

            paciente3.SNS = "100000002";
            paciente3.Fname = "Pedro";
            paciente3.Lname = "Casqueiro";
            paciente3.Address = "Rua Qualquer coisa e cenas";
            paciente3.Phone = "910000002";
            paciente3.Gender = "F";
            paciente3.DateBirth = new DateTime(1994, 6, 10);

            context.PatientSet.Add(paciente3);


            context.SaveChanges();

            MessageBox.Show("Pacientes criado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Appointement consulta = new Appointement();

            consulta.Patient = context.PatientSet.Where(i => i.SNS == "100000000").FirstOrDefault();
            consulta.Doctor = "doctor";
            consulta.Date = new DateTime(2016, 5, 1);

            context.AppointementSet.Add(consulta);

            Appointement consulta1 = new Appointement();

            consulta1.Patient = context.PatientSet.Where(i => i.SNS == "100000001").FirstOrDefault();
            consulta1.Doctor = "doctor";
            consulta1.Date = new DateTime(2016, 5, 1);

            context.AppointementSet.Add(consulta1);

            Appointement consulta2 = new Appointement();

            consulta2.Patient = context.PatientSet.Where(i => i.SNS == "100000002").FirstOrDefault();
            consulta2.Doctor = "doctor";
            consulta2.Date = new DateTime(2016, 5, 1);

            context.AppointementSet.Add(consulta2);

            Appointement consulta3 = new Appointement();

            consulta3.Patient = context.PatientSet.Where(i => i.SNS == "100000003").FirstOrDefault();
            consulta3.Doctor = "doctor";
            consulta3.Date = new DateTime(2016, 5, 1);

            context.AppointementSet.Add(consulta3);

            Appointement consulta4 = new Appointement();

            consulta4.Patient = context.PatientSet.Where(i => i.SNS == "100000000").FirstOrDefault();
            consulta4.Doctor = "doctor";
            consulta4.Date = new DateTime(2016, 5, 2);

            context.AppointementSet.Add(consulta4);

            Appointement consulta5 = new Appointement();

            consulta5.Patient = context.PatientSet.Where(i => i.SNS == "100000001").FirstOrDefault();
            consulta5.Doctor = "doctor";
            consulta5.Date = new DateTime(2016, 5, 2);

            context.AppointementSet.Add(consulta5);

            Appointement consulta6 = new Appointement();

            consulta6.Patient = context.PatientSet.Where(i => i.SNS == "100000002").FirstOrDefault();
            consulta6.Doctor = "doctor";
            consulta6.Date = new DateTime(2016, 5, 2);

            context.AppointementSet.Add(consulta6);

            Appointement consulta7 = new Appointement();

            consulta7.Patient = context.PatientSet.Where(i => i.SNS == "100000003").FirstOrDefault();
            consulta7.Doctor = "doctor";
            consulta7.Date = new DateTime(2016, 5, 2);

            context.AppointementSet.Add(consulta7);

            Appointement consulta8 = new Appointement();

            consulta8.Patient = context.PatientSet.Where(i => i.SNS == "100000000").FirstOrDefault();
            consulta8.Doctor = "doctor";
            consulta8.Date = new DateTime(2016, 5, 3);

            context.AppointementSet.Add(consulta8);

            Appointement consulta9 = new Appointement();

            consulta9.Patient = context.PatientSet.Where(i => i.SNS == "100000001").FirstOrDefault();
            consulta9.Doctor = "doctor";
            consulta9.Date = new DateTime(2016, 5, 3);

            context.AppointementSet.Add(consulta9);

            Appointement consulta10 = new Appointement();

            consulta10.Patient = context.PatientSet.Where(i => i.SNS == "100000002").FirstOrDefault();
            consulta10.Doctor = "doctor";
            consulta10.Date = new DateTime(2016, 5, 3);

            context.AppointementSet.Add(consulta10);

            Appointement consulta11 = new Appointement();

            consulta11.Patient = context.PatientSet.Where(i => i.SNS == "100000003").FirstOrDefault();
            consulta11.Doctor = "doctor";
            consulta11.Date = new DateTime(2016, 5, 3);

            context.AppointementSet.Add(consulta11);

            context.SaveChanges();

            MessageBox.Show("Appointments criados com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tb_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bt1_Click(this, new EventArgs());
            }
        }

        private void tb_pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bt1_Click(this, new EventArgs());
            }
        }
    }
}
