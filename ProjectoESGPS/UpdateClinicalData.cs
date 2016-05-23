using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectoESGPS
{
    public partial class UpdateClinicalData : Form
    {
        ModelDiagramaBDContainer context = new ModelDiagramaBDContainer();

        string snsPaciente = ProjectoESGPS.Properties.Settings.Default.SNS;

        public UpdateClinicalData()
        {
            InitializeComponent();

            String user = ProjectoESGPS.Properties.Settings.Default.User;
            User utilizador = context.UserSet.Where(i => i.Username == user).FirstOrDefault();
            lb_username.Text = utilizador.Fname + " " + utilizador.Lname;

            Patient paciente = context.PatientSet.Where(i => i.SNS == snsPaciente).FirstOrDefault();
            lb_sns.Text = paciente.SNS.ToString();
            lb_name.Text = paciente.Fname + " " + paciente.Lname;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdatePatient updatePatient = new UpdatePatient();
            updatePatient.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();

            int aux = context.FilesSet.Where(i => i.Patient.SNS == snsPaciente).ToList().Count();

            if(openFileDialog1.OpenFile() != null)
            {
                String dir = openFileDialog1.FileName;
                String destino = @"CData\" + snsPaciente;
                string[] split = dir.Split('.');

                try
                {
                    // Determine whether the directory exists.
                    if (Directory.Exists(destino))
                    {
                        File.Copy(dir, Path.Combine(destino, aux.ToString() + "." + split[split.Length -1]), true);
                        ProjectoESGPS.Properties.Settings.Default.Dir = destino + aux.ToString() + "." + split[split.Length - 1];

                        Files file = new Files();

                        file.Dir = destino + aux.ToString() + "." + split[split.Length - 1];
                        file.Patient = context.PatientSet.Where(i => i.SNS == snsPaciente).FirstOrDefault();

                        context.FilesSet.Add(file);
                        
                        context.SaveChanges();

                        MessageBox.Show("Clinical data file adicionado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(destino);
                        Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(destino));

                        File.Copy(dir, Path.Combine(destino, aux.ToString() + "." + split[split.Length - 1]), true);
                        ProjectoESGPS.Properties.Settings.Default.Dir = destino + aux.ToString() + "." + split[split.Length - 1];

                        Files file = new Files();

                        file.Dir = destino + aux.ToString() + "." + split[split.Length - 1];
                        file.Patient = context.PatientSet.Where(i => i.SNS == snsPaciente).FirstOrDefault();

                        context.FilesSet.Add(file);

                        context.SaveChanges();

                        MessageBox.Show("Clinical data file adicionado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception a)
                {
                    Console.WriteLine("The process failed: {0}", a.ToString());
                }
            }
        }
    }
}
