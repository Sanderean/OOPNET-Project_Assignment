using DataLayer;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for PlayerOverview.xaml
    /// </summary>
    public partial class PlayerOverview : Window
    {
        private static readonly string relativePlayerImagePath = @"DataLayer\PlayerImage.txt";

        private static readonly string PlayerImagePath = SettingsManager.GetSettingsPath(relativePlayerImagePath);

        private static readonly string relativePath = @"DataLayer\settings.txt";

        private static readonly string SettingsFilePath = SettingsManager.GetSettingsPath(relativePath);
        public PlayerOverview(OOPNET_Project_Assignment.Moduls.Country.Root root, string selectedPlayer)
        {
            InitializeComponent();
            CheckLanguage();
            int goals = 0;
            int yellowcards = 0;
            Image img = new Image();
            Uri uri = new Uri("/Images/UnknownPlayer.png", UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            img.Source = bitmapImage;
            foreach (var player in root.home_team_statistics.starting_eleven)
            {
                if (player.name == selectedPlayer)
                {
                    foreach (string line in File.ReadAllLines(PlayerImagePath))
                    {
                        // Разделяем строку на имя игрока и путь к изображению
                        string[] parts = line.Split('=');
                        if (parts.Length < 2) continue; // Пропустить строки, если они неправильно отформатированы

                        string imageName = parts[0].Trim();
                        string imagePathI = line.Split('=')[1].Trim();
                        if (player.name == imageName)
                        {
                            img.Source = new BitmapImage(new Uri(imagePathI));
                            break; // Выход из внутреннего цикла после нахождения соответствующего изображения
                        }
                    }
                    imgPlayerPicture.Source = img.Source;
                    lblName.Content = player.name;
                    lblNumber.Content = player.shirt_number;
                    lblPosition.Content = player.position;
                    if (player.captain == true)
                    {
                        lblIsCapitan.Content = "Yes";
                    }
                    else
                    {
                        lblIsCapitan.Content = "No";
                    }
                }
            }



            foreach (var player in root.away_team_statistics.starting_eleven)
            {
                if (player.name == selectedPlayer)
                {
                    foreach (string line in File.ReadAllLines(PlayerImagePath))
                    {
                        // Разделяем строку на имя игрока и путь к изображению
                        string[] parts = line.Split('=');
                        if (parts.Length < 2) continue; // Пропустить строки, если они неправильно отформатированы

                        string imageName = parts[0].Trim();
                        string imagePathI = line.Split('=')[1].Trim();
                        if (player.name == imageName)
                        {
                            img.Source = new BitmapImage(new Uri(imagePathI));
                            break; // Выход из внутреннего цикла после нахождения соответствующего изображения
                        }
                    }
                    imgPlayerPicture.Source = img.Source;
                    lblName.Content = player.name;
                    lblNumber.Content = player.shirt_number;
                    lblPosition.Content = player.position;
                    if (player.captain == true)
                    {
                        lblIsCapitan.Content = "Yes";
                    }
                    else
                    {
                        lblIsCapitan.Content = "No";
                    }
                }
            }

            foreach (var player in root.home_team_events)
            {
                if(player.player == selectedPlayer)
                {
                    if(player.type_of_event == "yellow-card")
                    {
                        yellowcards++;
                    }
                    if(player.type_of_event == "goal" || player.type_of_event == "goal-penalty")
                    {
                        goals++;
                    }
                }
            }

            foreach (var player in root.away_team_events)
            {
                if (player.player == selectedPlayer)
                {
                    if (player.type_of_event == "yellow-card")
                    {
                        yellowcards++;
                    }
                    if (player.type_of_event == "goal" || player.type_of_event == "goal-penalty")
                    {
                        goals++;
                    }
                }
            }

            lblGoals.Content = goals;
            lblYellowCards.Content = yellowcards;
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
                            lblLNumber.Content = "Number:";
                            lblLPosition.Content = "Position:";
                            lblLIsCaptain.Content = "Is capitan:";
                            lblLGoals.Content = "Goals:";
                            lblLYellowCards.Content = "Yellow cards:";
                        }
                        else if (language == "Croatian")
                        {
                            lblLName.Content = "Naziv:";
                            lblLNumber.Content = "Broj:";
                            lblLPosition.Content = "Pozicija:";
                            lblLIsCaptain.Content = "Je li kapetan:";
                            lblLGoals.Content = "Ciljevi:";
                            lblLYellowCards.Content = "Žuti kartoni:";
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
