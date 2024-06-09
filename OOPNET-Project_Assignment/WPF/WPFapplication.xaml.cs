using DataLayer;
using Microsoft.Win32;
using OOPNET_Project_Assignment.DataLayer;
using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static OOPNET_Project_Assignment.Moduls.Country;

namespace WPF
{
    /// <summary>
    /// Interaction logic for WPFapplication.xaml
    /// </summary>
    public partial class WPFapplication : Window
    {
        private static readonly string relativePath = @"DataLayer\settings.txt";

        private static readonly string SettingsFilePath = SettingsManager.GetSettingsPath(relativePath);

        private static readonly string relativePlayerImagePath = @"DataLayer\PlayerImage.txt";

        private static readonly string PlayerImagePath = SettingsManager.GetSettingsPath(relativePlayerImagePath);

        private static bool FileRead = false;

        private static string url;

        private static string fifaCodeInitial;
        private static string fifacodeOpponent;

        private static string FilePathJsonCountries;

        private static string FilePathJsonTeams;

        private static string FilePathJsonResults;

        private static bool isSettingsSaved= false;

        public WPFapplication()
        {
            InitializeComponent();


            if (CheckFile())
            {
                using (StreamReader reader = new StreamReader(SettingsFilePath))
                {
                    string gender = reader.ReadLine()?.Split(':')[1]?.Trim();
                    string language = reader.ReadLine()?.Split(':')[1]?.Trim();
                    string FileReadMode = reader.ReadLine()?.Split(':')[1]?.Trim();
                    string resolution = reader.ReadLine()?.Split(':')[1]?.Trim();
                    string[] parts = resolution.Split('x');
                    if (parts.Length == 2)
                    {
                        int width;
                        int height;
                        if (int.TryParse(parts[0], out width))
                        {
                            this.Width = width;
                        }
                        if (int.TryParse(parts[1], out height))
                        {
                            this.Height = height;
                        }
                    }
                    if (FileReadMode == "1")
                    {
                        FileRead = true;
                        ChoosePathToJson();
                    }
                    if (gender == "Male")
                    {
                        url = "/men";
                    }
                    else if (gender == "Female")
                    {
                        url = "/women";
                    }
                    else
                    {
                        MessageBox.Show("Error when reading settings file");
                    }
                    if(language == "English")
                    {
                        lblChooseYourTeam.Content = "Choose your team";
                        lblChooseOpponentTeam.Content = "Choose opponent team";
                        btnSettings.Content = "Settings";
                        btnInitialTeamStats.Content = "Show statistics";
                        btnOpponentTeamStats.Content = "Show statistics";
                        lblLineupInitial.Content = "LINEUP";
                        lblLineupOpponent.Content = "LINEUP";
                        lblBenchInitial.Content = "BENCH";
                        lblBenchOpponent.Content = "BENCH";
                    }
                    else if (language == "Croatian")
                    {
                        lblChooseYourTeam.Content = "Odaberi svoj tim";
                        lblChooseOpponentTeam.Content = "Odaberi protivnički tim";
                        btnSettings.Content = "Postavke";
                        btnInitialTeamStats.Content = "Prikaži statistiku";
                        btnOpponentTeamStats.Content = "Prikaži statistiku";
                        lblLineupInitial.Content = "POSTAVA";
                        lblLineupOpponent.Content = "POSTAVA";
                        lblBenchInitial.Content = "KLUPA";
                        lblBenchOpponent.Content = "KLUPA";
                    }
                    else
                    {
                        MessageBox.Show("Error when reading settings file");
                    }
                }
            }

            StartApplication();


        }

        private void ChoosePathToJson()
        {
            try
            {
                MessageBox.Show("Choose JSON file to get list of all teams in world cup");
                OpenFileDialog openFileDialogTeams = new OpenFileDialog();
                openFileDialogTeams.Filter = "Json Files (*.json)|*.JSON";

                if (openFileDialogTeams.ShowDialog() == true)
                {
                    try
                    {
                        string FilePath = openFileDialogTeams.FileName;
                        string DestinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSON", System.IO.Path.GetFileName(FilePath));
                        File.Copy(FilePath, DestinationPath, true);

                        FilePathJsonTeams = DestinationPath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                MessageBox.Show("Choose JSON file to get matches for any country");
                OpenFileDialog openFileDialogCountry = new OpenFileDialog();
                openFileDialogCountry.Filter = "Json Files (*.json)|*.JSON";

                if (openFileDialogCountry.ShowDialog() == true)
                {
                    try
                    {
                        string FilePath = openFileDialogCountry.FileName;
                        string DestinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSON", System.IO.Path.GetFileName(FilePath));
                        File.Copy(FilePath, DestinationPath, true);

                        FilePathJsonCountries = DestinationPath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                MessageBox.Show("Choose JSON file to get results for teams");
                OpenFileDialog openFileDialogResults = new OpenFileDialog();
                openFileDialogResults.Filter = "Json Files (*.json)|*.JSON";

                if (openFileDialogResults.ShowDialog() == true)
                {
                    try
                    {
                        string FilePath = openFileDialogResults.FileName;
                        string DestinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSON", System.IO.Path.GetFileName(FilePath));
                        File.Copy(FilePath, DestinationPath, true);

                        FilePathJsonResults = DestinationPath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error while choosing JSON file, the program will run using SwaggerReading mode");
                FileRead = false;
                return;
            }
        }

        private async void StartApplication()
        {
            IRepoTeam repoTeam = new RepoFactoryTeam();
            LoadingProgressBar.Visibility = Visibility.Visible;
            try
            {
                List<Team> teams = new List<Team>();

                if (FileRead == false)
                {
                    teams = await repoTeam.GetTeamsAsyncSwagger(url);
                }
                else
                {
                    teams = await repoTeam.GetTeamsAsyncFile(FilePathJsonTeams);
                }
                



                if (!CheckFile())
                {
                    InitialSettings settingsWindow = new InitialSettings();
                    settingsWindow.Show();
                    this.Hide();
                }

                await FillInitialTeamsComboBoxWithTeams();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadingProgressBar.Visibility = Visibility.Hidden;
            }
        }

        private async Task FillInitialTeamsComboBoxWithTeams()
        {
            IRepoTeam repoTeam = new RepoFactoryTeam();
            LoadingProgressBar.Visibility = Visibility.Visible;
            try
            {
                List<Team> teams = new List<Team>();

                if (FileRead == false)
                {
                    teams = await repoTeam.GetTeamsAsyncSwagger(url);
                }
                else 
                {
                    teams = await repoTeam.GetTeamsAsyncFile(FilePathJsonTeams);
                }

                foreach (Team team in teams)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = $"{team.Country} ({team.Fifa_Code})";
                    cbInitialTeam.Items.Add(item);
                }

                cbInitialTeam.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadingProgressBar.Visibility = Visibility.Hidden;
            }

        }

        private bool CheckFile()
        {
            if (File.Exists(SettingsFilePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(SettingsFilePath))
                    {
                        string gender = reader.ReadLine()?.Split(':')[1]?.Trim();
                        string language = reader.ReadLine()?.Split(':')[1]?.Trim();
                        string FileReadMode = reader.ReadLine()?.Split(':')[1]?.Trim();
                        string resolution = reader.ReadLine()?.Split(':')[1]?.Trim();
                        if (!string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(language) && !string.IsNullOrEmpty(FileReadMode) && !string.IsNullOrEmpty(resolution))
                        {
                            // Проверка, что gender и language записаны корректно
                            if ((gender == "Male" || gender == "Female") && (language == "English" || language == "Croatian") && (FileReadMode == "0" || FileReadMode == "1") && (resolution == "1920x1080" || resolution == "1280x720" || resolution == "800x630"))
                            {
                                return true;
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading file: " + ex.Message);
                }
            }

            // Если файл отсутствует или содержит некорректные данные, возвращаем false
            MessageBox.Show("Settings file is invalid or not found.");
            InitialSettings settingsWindow = new InitialSettings();
            settingsWindow.Show();
            this.Hide();
            return false;
        }

        private async void cbInitialTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbInitialTeam.SelectedIndex == -1)
                return;

            IRepoCountry country = new RepoFactoryCountry();

            PlayerGrid.Children.Clear();

            // Получаем выбранный элемент ComboBox
            var selectedItem = cbInitialTeam.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedText = selectedItem.Content.ToString();
                int startIndex = selectedText.IndexOf('(');
                int endIndex = selectedText.IndexOf(')');



                if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                {
                    fifaCodeInitial = selectedText.Substring(startIndex + 1, endIndex - startIndex - 1);
                }
                else
                {
                    Console.WriteLine("Invalid format for selected item.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Selected item is null.");
                return;
            }

            
            string imagePath = ChangeCountryFlag(fifaCodeInitial);
            Uri uri = new Uri(imagePath, UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            imgInitialTeamFlag.Source = bitmapImage;

            List<Country.Root> roots = new List<Country.Root>();


                LoadingProgressBar.Visibility = Visibility.Visible;
                try
                {
                    if (FileRead == false)
                    {
                        roots = await country.GetCountryAsyncSwagger(fifaCodeInitial, url);
                    }
                    else
                    {
                        roots = await country.GetCountryAsyncFile(fifaCodeInitial, FilePathJsonCountries);
                    }
                    

                    // Очищаем элементы перед добавлением новых
                    cbOpponentTeam.Items.Clear();

                    // Добавляем данные в ComboBox
                    foreach (Country.Root root in roots)
                    {
                        if (root.away_team.code == fifaCodeInitial)
                        {
                            cbOpponentTeam.Items.Add(root.home_team.country + " (" + root.home_team.code + ")");
                        }
                        if (root.home_team.code == fifaCodeInitial)
                        {
                            cbOpponentTeam.Items.Add(root.away_team.country + " (" + root.away_team.code + ")");
                        }

                    }
                    // Устанавливаем выбранный элемент ComboBox
                    cbOpponentTeam.SelectedIndex = 0;

                    CountTheScore();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting data: {ex.Message}");
                }
                finally
                {
                    LoadingProgressBar.Visibility = Visibility.Hidden;
                }


            LoadingProgressBar.Visibility = Visibility.Visible;
            try
            {
                List<Country.StartingEleven> startingEleven = new List<Country.StartingEleven>();
                List<Country.Substitute> substitutes = new List<Country.Substitute>();
                
                foreach (Country.Root root in roots)
                {
                    if ((root.away_team.code == fifaCodeInitial || root.home_team.code == fifaCodeInitial) && (root.away_team.code == fifacodeOpponent || root.home_team.code == fifacodeOpponent))
                    {
                        if (root.away_team.code == fifaCodeInitial)
                        {
                            FillPlayersIntoLbInitialPlayersAway(root, startingEleven, substitutes);
                            break;
                        }
                        if (root.home_team.code == fifaCodeInitial)
                        {
                            FillPlayersIntoLbInitialPlayersHome(root, startingEleven, substitutes);
                            break;
                        }
                    }
                }
                foreach (Country.Root root in roots)
                {
                    if ((root.away_team.code == fifacodeOpponent || root.home_team.code == fifacodeOpponent) && (root.away_team.code == fifacodeOpponent || root.home_team.code == fifacodeOpponent))
                    {
                        if (root.away_team.code == fifacodeOpponent)
                        {
                            FillPlayersIntoLbOpponentPlayersAway(root, startingEleven, substitutes);
                            break;
                        }
                        if (root.home_team.code == fifacodeOpponent)
                        {
                            FillPlayersIntoLbOpponentPlayersHome(root, startingEleven, substitutes);
                            break;
                        }
                    }
                }

                foreach (Country.Root root in roots)
                {
                    if ((root.away_team.code == fifaCodeInitial && root.home_team.code == fifacodeOpponent) || (root.away_team.code == fifacodeOpponent &&  root.home_team.code == fifaCodeInitial))
                    {
                        FillDateTimeANDTactics(root);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadingProgressBar.Visibility = Visibility.Hidden;
            }
        }

        private string ChangeCountryFlag(string fifa)
        {
            string imagePath = "";

            switch (fifa.ToString())
            {
                case "KOR":
                    imagePath = "/Images/Flag_of_South_Korea.png";
                    break;
                case "GER":
                    imagePath = "/Images/GermanyFlag.png";
                    break;
                case "SRB":
                    imagePath = "/Images/1200px-Flag_of_Serbia.png";
                    break;
                case "CRC":
                    imagePath = "/Images/Flag_of_Costa_Rica.svg.png";
                    break;
                case "BEL":
                    imagePath = "/Images/Flag_of_Belgium.svg.png";
                    break;
                case "POL":
                    imagePath = "/Images/Flag_of_Poland.svg.png";
                    break;
                case "SEN":
                    imagePath = "/Images/Flag_of_Senegal.svg.png";
                    break;
                case "ENG":
                    imagePath = "/Images/Flag_of_Great_Britain_(1707–1800).svg.png";
                    break;
                case "FRA":
                    imagePath = "/Images/FranceFlag.jpg";
                    break;
                case "CRO":
                    imagePath = "/Images/Flag_of_Croatia.svg.png";
                    break;
                case "PAN":
                    imagePath = "/Images/Flag_of_Panama.svg.png";
                    break;
                case "TUN":
                    imagePath = "/Images/Flag_of_Tunisia.svg.png";
                    break;
                case "ARG":
                    imagePath = "/Images/Flag_of_Argentina.svg.png";
                    break;
                case "POR":
                    imagePath = "/Images/PortugalFlag.jpg";
                    break;
                case "MEX":
                    imagePath = "/Images/Mexico-Flag.jpg";
                    break;
                case "JPN":
                    imagePath = "/Images/JapanFlag.jpg";
                    break;
                case "COL":
                    imagePath = "/Images/New-Flag-of-the-President-of-Colombia.svg.png";
                    break;
                case "SUI":
                    imagePath = "/Images/Switzerland-flag.jpg";
                    break;
                case "URU":
                    imagePath = "/Images/Flag_of_Uruguay.svg.png";
                    break;
                case "BRA":
                    imagePath = "/Images/1200px-Flag_of_Brazil.svg.png";
                    break;
                case "ESP":
                    imagePath = "/Images/SpainFlag.jpg";
                    break;
                case "KSA":
                    imagePath = "/Images/Flag_of_Saudi_Arabia.svg.png";
                    break;
                case "EGY":
                    imagePath = "/Images/EgyptFlag.jpg";
                    break;
                case "MAR":
                    imagePath = "/Images/Moroccan-flag.jpg";
                    break;
                case "IRN":
                    imagePath = "/Images/Flag_of_Iran.svg.png";
                    break;
                case "SWE":
                    imagePath = "/Images/Flag_of_Sweden.svg.png";
                    break;
                case "AUS":
                    imagePath = "/Images/AustraliaFlag.jpg";
                    break;
                case "PER":
                    imagePath = "/Images/Flag_of_Peru.svg.png";
                    break;
                case "ISL":
                    imagePath = "/Images/Flag_of_Iceland.svg.png";
                    break;
                case "NGA":
                    imagePath = "/Images/Flag_of_Nigeria.svg.png";
                    break;
                case "RUS":
                    imagePath = "/Images/Flag_of_Russia.png";
                    break;
                case "DEN":
                    imagePath = "/Images/Flag_of_Denmark.svg.png";
                    break;

            }
            return imagePath;
        }

        private void FillDateTimeANDTactics(Root root)
        {
            DateTime dateTime = DateTime.Parse(root.datetime);
            lblDate.Content = dateTime.ToString("dd MMMM yyyy");
            lblTime.Content = dateTime.ToString("t");
            lblNameOfTheMatch.Content = root.stage_name;
        }

        private void FillPlayersIntoLbOpponentPlayersAway(Root root, List<StartingEleven> startingEleven, List<Substitute> substitutes)
        {
            foreach (var player in root.away_team_statistics.starting_eleven)
            {
                if (!startingEleven.Contains(player))
                {
                    startingEleven.Add(player);
                }
            }

            foreach (var player in root.away_team_statistics.substitutes)
            {
                if (!substitutes.Contains(player))
                {
                    substitutes.Add(player);
                }
            }

            lbOpponentTeamBench.Items.Clear();
            lbOpponentTeamLineup.Items.Clear();

            double rightMarginDefender = 0;
            double rightMarginMidfield = 0;
            double rightMarginForward = 0;
            int dcounter = 0;
            int mcounter = 0;
            int fcounter = 0;
            double totalWidth = PlayerGrid.ActualWidth;

            foreach (var item in startingEleven)
            {
                if (item.position == "Midfield")
                {
                    mcounter++;
                }
                if (item.position == "Defender")
                {
                    dcounter++;
                }
                if (item.position == "Forward")
                {
                    fcounter++;
                }
            }
            double standardMarginDefender = (totalWidth / dcounter - FontSize / 2) / 2;
            double standardMarginMidfield = (totalWidth / mcounter - FontSize / 2) / 2;
            double standardMarginForward = (totalWidth / fcounter - FontSize / 2) / 2;
            foreach (var player in startingEleven)
            {
                // Создаём изображение
                Image img = new Image();
                img.Width = 30; // Задайте нужные размеры изображения
                img.Height = 30;

                Uri uri = new Uri("/Images/UnknownPlayer.png", UriKind.Relative);
                BitmapImage bitmapImage = new BitmapImage(uri);
                img.Source = bitmapImage;

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
                    else
                    {
                        img.Source = bitmapImage;
                    }
                }

                // Создаём текст
                TextBlock lb = new TextBlock
                {
                    Text = $"{player.shirt_number}",
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Создаём StackPanel и добавляем в него изображение и текст
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 45 // Достаточная ширина для картинки и текста
                };
                stackPanel.Children.Add(img);
                stackPanel.Children.Add(lb);

                // Создаём ListViewItem и добавляем в него StackPanel
                ListViewItem listViewItem = new ListViewItem
                {
                    Content = stackPanel
                };

                lbOpponentTeamLineup.Items.Add(player.name + " ," + player.shirt_number);

                Thickness margin = new Thickness();

                if (player.position == "Goalie")
                {
                    Grid.SetRow(listViewItem, 8);
                    margin = new Thickness(totalWidth / 2 - 30, 0, 0, 10);
                }
                else if (player.position == "Defender")
                {
                    Grid.SetRow(listViewItem, 7);
                    if (dcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - FontSize / 2, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginDefender, 0, 0, 0);
                        rightMarginDefender += totalWidth / dcounter;
                        if (dcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginDefender == totalWidth / dcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginDefender / 2; // Добавляем половину стандартного отступа
                                rightMarginDefender += standardMarginDefender / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }
                else if (player.position == "Midfield")
                {
                    Grid.SetRow(listViewItem, 6);
                    if (mcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - FontSize / 2, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginMidfield, 0, 0, 0);
                        rightMarginMidfield += totalWidth / mcounter;
                        if (mcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginMidfield == totalWidth / mcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginMidfield / 2; // Добавляем половину стандартного отступа
                                rightMarginMidfield += standardMarginMidfield / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }
                else if (player.position == "Forward")
                {
                    Grid.SetRow(listViewItem, 5);
                    if (fcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - 30, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginForward, 0, 0, 0);
                        rightMarginForward += totalWidth / fcounter;
                        if (fcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginForward == totalWidth / fcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginForward / 2; // Добавляем половину стандартного отступа
                                rightMarginForward += standardMarginForward / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }

                listViewItem.Margin = margin; // Устанавливаем Margin для текущего элемента
                PlayerGrid.Children.Add(listViewItem);
            }

            foreach (var player in substitutes)
            {
                lbOpponentTeamBench.Items.Add(player.name + " ," + player.shirt_number);
            }
            startingEleven.Clear();
            substitutes.Clear();

            lblTacticsOpposit.Content = root.away_team_statistics.tactics;
            lblOpponentTeamName.Content = root.away_team.country;
        }




        private void FillPlayersIntoLbOpponentPlayersHome(Root root, List<StartingEleven> startingEleven, List<Substitute> substitutes)
        {
            foreach (var player in root.home_team_statistics.starting_eleven)
            {
                if (!startingEleven.Contains(player))
                {
                    startingEleven.Add(player);
                }
            }

            foreach (var player in root.home_team_statistics.substitutes)
            {
                if (!substitutes.Contains(player))
                {
                    substitutes.Add(player);
                }
            }

            lbOpponentTeamLineup.Items.Clear();
            lbOpponentTeamBench.Items.Clear();


            double rightMarginDefender = 0;
            double rightMarginMidfield = 0;
            double rightMarginForward = 0;
            int dcounter = 0;
            int mcounter = 0;
            int fcounter = 0;
            double totalWidth = PlayerGrid.ActualWidth;

            foreach (var item in startingEleven)
            {
                if (item.position == "Midfield")
                {
                    mcounter++;
                }
                if (item.position == "Defender")
                {
                    dcounter++;
                }
                if (item.position == "Forward")
                {
                    fcounter++;
                }
            }
            double standardMarginDefender = (totalWidth / dcounter - FontSize / 2) / 2;
            double standardMarginMidfield = (totalWidth / mcounter - FontSize / 2) / 2;
            double standardMarginForward = (totalWidth / fcounter - FontSize / 2) / 2;
            foreach (var player in startingEleven)
            {
                // Создаём изображение
                Image img = new Image();
                img.Width = 30; // Задайте нужные размеры изображения
                img.Height = 30;
                Uri uri = new Uri("/Images/UnknownPlayer.png", UriKind.Relative);
                BitmapImage bitmapImage = new BitmapImage(uri);
                img.Source = bitmapImage;
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
                    else
                    {
                        img.Source = bitmapImage;
                    }
                }

                // Создаём текст
                TextBlock lb = new TextBlock
                {
                    Text = $"{player.shirt_number}",
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Создаём StackPanel и добавляем в него изображение и текст
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 45 // Достаточная ширина для картинки и текста
                };
                stackPanel.Children.Add(img);
                stackPanel.Children.Add(lb);

                // Создаём ListViewItem и добавляем в него StackPanel
                ListViewItem listViewItem = new ListViewItem
                {
                    Content = stackPanel
                };

                lbOpponentTeamLineup.Items.Add(player.name + " ," + player.shirt_number);

                Thickness margin = new Thickness();

                if (player.position == "Goalie")
                {
                    Grid.SetRow(listViewItem, 0);
                    margin = new Thickness(totalWidth / 2 - 30, 0, 0, 10);
                }
                else if (player.position == "Defender")
                {
                    Grid.SetRow(listViewItem, 1);
                    if (dcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - FontSize / 2, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginDefender, 0, 0, 0);
                        rightMarginDefender += totalWidth / dcounter;
                        if (dcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginDefender == totalWidth / dcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginDefender / 2; // Добавляем половину стандартного отступа
                                rightMarginDefender += standardMarginDefender / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }
                else if (player.position == "Midfield")
                {
                    Grid.SetRow(listViewItem, 2);
                    if (mcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - FontSize / 2, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginMidfield, 0, 0, 0);
                        rightMarginMidfield += totalWidth / mcounter;
                        if (mcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginMidfield == totalWidth / mcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginMidfield / 2; // Добавляем половину стандартного отступа
                                rightMarginMidfield += standardMarginMidfield / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }
                else if (player.position == "Forward")
                {
                    Grid.SetRow(listViewItem, 3);
                    if (fcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - 30, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginForward, 0, 0, 0);
                        rightMarginForward += totalWidth / fcounter;
                        if (fcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginForward == totalWidth / fcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginForward / 2; // Добавляем половину стандартного отступа
                                rightMarginForward += standardMarginForward / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }

                listViewItem.Margin = margin; // Устанавливаем Margin для текущего элемента
                PlayerGrid.Children.Add(listViewItem);
            }

            foreach (var player in substitutes)
            {
                lbOpponentTeamBench.Items.Add(player.name + " ," + player.shirt_number);
            }
            startingEleven.Clear();
            substitutes.Clear();

            lblTacticsOpposit.Content = root.home_team_statistics.tactics;
            lblOpponentTeamName.Content = root.home_team.country;
        }

        private void FillPlayersIntoLbInitialPlayersHome(Root root, List<StartingEleven> startingEleven, List<Substitute> substitutes)
        {
            foreach (var player in root.home_team_statistics.starting_eleven)
            {
                if (!startingEleven.Contains(player))
                {
                    startingEleven.Add(player);
                }
            }

            foreach (var player in root.home_team_statistics.substitutes)
            {
                if (!substitutes.Contains(player))
                {
                    substitutes.Add(player);
                }
            }

            lbInitialTeamBench.Items.Clear();
            lbInitialTeamLineup.Items.Clear();


            double rightMarginDefender = 0;
            double rightMarginMidfield = 0;
            double rightMarginForward = 0;
            int dcounter = 0;
            int mcounter = 0;
            int fcounter = 0;
            double totalWidth = PlayerGrid.ActualWidth;

            foreach (var item in startingEleven)
            {
                if (item.position == "Midfield")
                {
                    mcounter++;
                }
                if (item.position == "Defender")
                {
                    dcounter++;
                }
                if (item.position == "Forward")
                {
                    fcounter++;
                }
            }
            double standardMarginDefender = (totalWidth / dcounter - FontSize / 2) / 2;
            double standardMarginMidfield = (totalWidth / mcounter - FontSize / 2) / 2;
            double standardMarginForward = (totalWidth / fcounter - FontSize / 2) / 2;
            foreach (var player in startingEleven)
            {
                // Создаём изображение
                Image img = new Image();
                img.Width = 30; // Задайте нужные размеры изображения
                img.Height = 30;
                Uri uri = new Uri("/Images/UnknownPlayer.png", UriKind.Relative);
                BitmapImage bitmapImage = new BitmapImage(uri);
                img.Source = bitmapImage;
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
                    else
                    {
                        img.Source = bitmapImage;
                    }
                }

                // Создаём текст
                TextBlock lb = new TextBlock
                {
                    Text = $"{player.shirt_number}",
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Создаём StackPanel и добавляем в него изображение и текст
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 45 // Достаточная ширина для картинки и текста
                };
                stackPanel.Children.Add(img);
                stackPanel.Children.Add(lb);

                // Создаём ListViewItem и добавляем в него StackPanel
                ListViewItem listViewItem = new ListViewItem
                {
                    Content = stackPanel
                };

                lbInitialTeamLineup.Items.Add(player.name + " ," + player.shirt_number);

                Thickness margin = new Thickness();

                if (player.position == "Goalie")
                {
                    Grid.SetRow(listViewItem, 0);
                    margin = new Thickness(totalWidth / 2 - 30, 0, 0, 10);
                }
                else if (player.position == "Defender")
                {
                    Grid.SetRow(listViewItem, 1);
                    if (dcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - FontSize / 2, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginDefender, 0, 0, 0);
                        rightMarginDefender += totalWidth / dcounter;
                        if (dcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginDefender == totalWidth / dcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginDefender / 2; // Добавляем половину стандартного отступа
                                rightMarginDefender += standardMarginDefender / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }
                else if (player.position == "Midfield")
                {
                    Grid.SetRow(listViewItem, 2);
                    if (mcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - FontSize / 2, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginMidfield, 0, 0, 0);
                        rightMarginMidfield += totalWidth / mcounter;
                        if (mcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginMidfield == totalWidth / mcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginMidfield / 2; // Добавляем половину стандартного отступа
                                rightMarginMidfield += standardMarginMidfield / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }
                else if (player.position == "Forward")
                {
                    Grid.SetRow(listViewItem, 3);
                    if (fcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - FontSize / 2, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginForward, 0, 0, 0);
                        rightMarginForward += totalWidth / fcounter;
                        if (fcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginForward == totalWidth / fcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginForward / 2; // Добавляем половину стандартного отступа
                                rightMarginForward += standardMarginForward / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }

                listViewItem.Margin = margin; // Устанавливаем Margin для текущего элемента
                PlayerGrid.Children.Add(listViewItem);
            }

            foreach (var player in substitutes)
            {
                lbInitialTeamBench.Items.Add(player.name + " ," + player.shirt_number);
            }
            startingEleven.Clear();
            substitutes.Clear();

            lblTacticsInitial.Content = root.home_team_statistics.tactics;
            lblInitialTeamName.Content = root.home_team.country;
        }

        private void FillPlayersIntoLbInitialPlayersAway(Root root, List<StartingEleven> startingEleven, List<Substitute> substitutes)
        {
            foreach (var player in root.away_team_statistics.starting_eleven)
            {
                if (!startingEleven.Contains(player))
                {
                    startingEleven.Add(player);
                }
            }

            foreach (var player in root.away_team_statistics.substitutes)
            {
                if (!substitutes.Contains(player))
                {
                    substitutes.Add(player);
                }
            }

            lbInitialTeamBench.Items.Clear();
            lbInitialTeamLineup.Items.Clear();


            double rightMarginDefender = 0;
            double rightMarginMidfield = 0;
            double rightMarginForward = 0;
            int dcounter = 0;
            int mcounter = 0;
            int fcounter = 0;
            double totalWidth = PlayerGrid.ActualWidth;

            foreach (var item in startingEleven)
            {
                if (item.position == "Midfield")
                {
                    mcounter++;
                }
                if (item.position == "Defender")
                {
                    dcounter++;
                }
                if (item.position == "Forward")
                {
                    fcounter++;
                }
            }
            double standardMarginDefender = (totalWidth / dcounter - FontSize / 2) / 2;
            double standardMarginMidfield = (totalWidth / mcounter - FontSize / 2) / 2;
            double standardMarginForward = (totalWidth / fcounter - FontSize / 2) / 2;
            foreach (var player in startingEleven)
            {
                // Создаём изображение
                Image img = new Image();
                img.Width = 30; // Задайте нужные размеры изображения
                img.Height = 30;
                Uri uri = new Uri("/Images/UnknownPlayer.png", UriKind.Relative);
                BitmapImage bitmapImage = new BitmapImage(uri);
                img.Source = bitmapImage;
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
                    else
                    {
                        img.Source = bitmapImage;
                    }
                }

                // Создаём текст
                TextBlock lb = new TextBlock
                {
                    Text = $"{player.shirt_number}",
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Создаём StackPanel и добавляем в него изображение и текст
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 45 // Достаточная ширина для картинки и текста
                };
                stackPanel.Children.Add(img);
                stackPanel.Children.Add(lb);

                // Создаём ListViewItem и добавляем в него StackPanel
                ListViewItem listViewItem = new ListViewItem
                {
                    Content = stackPanel
                };

                lbInitialTeamLineup.Items.Add(player.name + " ," + player.shirt_number);

                Thickness margin = new Thickness();

                if (player.position == "Goalie")
                {
                    Grid.SetRow(listViewItem, 8);
                    margin = new Thickness(totalWidth / 2 - 30, 0, 0, 10);
                }
                else if (player.position == "Defender")
                {
                    Grid.SetRow(listViewItem, 7);
                    if (dcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - FontSize / 2, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginDefender, 0, 0, 0);
                        rightMarginDefender += totalWidth / dcounter;
                        if (dcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginDefender == totalWidth / dcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginDefender / 2; // Добавляем половину стандартного отступа
                                rightMarginDefender += standardMarginDefender / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }
                else if (player.position == "Midfield")
                {
                    Grid.SetRow(listViewItem, 6);
                    if (mcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - FontSize / 2, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginMidfield, 0, 0, 0);
                        rightMarginMidfield += totalWidth / mcounter;
                        if (mcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginMidfield == totalWidth / mcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginMidfield / 2; // Добавляем половину стандартного отступа
                                rightMarginMidfield += standardMarginMidfield / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }
                else if (player.position == "Forward")
                {
                    Grid.SetRow(listViewItem, 5);
                    if (fcounter <= 1)
                    {
                        margin = new Thickness(totalWidth / 2 - 30, 0, 0, 0);
                    }
                    else
                    {
                        margin = new Thickness(rightMarginForward, 0, 0, 0);
                        rightMarginForward += totalWidth / fcounter;
                        if (fcounter > 1) // Если в строке больше одного элемента
                        {
                            if (rightMarginForward == totalWidth / fcounter) // Проверяем, является ли элемент первым в строке
                            {
                                margin.Left += standardMarginForward / 2; // Добавляем половину стандартного отступа
                                rightMarginForward += standardMarginForward / 2; // Увеличиваем отступ
                            }
                        }
                    }
                }

                listViewItem.Margin = margin; // Устанавливаем Margin для текущего элемента
                PlayerGrid.Children.Add(listViewItem);
            }

            foreach (var player in substitutes)
            {
                lbInitialTeamBench.Items.Add(player.name + " ," + player.shirt_number);
            }
            startingEleven.Clear();
            substitutes.Clear();

            lblTacticsInitial.Content = root.away_team_statistics.tactics;
            lblInitialTeamName.Content = root.away_team.country;
        }

        private async void cbOpponentTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbOpponentTeam.SelectedIndex == -1)
                return;

            PlayerGrid.Children.Clear();
            CountTheScore();

            IRepoCountry country = new RepoFactoryCountry();
            List<Country.Root> roots = new List<Country.Root>();

            LoadingProgressBar.Visibility = Visibility.Visible;
            try
            {
                if (FileRead == false)
                {
                    roots = await country.GetCountryAsyncSwagger(fifaCodeInitial, url);
                }
                else
                {
                    roots = await country.GetCountryAsyncFile(fifaCodeInitial, FilePathJsonCountries);
                }
                
                List<Country.StartingEleven> startingEleven = new List<Country.StartingEleven>();
                List<Country.Substitute> substitutes = new List<Country.Substitute>();

                foreach (Country.Root root in roots)
                {
                    if ((root.away_team.code == fifaCodeInitial || root.home_team.code == fifaCodeInitial) && (root.away_team.code == fifacodeOpponent || root.home_team.code == fifacodeOpponent))
                    {
                        if (root.away_team.code == fifaCodeInitial)
                        {
                            FillPlayersIntoLbInitialPlayersAway(root, startingEleven, substitutes);
                            break;
                        }
                        if (root.home_team.code == fifaCodeInitial)
                        {
                            FillPlayersIntoLbInitialPlayersHome(root, startingEleven, substitutes);
                            break;
                        }
                    }
                }
                foreach (Country.Root root in roots)
                {
                    if ((root.away_team.code == fifacodeOpponent || root.home_team.code == fifacodeOpponent) && (root.away_team.code == fifaCodeInitial || root.home_team.code == fifaCodeInitial))
                    {
                        if (root.away_team.code == fifacodeOpponent)
                        {
                            FillPlayersIntoLbOpponentPlayersAway(root, startingEleven, substitutes);
                            break;
                        }
                        if (root.home_team.code == fifacodeOpponent)
                        {
                            FillPlayersIntoLbOpponentPlayersHome(root, startingEleven, substitutes);
                            break;
                        }
                    }
                }

                foreach (Country.Root root in roots)
                {
                    if ((root.away_team.code == fifaCodeInitial && root.home_team.code == fifacodeOpponent) || (root.away_team.code == fifacodeOpponent && root.home_team.code == fifaCodeInitial))
                    {
                        FillDateTimeANDTactics(root);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadingProgressBar.Visibility = Visibility.Hidden;
            }

            var selectedItem = cbOpponentTeam.SelectedItem as string;
            if (selectedItem != null)
            {
                int startIndex = selectedItem.IndexOf('(');
                int endIndex = selectedItem.IndexOf(')');

                if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                {
                    fifacodeOpponent = selectedItem.Substring(startIndex + 1, endIndex - startIndex - 1);
                }
                else
                {
                    Console.WriteLine("Invalid format for selected item.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Selected item is null.");
                return;
            }


            string imagePath = ChangeCountryFlag(fifacodeOpponent);
            Uri uri = new Uri(imagePath, UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            imgOpponentTeamFlag.Source = bitmapImage;
        }

        private async void CountTheScore()
        {
            LoadingProgressBar.Visibility = Visibility.Visible;
            try
            {
                IRepoCountry country = new RepoFactoryCountry();

                string selectedTextOpponent = cbOpponentTeam.SelectedValue.ToString();
                int startIndexOpponent = selectedTextOpponent.IndexOf('(');
                int endIndexOpponent = selectedTextOpponent.IndexOf(')');

                fifacodeOpponent = selectedTextOpponent.Substring(startIndexOpponent + 1, endIndexOpponent - startIndexOpponent - 1);
                List<Country.Root> roots = new List<Country.Root>();

                if (FileRead == false)
                {
                    roots = await country.GetCountryAsyncSwagger(fifaCodeInitial, url);
                }
                else
                {
                    roots = await country.GetCountryAsyncFile(fifaCodeInitial, FilePathJsonCountries);
                }

                foreach (Country.Root root in roots)
                {
                    if ((root.away_team.code == fifaCodeInitial && root.home_team.code == fifacodeOpponent) || (root.away_team.code == fifacodeOpponent && root.home_team.code == fifaCodeInitial))
                    {
                        if (root.home_team.code == fifaCodeInitial)
                        {
                            lblScore.Content = root.home_team.goals + " : " + root.away_team.goals;
                            break;
                        }
                        else
                        {
                            lblScore.Content = root.away_team.goals + " : " + root.home_team.goals;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadingProgressBar.Visibility = Visibility.Hidden;
            }
        }

        private void btnInitialTeamStats_Click(object sender, RoutedEventArgs e)
        {
            TeamStatistics teamStatistics = new TeamStatistics(fifaCodeInitial, url, FilePathJsonResults, FileRead);
            teamStatistics.Show();
        }

        private void btnOpponentTeamStats_Click(object sender, RoutedEventArgs e)
        {
            TeamStatistics teamStatistics = new TeamStatistics(fifacodeOpponent, url, FilePathJsonResults, FileRead);
            teamStatistics.Show();
        }

        private async void lbInitialTeamLineup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbInitialTeamLineup.SelectedItem == null)
                return;

            string selectedPlayer = lbInitialTeamLineup.SelectedItem.ToString().Split(',')[0].Trim();

            IRepoCountry country = new RepoFactoryCountry();
            LoadingProgressBar.Visibility = Visibility.Visible;
            try
            {
                List<Country.Root> roots = new List<Country.Root>();
                if (FileRead == false)
                {
                    roots = await country.GetCountryAsyncSwagger(fifaCodeInitial, url);
                }
                else
                {
                    roots = await country.GetCountryAsyncFile(fifaCodeInitial, FilePathJsonCountries);
                }
                
                foreach (Country.Root root in roots)
                {
                    if ((root.away_team.code == fifaCodeInitial || root.home_team.code == fifaCodeInitial) && (root.away_team.code == fifacodeOpponent || root.home_team.code == fifacodeOpponent))
                    {
                        PlayerOverview playerOverview = new PlayerOverview(root, selectedPlayer);
                        playerOverview.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
            finally
            {
                LoadingProgressBar.Visibility = Visibility.Hidden;
            }
        }

        private async void lbOpponentTeamLineup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbOpponentTeamLineup.SelectedItem == null)
                return;

            string selectedPlayer = lbOpponentTeamLineup.SelectedItem.ToString().Split(',')[0].Trim();

            IRepoCountry country = new RepoFactoryCountry();
            LoadingProgressBar.Visibility = Visibility.Visible;
            try
            {
                List<Country.Root> roots = new List<Country.Root>();
                if (FileRead == false)
                {
                    roots = await country.GetCountryAsyncSwagger(fifacodeOpponent, url);
                }
                else
                {
                    roots = await country.GetCountryAsyncFile(fifaCodeInitial, FilePathJsonCountries);
                }

                foreach (Country.Root root in roots)
                {
                    if ((root.away_team.code == fifaCodeInitial || root.home_team.code == fifaCodeInitial) && (root.away_team.code == fifacodeOpponent || root.home_team.code == fifacodeOpponent))
                    {
                        PlayerOverview playerOverview = new PlayerOverview(root, selectedPlayer);
                        playerOverview.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
            finally
            {
                LoadingProgressBar.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Проверяем, были ли сохранены настройки
            if (!isSettingsSaved)
            {
                // Если настройки не были сохранены, вызываем окно подтверждения выхода
                MessageBoxResult result = ShowExitConfirmation();
                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    e.Cancel = true; // Отменяем закрытие окна, если пользователь отказался выходить
                }
            }
            else
            {
                // Сбрасываем флаг, так как настройки уже сохранены
                isSettingsSaved = false;
            }
        }

        private MessageBoxResult ShowExitConfirmation()
        {
            MessageBoxResult result = MessageBoxResult.None;

            // Создаем новое окно для подтверждения выхода
            Window confirmationWindow = new Window
            {
                Title = "Exit Confirmation",
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.ToolWindow,
                ResizeMode = ResizeMode.NoResize,   
            };

            // Создаем кнопки
            StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };
            Button yesButton = new Button { Content = "Yes", Margin = new Thickness(5), MinWidth = 75 };
            Button noButton = new Button { Content = "No", Margin = new Thickness(5), MinWidth = 75 };
            stackPanel.Children.Add(yesButton);
            stackPanel.Children.Add(noButton);

            // Добавляем кнопки к содержимому окна
            DockPanel dockPanel = new DockPanel();
            dockPanel.Children.Add(new TextBlock { Text = "Are you sure you want to exit the program?", TextAlignment = TextAlignment.Center, Margin = new Thickness(10) });
            dockPanel.Children.Add(stackPanel);
            DockPanel.SetDock(stackPanel, Dock.Bottom);
            confirmationWindow.Content = dockPanel;

            // Обработка нажатий кнопок
            yesButton.Click += (s, e) => { result = MessageBoxResult.Yes; confirmationWindow.Close(); };
            noButton.Click += (s, e) => { result = MessageBoxResult.No; confirmationWindow.Close(); };

            // Обработка нажатия клавиши Escape и Enter
            confirmationWindow.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    result = MessageBoxResult.No;
                    confirmationWindow.Close();
                }
                else if (e.Key == Key.Enter)
                {
                    result = MessageBoxResult.Yes;
                    confirmationWindow.Close();
                }
            };

            // Показываем окно подтверждения как диалог
            confirmationWindow.ShowDialog();

            return result;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            InitialSettings initialSettings = new InitialSettings();
            initialSettings.Owner = this; // Установите владельцем текущее окно
            bool? result = initialSettings.ShowDialog();

            // Закрываем предыдущее окно, если настройки были успешно сохранены
            if (result == true)
            {
                isSettingsSaved = true;
                this.Close();
            }
        }
    }
}
