using DataLayer;
using DataLayer.DataLayer;
using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for TeamStatistics.xaml
    /// </summary>
    public partial class TeamStatistics : Window
    {
        private static readonly string del = "/";

        private static readonly string relativePath = @"DataLayer\settings.txt";

        private static readonly string SettingsFilePath = SettingsManager.GetSettingsPath(relativePath);
        public TeamStatistics(string fifacode, string url, string filePath, bool FileRead)
        {
            InitializeComponent();
            CheckLanguage();
            LoadWPF(fifacode, url, filePath, FileRead);
            
        }

        private async void LoadWPF(string fifacode, string url, string filePath, bool FileRead)
        {
            IRepoResults RepoResults = new RepoFactoryResults();

            List<Results> results = new List<Results>();

            if (FileRead == false)
            {
                results = await RepoResults.GetResultsAsyncSwagger(url);
            }
            else
            {
                results = await RepoResults.GetResultsAsyncFile(filePath);
            }
            

            foreach (Results r in results)
            {
                if(fifacode == r.Fifa_Code)
                {
                    lblName.Content = r.Country;
                    lblFifaCode.Content = r.Fifa_Code;
                    lblGamesStats.Content = r.games_played + del + r.wins + del + r.losses + del + r.draws;
                    lblGoalsStats.Content = r.goals_for + del + r.goals_against + del + r.goal_differential;
                    break;
                }
            }
        }

        private void CheckLanguage()
        {
            if (File.Exists(SettingsFilePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(SettingsFilePath))
                    {
                        string gender = reader.ReadLine()?.Split(':')[1]?.Trim();
                        string language = reader.ReadLine()?.Split(':')[1]?.Trim();
                        if (language == "English")
                        {
                            lblLName.Content = "Name:";
                            lblLFifaCode.Content = "Number:";
                            lblLGamesStats.Content = "Number of games/winds/losses/draws:";
                            lblLGoalsStats.Content = "Goals scored/conceived/difference:";
                        }
                        else if (language == "Croatian")
                        {
                            lblLName.Content = "Naziv:";
                            lblLFifaCode.Content = "Broj:";
                            lblLGamesStats.Content = "Broj igara/vjetrova/gubitaka/izjednačenja:";
                            lblLGoalsStats.Content = "Postignuti/koncipirani golovi/razlika:";
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

    }
}
