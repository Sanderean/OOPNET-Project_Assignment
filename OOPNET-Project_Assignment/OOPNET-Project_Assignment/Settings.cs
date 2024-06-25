using DataLayer;
using OOPNET_Project_Assignment.DataLayer;
using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPNET_Project_Assignment
{
    public partial class Settings : Form
    {
        private static readonly string relativePath = @"DataLayer\settings.txt";
        private static readonly string FilePath = SettingsManager.GetSettingsPath(relativePath);

        private static string url;
        public Settings()
        {
            InitializeComponent();
            CheckLanguage();
            cbGender.SelectedIndex = 0;
            cbLanguage.SelectedIndex = 0;
        }

        private void CheckLanguage()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string gender = reader.ReadLine()?.Split(':')[1]?.Trim();
                        string language = reader.ReadLine()?.Split(':')[1]?.Trim();
                        if (language == "English")
                        {
                            lblChooseGender.Text = "Choose gender:";
                            lblChooseLanguage.Text = "Choose language:";
                            btnSubmit.Text = "Submit";
                            cbReadFromFile.Text = "Read from file";
                        }
                        else if (language == "Croatian")
                        {
                            lblChooseGender.Text = "Odaberite spol:";
                            lblChooseLanguage.Text = "Odaberite jezik:";
                            btnSubmit.Text = "Podnijeti";
                            cbReadFromFile.Text = "Čitaj iz datoteke";
                        }
                        else
                        {
                            MessageBox.Show("Error while checking language");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading file: " + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;

            this.Location = new Point(x, y);
        }

        private void WriteToFile(string FilePath, string gender, string language)
        {
            try
            {
                if (gender == "Male")
                {
                    url = "/men";
                }
                else if(gender == "Female")
                {
                    url = "/women";
                }
                else
                {
                    MessageBox.Show("Error while assigning the gender");
                }
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.WriteLine("Gender: " + gender);
                    writer.WriteLine("Language: " + language);
                    if (cbReadFromFile.Checked)
                    {
                        writer.WriteLine("FileRead: " + 1);
                    }
                    else
                    {
                        writer.WriteLine("FileRead: " + 0);
                    }
                    writer.WriteLine("Resolution: 1920x1080");
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void btnSubmit_Click(object sender, EventArgs e)
        {   

            WriteToFile(FilePath, cbGender.Text, cbLanguage.Text);

            this.Hide();

            IRepoTeam repoTeam = new RepoFactoryTeam();
            try
            {
                List<Team> teams = await repoTeam.GetTeamsAsyncSwagger(url);
                Application form2 = new Application();
                form2.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
