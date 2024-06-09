    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.IO;
    using OOPNET_Project_Assignment.Moduls;
    using OOPNET_Project_Assignment.DataLayer;
    using static System.Windows.Forms.LinkLabel;
    using System.Text.RegularExpressions;
    using iText.Kernel.Pdf;
    using iText.Layout.Element;
    using iText.Layout;
    using iText.IO.Image;
    
    using static OOPNET_Project_Assignment.Application;
    using static OOPNET_Project_Assignment.Moduls.Country;
    using DataLayer;

namespace OOPNET_Project_Assignment
    {

        public partial class Application : Form
        {
            private static readonly string relativeSettingsPath = @"DataLayer\settings.txt";

            private static readonly string SettingsFilePath = SettingsManager.GetSettingsPath(relativeSettingsPath);

            private static readonly string relativePlayerImagePath = @"DataLayer\PlayerImage.txt";

            private static readonly string PlayerImagePath = SettingsManager.GetSettingsPath(relativePlayerImagePath);

            private static readonly string FilePathOnLoad = "onload.txt";

            private static string FilePathJsonCountries = "JSON\\Matches\\Country\\response_1716882004474.json";

            private static string FilePathJsonTeams = "JSON\\Teams\\response_1716882020462.json";

            private static readonly string FilePathFavouritePlayers = "FavouritePlayers.txt";

            private static string url;

            ContextMenuStrip ContextMenuPlayers = new ContextMenuStrip();

            ContextMenuStrip ContextMenuFavouritePlayers = new ContextMenuStrip();

            private static string fifaCode;

            private ImageList imageList = new ImageList();

            private static bool FileRead = false;

            public Application()
            {
                InitializeComponent();

                
            if (CheckFile())
                {
               
                    try
                    {
                        using (StreamReader reader = new StreamReader(SettingsFilePath))
                        {
                            string gender = reader.ReadLine()?.Split(':')[1]?.Trim();
                            string language = reader.ReadLine()?.Split(':')[1]?.Trim();
                            string FileReadMode = reader.ReadLine()?.Split(':')[1]?.Trim();
                        if (!string.IsNullOrEmpty(FileReadMode))
                        {
                            if (FileReadMode == "0")
                            {
                                FileRead = false;
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
                            }
                            else if (FileReadMode == "1")
                            {
                                FileRead = true;
                                ChoosePathToJson();
                            }
                            else
                            {
                                MessageBox.Show("Error while reading file read mode, in this case file read mode assigned as false");
                                FileRead = false;
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
                            }
                        }
                       
                            
                    }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading file: " + ex.Message);
                    }
                }

                CheckLanguage();

                StartApplication();

                InitializeDataGridViewsPlayers();

                InitializeDataGridViewsMatches();

                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "UnknownPlayer.png");
                imageList.ImageSize = new Size(50, 50);
                imageList.Images.Add(System.Drawing.Image.FromFile(imagePath));
                lvPlayers.LargeImageList = imageList;
                lvFavouritePlayers.LargeImageList = imageList;

                ContextMenuPlayers.Items.Add("Add to favourite", null, AddToFavourite);
                ContextMenuPlayers.Items.Add("Change photo", null, delegate { ChangePhoto(lvPlayers); });
                ContextMenuPlayers.Items.Add("Reset photo", null, delegate { ResetPhoto(lvPlayers); });

                lvPlayers.ContextMenuStrip = ContextMenuPlayers;

                ContextMenuFavouritePlayers.Items.Add("Remove from favourite", null, RemoveFromFavourite);
                ContextMenuFavouritePlayers.Items.Add("Change photo", null, delegate { ChangePhoto(lvFavouritePlayers); } );
                ContextMenuFavouritePlayers.Items.Add("Reset photo", null, delegate { ResetPhoto(lvFavouritePlayers); });

                lvFavouritePlayers.ContextMenuStrip = ContextMenuFavouritePlayers;

                ContextMenuFavouritePlayers.Opening += new System.ComponentModel.CancelEventHandler(OnContextMenuOpeningFavouritePlayers);

                ContextMenuPlayers.Opening += new System.ComponentModel.CancelEventHandler(OnContextMenuOpeningPlayers);

                lvFavouritePlayers.MouseDown += new MouseEventHandler(OnEmptySpaceClickFavouritePlayers);

                lvPlayers.MouseDown += new MouseEventHandler(OnEmptySpaceClickPlayers);

                lvPlayers.ItemDrag += new ItemDragEventHandler(ListViewItemDrag);
                lvPlayers.DragEnter += new DragEventHandler(ListViewDragEnter);
                lvPlayers.DragDrop += new DragEventHandler(ListViewDragDropRemoveFavourite);

                lvFavouritePlayers.ItemDrag += new ItemDragEventHandler(ListViewItemDrag);
                lvFavouritePlayers.DragEnter += new DragEventHandler(ListViewDragEnter);
                lvFavouritePlayers.DragDrop += new DragEventHandler(ListViewDragDropAddFavourite);



                List<string> FavouritePlayersStartProgram = new List<string>(File.ReadAllLines(FilePathFavouritePlayers));

                List<string> FavouritePlayersStartProgramImages = new List<string>(File.ReadAllLines(PlayerImagePath));

            foreach (string item in FavouritePlayersStartProgram)
            {
                // Разбор строки элемента, чтобы извлечь имя игрока
                int endIndex = item.Length - item.IndexOf("Country:");
                ListViewItem listViewItem = new ListViewItem(item.Substring(0, item.Length - endIndex - 1) + " " + star);

                int nameStartIndex = item.IndexOf("Name: ") + "Name: ".Length;
                int nameEndIndex = item.IndexOf(',', nameStartIndex); // Находим конец имени (запятая)
                string playerName = item.Substring(nameStartIndex, nameEndIndex - nameStartIndex).Trim();


                listViewItem.ImageIndex = 0;
                foreach (string line in FavouritePlayersStartProgramImages)
                {
                    // Разделяем строку на имя игрока и путь к изображению
                    string[] parts = line.Split('=');
                    if (parts.Length < 2) continue; // Пропустить строки, если они неправильно отформатированы

                    string imageName = parts[0].Trim();
                    string imagePathI = line.Split('=')[1].Trim();

                    if (playerName == imageName)
                    {
                        // Проверка, если изображение уже добавлено в ImageList
                        if (!imageList.Images.ContainsKey(imageName))
                        {
                            // Добавляем изображение в ImageList с использованием имени игрока в качестве ключа
                            imageList.Images.Add(imageName, System.Drawing.Image.FromFile(imagePathI));
                        }
                        // Устанавливаем ImageKey для элемента списка
                        listViewItem.ImageKey = imageName;
                        break; // Выход из внутреннего цикла после нахождения соответствующего изображения
                    }
                    else
                    {
                        listViewItem.ImageIndex = 0;
                    }
                }

                // Добавление элемента в ListView
                lvFavouritePlayers.Items.Add(listViewItem);
            }




            lblPlayers.Text = $"{lvFavouritePlayers.Items.Count} / 3";
            }

        private void ChoosePathToJson()
        {
            try
            {
                MessageBox.Show("Choose JSON file to get list of all teams in world cup");
                OpenFileDialog openFileDialogTeams = new OpenFileDialog();
                openFileDialogTeams.Filter = "Json Files (*.json)|*.JSON";

                if (openFileDialogTeams.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string FilePath = openFileDialogTeams.FileName;
                        string DestinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSON", Path.GetFileName(FilePath));
                        File.Copy(FilePath, DestinationPath, true);

                        FilePathJsonTeams = DestinationPath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                MessageBox.Show("Choose JSON file to get matches for any country, by entering their FIFA Code");
                OpenFileDialog openFileDialogCountry = new OpenFileDialog();
                openFileDialogCountry.Filter = "Json Files (*.json)|*.JSON";

                if (openFileDialogCountry.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string FilePath = openFileDialogCountry.FileName;
                        string DestinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSON", Path.GetFileName(FilePath));
                        File.Copy(FilePath, DestinationPath, true);

                        FilePathJsonCountries = DestinationPath;
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
                            lblChooseNationalTeam.Text = "Choose your favourite national team:";
                            btnPrintRankingList.Text = "Print the ranking lists";
                            btnSettings.Text = "Change settings";
                            btnCreateRListOfPlayers.Text = "Create ranking list of players";
                            btnCreateRListOfMatches.Text = "Create ranking list of matches";
                        }
                        else if (language == "Croatian")
                        {
                            lblChooseNationalTeam.Text = "Odaberi svoju omiljenu reprezentaciju:";
                            btnPrintRankingList.Text = "Ispiši rang liste";
                            btnSettings.Text = "Promijeniti postavke";
                            btnCreateRListOfPlayers.Text = "Napravite rang listu igrača";
                            btnCreateRListOfMatches.Text = "Napravite rang listu utakmica";
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

        private void InitializeDataGridViewsMatches()
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
                            dgvRankingListMatch.Columns.Add("Id", "Id");
                            dgvRankingListMatch.Columns.Add("Location", "Location");
                            dgvRankingListMatch.Columns.Add("Number of visitors", "Number of visitors");
                            dgvRankingListMatch.Columns.Add("Host team", "Host team");
                            dgvRankingListMatch.Columns.Add("Guest team", "Guest team");
                        }
                        else if (language == "Croatian")
                        {
                            dgvRankingListMatch.Columns.Add("Iskaznica", "Iskaznica");
                            dgvRankingListMatch.Columns.Add("Mjesto", "Mjesto");
                            dgvRankingListMatch.Columns.Add("Broj posjetitelja", "Broj posjetitelja");
                            dgvRankingListMatch.Columns.Add("Ekipa domaćina", "Ekipa domaćina");
                            dgvRankingListMatch.Columns.Add("Gostujuća ekipa", "Gostujuća ekipa");
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

        private void InitializeDataGridViewsPlayers()
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
                            // Добавляем колонки
                            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                            imgColumn.Name = "Image";
                            imgColumn.HeaderText = "Image";
                            imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            dgvRankingListPlayers.Columns.Add(imgColumn);


                            dgvRankingListPlayers.Columns.Add("Name", "Name");
                            dgvRankingListPlayers.Columns.Add("Goals", "Goals");
                            dgvRankingListPlayers.Columns.Add("Yellow Cards", "Yellow Cards");
                            dgvRankingListPlayers.Columns.Add("Appearances", "Appearances");
                        }
                        else if (language == "Croatian")
                        {
                            // Добавляем колонки
                            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                            imgColumn.Name = "Slika";
                            imgColumn.HeaderText = "Slika";
                            imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            dgvRankingListPlayers.Columns.Add(imgColumn);


                            dgvRankingListPlayers.Columns.Add("Ime", "Ime");
                            dgvRankingListPlayers.Columns.Add("Ciljevi", "Ciljevi");
                            dgvRankingListPlayers.Columns.Add("Žuti kartoni", "Žuti kartoni");
                            dgvRankingListPlayers.Columns.Add("Pojave", "Pojave");
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

        private async void FillRankingListPlayers()
        {

            dgvRankingListPlayers.Rows.Clear();

            IRepoCountry country = new RepoFactoryCountry();
            int startIndex = cbTeam.Text.IndexOf('(');
            int endIndex = cbTeam.Text.IndexOf(')');
            fifaCode = cbTeam.Text.Substring(startIndex + 1, endIndex - startIndex - 1);
            List<Country.Root> roots = new List<Country.Root>();

            Dictionary<string, PlayerStats> playerStats = new Dictionary<string, PlayerStats>();

            if (FileRead == false)
            {
                roots = await country.GetCountryAsyncSwagger(fifaCode, url);

                foreach (Country.Root root in roots)
                {
                    FillPlayersIntoRankingListPlayers(root, playerStats);
                }
            }
            else
            {
                roots = await country.GetCountryAsyncFile(fifaCode, FilePathJsonCountries);


                foreach (Country.Root root in roots)
                {
                    if (root.home_team.country == cbTeam.Text.Split('(')[0].Trim() || root.away_team.country == cbTeam.Text.Split('(')[0].Trim())
                    {
                        FillPlayersIntoRankingListPlayers(root, playerStats);
                    }
                }
            }

            foreach (var player in playerStats)
            {
                System.Drawing.Image playerImage = null;
                ListViewItem item = FindPlayerInListView(player.Key);
                if (item == null)
                {
                    continue; // Пропускаем игроков, которые не находятся в списках
                }


                if (item != null && item.ImageIndex >= 0)
                {
                    playerImage = imageList.Images[item.ImageIndex];
                }
                if (item.ImageKey != "")
                {
                    playerImage = imageList.Images[item.ImageKey];
                }

                dgvRankingListPlayers.Rows.Add(playerImage, player.Key, player.Value.Goals, player.Value.YellowCards, player.Value.Appearances);
            }

            CheckBtnPrint();
        }

        private void FillPlayersIntoRankingListPlayers(Country.Root root, Dictionary<string, PlayerStats> playerStats)
        {
            foreach (var item in root.home_team_events)
            {
                ProcessEventHome(item, playerStats);
            }

            foreach (var item in root.away_team_events)
            {
                ProcessEventAway(item, playerStats);
            }

            foreach (var item in root.home_team_statistics.starting_eleven)
            {
                if (!playerStats.ContainsKey(item.name))
                {
                    playerStats[item.name] = new PlayerStats();
                }
                playerStats[item.name].Appearances++;
            }

            foreach (var item in root.away_team_statistics.starting_eleven)
            {
                if (!playerStats.ContainsKey(item.name))
                {
                    playerStats[item.name] = new PlayerStats();
                }
                playerStats[item.name].Appearances++;
            }

            foreach (var item in root.home_team_statistics.substitutes)
            {
                if (!playerStats.ContainsKey(item.name))
                {
                    playerStats[item.name] = new PlayerStats();
                }
                playerStats[item.name].Appearances++;
            }

            foreach (var item in root.away_team_statistics.substitutes)
            {
                if (!playerStats.ContainsKey(item.name))
                {
                    playerStats[item.name] = new PlayerStats();
                }
                playerStats[item.name].Appearances++;
            }
        }

        private void CheckBtnPrint()
        {
            if (dgvRankingListPlayers.Rows.Count > 0 && dgvRankingListMatch.Rows.Count > 0)
            {
                btnPrintRankingList.Enabled = true;
            }
            else
            {
                btnPrintRankingList.Enabled = false;
            }
        }

        private void ProcessEventAway(Country.AwayTeamEvent item, Dictionary<string, PlayerStats> playerStats)
        {
            if (!playerStats.ContainsKey(item.player))
            {
                playerStats[item.player] = new PlayerStats();
            }

            if (item.type_of_event == "goal")
            {
                playerStats[item.player].Goals++;
            }
            else if (item.type_of_event == "yellow-card")
            {
                playerStats[item.player].YellowCards++;
            }
        }

        private ListViewItem FindPlayerInListView(string key)
        {
            foreach (ListViewItem item in lvPlayers.Items)
            {
                if (item.Text.Contains(key))
                {
                    return item;
                }
            }

            foreach (ListViewItem item in lvFavouritePlayers.Items)
            {
                if (item.Text.Contains(key))
                {
                    return item;
                }
            }

            return null;
        }

        private void ProcessEventHome(Country.HomeTeamEvent item, Dictionary<string, PlayerStats> playerStats)
        {
            if (!playerStats.ContainsKey(item.player))
            {
                playerStats[item.player] = new PlayerStats();
            }

            if (item.type_of_event == "goal")
            {
                playerStats[item.player].Goals++;
            }
            else if (item.type_of_event == "yellow-card")
            {
                playerStats[item.player].YellowCards++;
            }
        }

        public class PlayerStats
        {
            public int Goals { get; set; }
            public int YellowCards { get; set; }
            public int Appearances { get; set; }
        }

        private async void FillRankingListMatches()
        {
            dgvRankingListMatch.Rows.Clear();

            IRepoCountry country = new RepoFactoryCountry();
            int startIndex = cbTeam.Text.IndexOf('(');
            int endIndex = cbTeam.Text.IndexOf(')');
            fifaCode = cbTeam.Text.Substring(startIndex + 1, endIndex - startIndex - 1);

            List<Country.Root> roots = new List<Country.Root>();

            if (FileRead == false)
            {
                roots = await country.GetCountryAsyncSwagger(fifaCode, url);
                foreach (Country.Root root in roots)
                {
                    dgvRankingListMatch.Rows.Add(root.fifa_id, root.location, root.attendance, root.home_team_country, root.away_team_country);
                }
            }
            else
            {
                roots = await country.GetCountryAsyncFile(fifaCode, FilePathJsonCountries);
                foreach (Country.Root root in roots)
                {
                    if (root.away_team.country == cbTeam.Text.Split('(')[0].Trim() || root.home_team.country == cbTeam.Text.Split('(')[0].Trim())
                    {
                        dgvRankingListMatch.Rows.Add(root.fifa_id, root.location, root.attendance, root.home_team_country, root.away_team_country);
                    }
                }
            }

            

            CheckBtnPrint();
        }

        public class Match
        {
            public int FifaId { get; set; }
            public string Location { get; set; }
            public int NumberOfVisitors { get; set; }
            public string HostTeam { get; set; }
            public string GuestTeam { get; set; }
        }

        private void ListViewDragDropAddFavourite(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                ListView sourceListView = (ListView)item.ListView;
                ListView targetListView = (ListView)sender;

                if (targetListView.Items.Count >= 3)
                {
                    MessageBox.Show("You can have only 3 favourite players");
                    return;
                }

                if (targetListView.Items.Contains(item))
                {
                    return;
                }

                if(sourceListView.SelectedItems.Count > 0)
                {
                    if(sourceListView.SelectedItems.Count > 3)
                    {
                        MessageBox.Show("It allowed to add maximum 3 players to favourite list"); return;
                    }
                    List<ListViewItem> itemsToMove = new List<ListViewItem>();

                    List<string> playersToFile = new List<string>(File.ReadAllLines(FilePathFavouritePlayers));
                    foreach (ListViewItem litem in sourceListView.SelectedItems)
                    {
                        itemsToMove.Add(litem);
                        string playerInfo = item.Text;
                        playersToFile.Add(playerInfo + ", Country: " + cbTeam.Text);
                    }
                    File.WriteAllLines(FilePathFavouritePlayers, playersToFile);

                    foreach (ListViewItem litem in itemsToMove)
                    {
                        ListViewItem newItem = (ListViewItem)litem.Clone();
                        newItem.Text = litem.Text + " " + star;
                        lvFavouritePlayers.Items.Add(newItem);
                        lvPlayers.Items.Remove(litem);
                    }

                    lblPlayers.Text = $"{lvFavouritePlayers.Items.Count} / 3";

                } 

            }
        }

        private void ListViewDragDropRemoveFavourite(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                ListView sourceListView = (ListView)item.ListView;
                ListView targetListView = (ListView)sender;

                if(sourceListView.SelectedItems.Count > 0)
                {
                    List<ListViewItem> itemsToMove = new List<ListViewItem>();

                    foreach(ListViewItem litem in sourceListView.SelectedItems)
                    {
                        itemsToMove.Add(litem);
                    }

                    foreach (ListViewItem litem in itemsToMove)
                    {
                        int index = sourceListView.Items.IndexOf(litem);

                        if (RemoveItem(index, cbTeam.Text))
                        {
                            ListViewItem newItem = new ListViewItem(litem.Text.Replace(" " + star, ""));
                            if (!targetListView.Items.Contains(newItem))
                            {
                                if (item.ImageKey != "")
                                {
                                    newItem.ImageKey = item.ImageKey;
                                }
                                else
                                {
                                    newItem.ImageIndex = item.ImageIndex;
                                }
                                targetListView.Items.Add(newItem);
                            }
                        }

                        lvFavouritePlayers.Items.Remove(litem);
                        lblPlayers.Text = $"{lvFavouritePlayers.Items.Count} / 3";
                    }
                }
            }
        }
        private void ListViewDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ListViewItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private async void StartApplication()
        {
            IRepoTeam repoTeam = new RepoFactoryTeam();
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
                    System.Windows.Forms.Application.Run(new Settings());
                    this.Hide();
                }

                await FillComboBoxWithTeams();
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task FillComboBoxWithTeams()
        {
            try
            {
                IRepoTeam repoTeam = new RepoFactoryTeam();
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
                    cbTeam.Items.Add($"{team.Country} ({team.Fifa_Code})");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (!File.Exists(FilePathOnLoad))
            {
                List<string> countries = new List<string>(File.ReadAllLines(FilePathOnLoad));
                if (countries[0].Split(':')[1].Trim() != "Team")
                {
                    File.Create(FilePathOnLoad);
                }
                cbTeam.SelectedIndex = 0;
            }
            else
            {
                List<string> lines = new List<string>(File.ReadAllLines(FilePathOnLoad));
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Split(':')[0].Trim() == "Team" && cbTeam.Items.Contains(lines[i].Split(':')[1].Trim()))
                    {
                        cbTeam.Text = lines[i].Split(':')[1].Trim();
                    }
                }
            }
        }

        private static bool CheckFile()
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
                        if (!string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(language) && !string.IsNullOrEmpty(FileReadMode))
                        {
                            // Проверка, что gender и language записаны корректно
                            if ((gender == "Male" || gender == "Female") && (language == "English" || language == "Croatian") && (FileReadMode == "0" || FileReadMode == "1"))
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
            System.Windows.Forms.Application.Run(new Settings());
            return false;
        }

            private void ResetPhoto(ListView lv)
            {
                if (lv.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = lv.SelectedItems[0];
                    List<string> updateplayers = new List<string>();
                    foreach(string line in File.ReadAllLines(PlayerImagePath))
                    {
                        int nameStartIndex = selectedItem.Text.IndexOf("Name: ") + "Name: ".Length;
                        int nameEndIndex = selectedItem.Text.IndexOf(',', nameStartIndex); // Находим конец имени (запятая)
                        string playerName = selectedItem.Text.Substring(nameStartIndex, nameEndIndex - nameStartIndex).Trim();
                        // Разделяем строку на имя игрока и путь к изображению
                        string[] parts = line.Split('=');

                        string imageName = parts[0].Trim();
                        if (playerName != imageName)
                        {
                            updateplayers.Add(line);
                        }
                    }
                    File.WriteAllLines(PlayerImagePath, updateplayers);
                    selectedItem.ImageIndex = 0; // Сброс индекса изображения для выбранного элемента
                }
                else
                {
                    MessageBox.Show("Please select exactly one player to reset the photo.") ; return;
                }
            }

            private void ChangePhoto(ListView lv)
            {
                if(lv.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Please select exactly one player to change the photo.") ; return;
                }

                ResetPhoto(lv);
                
                ListViewItem selecteditem = lv.SelectedItems[0];

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", Path.GetFileName(filePath));
                        File.Copy(filePath,destinationPath,true);
                       

                        imageList.Images.Add(Path.GetFileName(filePath), System.Drawing.Image.FromFile(destinationPath));
                        int index = imageList.Images.IndexOfKey(Path.GetFileName(filePath));
                        if (index != -1)
                        {
                            selecteditem.ImageIndex = index;
                        }
                        else
                        {
                            MessageBox.Show("Error when setting the image index") ; return;
                        }


                        int endindex = selecteditem.Text.IndexOf(',');
                        string playerImagePathFile =  PlayerImagePath;
                        File.AppendAllText(playerImagePathFile, selecteditem.Text.Substring(6, endindex - 6) + "= " + destinationPath + Environment.NewLine);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        private void OnEmptySpaceClickPlayers(object sender, MouseEventArgs e)
            {
                int index = lvPlayers.Items.IndexOf(lvPlayers.GetItemAt(e.X, e.Y));
                if (index == ListBox.NoMatches)
                {
                    lvPlayers.SelectedItems.Clear();
                }
                else if(e.Button == MouseButtons.Right && lvPlayers.SelectedItems.Count > 1)
                {
                    lvPlayers.Items[index].Selected = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    lvPlayers.SelectedItems.Clear();
                    lvPlayers.Items[index].Selected = true;
                }
            }

            private void OnEmptySpaceClickFavouritePlayers(object sender, MouseEventArgs e)
            {
                int index = lvFavouritePlayers.Items.IndexOf(lvFavouritePlayers.GetItemAt(e.X, e.Y));
                if (index == ListBox.NoMatches)
                {
                    lvFavouritePlayers.SelectedItems.Clear();
                }
                else if (e.Button == MouseButtons.Right && lvFavouritePlayers.SelectedItems.Count > 1)
                {
                    lvFavouritePlayers.Items[index].Selected = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    lvFavouritePlayers.SelectedItems.Clear();
                    lvFavouritePlayers.Items[index].Selected = true;
                }
            }

            private void OnContextMenuOpeningPlayers(object sender, CancelEventArgs e)
            {
                if (lvPlayers.SelectedItems.Count == 0)
                {
                    e.Cancel = true;
                }
            }

            private void OnContextMenuOpeningFavouritePlayers(object sender, CancelEventArgs e)
            {
                if(lvFavouritePlayers.SelectedItems.Count == 0)
                {
                    e.Cancel = true;
                }
            }


            private static readonly string star = "🌟";
            private void RemoveFromFavourite(object sender, EventArgs e)
            {
                if (lvFavouritePlayers.SelectedItems.Count > 0)
                {
                    List<ListViewItem> itemsToMove = new List<ListViewItem>();

                    foreach (ListViewItem item in lvFavouritePlayers.SelectedItems)
                    {
                        itemsToMove.Add(item);
                    }

                   foreach (ListViewItem item in itemsToMove)
                {
                    int index = lvFavouritePlayers.Items.IndexOf(item);

                    // Удаление только если страна совпадает с текущей выбранной страной
                    if (RemoveItem(index, cbTeam.Text))
                    {
                        ListViewItem newItem = new ListViewItem(item.Text.Replace(" " + star, ""));
                        if (!lvPlayers.Items.Contains(newItem))
                        {
                            if (item.ImageKey != "")
                            {
                                newItem.ImageKey = item.ImageKey;
                            }
                            else
                            {
                                newItem.ImageIndex = item.ImageIndex;
                            }  
                            lvPlayers.Items.Add(newItem);
                        }
                    }

                    lvFavouritePlayers.Items.Remove(item);
                    lblPlayers.Text = $"{lvFavouritePlayers.Items.Count} / 3";
                }
                }
            }

            private bool RemoveItem(int index, string currentTeam)
            {
                List<string> itemsFromFile = new List<string>(File.ReadAllLines(FilePathFavouritePlayers));

                if (index < 0 || index >= itemsFromFile.Count)
                {
                    return false;
                }

                string line = itemsFromFile[index];
                int startIndexChecking = line.IndexOf("Country:");
                int endIndexChecking = line.Length;

                string tempChecker = line.Substring(startIndexChecking + 9, endIndexChecking - startIndexChecking - 9).Trim();

                itemsFromFile.RemoveAt(index);
                File.WriteAllLines(FilePathFavouritePlayers, itemsFromFile);

                // Удаляем только если страна совпадает с текущей выбранной страной
                if (tempChecker == currentTeam.Trim())
                {
                    return true;
                }

                return false;
            }



            private void AddToFavourite(object sender, EventArgs e)
                {
                    if (!(lvFavouritePlayers.Items.Count >= 3)) 
                    { 
                        if (lvPlayers.SelectedItems.Count > 0)
                        {
                        if(lvPlayers.SelectedItems.Count > 3)
                        {
                            MessageBox.Show("It allowed to add maximum 3 players to favourite list");
                            return;
                        }
                        List<ListViewItem> itemsToMove = new List<ListViewItem>();

                        try
                        {
                            if (!File.Exists(FilePathFavouritePlayers))
                            {
                                File.Create(FilePathFavouritePlayers); 
                            }

                            List<string> playersToFile = new List<string>(File.ReadAllLines(FilePathFavouritePlayers));

                            foreach (ListViewItem item in lvPlayers.SelectedItems)
                            {
                                itemsToMove.Add(item);
                                string playerInfo = item.Text;
                                playersToFile.Add(playerInfo + ", Country: " + cbTeam.Text);
                            }

                            File.WriteAllLines(FilePathFavouritePlayers, playersToFile);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        } 
                    

                        // Перемещаем элементы
                        foreach (ListViewItem item in itemsToMove)
                        {
                            ListViewItem newItem = (ListViewItem)item.Clone();
                            newItem.Text = item.Text + " " + star;
                            lvFavouritePlayers.Items.Add(newItem);
                            lvPlayers.Items.Remove(item);
                        }

                        lblPlayers.Text = $"{lvFavouritePlayers.Items.Count} / 3";
                        }
                    }
                    else
                    {
                        MessageBox.Show("It allowed to add maximum 3 players to favourite list");
                        return;
                    }
                
                    
                }

            private void Form2_Load(object sender, EventArgs e)
            {
                int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;

                this.Location = new Point(x, y);
            }

            private async void cbTeam_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    string targetline = "Team";
                    bool keyFound = false;

                    List<string> lines = new List<string>(File.ReadAllLines(FilePathOnLoad));

                    for (int i = 0; i < lines.Count; i++) 
                    {
                        if (lines[i].Split(':')[0].Trim() == targetline)
                        {
                            lines[i] = $"{targetline}: {cbTeam.Text}";
                            keyFound = true;
                            break;
                        }
                    }

                    if (!keyFound)
                    {
                        lines.Add($"{targetline}: {cbTeam.Text}");
                    }

                    File.WriteAllLines(FilePathOnLoad, lines);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    IRepoCountry country = new RepoFactoryCountry();
                    int startIndex = cbTeam.Text.IndexOf('(');
                    int endIndex = cbTeam.Text.IndexOf(')');
                    fifaCode = cbTeam.Text.Substring(startIndex + 1, endIndex - startIndex - 1);

                    List<Country.Root> roots = new List<Country.Root>();
                    List<Country.StartingEleven> StartingEleven = new List<Country.StartingEleven>();
                    List<Country.Substitute> Substitutes = new List<Country.Substitute>();

                if (FileRead == false)
                {
                    roots = await country.GetCountryAsyncSwagger(fifaCode, url);                  
                }
                else
                {
                    roots = await country.GetCountryAsyncFile(fifaCode, FilePathJsonCountries);
                }



                foreach (var root in roots)
                {
                    if (root.home_team_statistics.country == cbTeam.SelectedItem.ToString().Split('(')[0].Trim())
                    {
                        FillPlayersIntoLbPlayersHome(root, StartingEleven, Substitutes);
                        break;
                    }
                    else if (root.away_team_statistics.country == cbTeam.SelectedItem.ToString().Split('(')[0].Trim())
                    {
                        FillPlayersIntoLbPlayersAway(root, StartingEleven, Substitutes);
                        break;
                    }
                }


                lvPlayers.Items.Clear();
                    
                    foreach(var player in StartingEleven)
                    {
                        ListViewItem newitem = new ListViewItem(player.ToString());
                        if (!lvPlayers.Items.Contains(newitem))
                        {
                        FillLVBPlayersStartingEleven(player);
                        }
                    }

                    foreach(var player in Substitutes)
                    {
                        ListViewItem newitem = new ListViewItem(player.ToString());
                        if (!lvPlayers.Items.Contains(newitem))
                        {
                            FillLVBPlayersSubstitutes(player);
                        }
                    }

                    lvPlayers.Sorting = SortOrder.Ascending;
                    
                
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

        private void FillPlayersIntoLbPlayersAway(Root root, List<StartingEleven> startingEleven, List<Substitute> substitutes)
        {
            foreach (var player in root.away_team_statistics.starting_eleven)
            {
                if (!startingEleven.Contains(player) && !lvFavouritePlayers.Items.Cast<ListViewItem>().Any(i => i.Text.Contains(player.ToString())))
                {
                    startingEleven.Add(player);
                }
            }

            foreach (var player in root.away_team_statistics.substitutes)
            {
                if (!substitutes.Contains(player) && !lvFavouritePlayers.Items.Cast<ListViewItem>().Any(i => i.Text.Contains(player.ToString())))
                {
                    substitutes.Add(player);
                }
            }
        }

        private void FillLVBPlayersSubstitutes(Substitute player)
        {
            ListViewItem newitem = new ListViewItem(player.ToString());
            int nameStartIndex = player.ToString().IndexOf("Name: ") + "Name: ".Length;
            int nameEndIndex = player.ToString().IndexOf(',', nameStartIndex);
            newitem.ImageIndex = 0;
            if (nameStartIndex != -1 && nameEndIndex != -1)
            {
                string playerName = player.ToString().Substring(nameStartIndex, nameEndIndex - nameStartIndex).Trim();
                foreach (string line in File.ReadAllLines(PlayerImagePath))
                {
                    // Разделяем строку на имя игрока и путь к изображению
                    string[] parts = line.Split('=');
                    if (parts.Length < 2) continue; // Пропустить строки, если они неправильно отформатированы

                    string imageName = parts[0].Trim();
                    string imagePathI = line.Split('=')[1].Trim();
                    if (playerName == imageName)
                    {
                        // Проверка, если изображение уже добавлено в ImageList
                        if (!imageList.Images.ContainsKey(imageName))
                        {
                            // Добавляем изображение в ImageList с использованием имени игрока в качестве ключа
                            imageList.Images.Add(imageName, System.Drawing.Image.FromFile(imagePathI));
                        }
                        // Устанавливаем ImageKey для элемента списка
                        newitem.ImageKey = imageName;
                        break; // Выход из внутреннего цикла после нахождения соответствующего изображения
                    }
                    else
                    {
                        newitem.ImageIndex = 0;
                    }
                }
            }
            lvPlayers.Items.Add(newitem);
        }

        private void FillLVBPlayersStartingEleven(StartingEleven player)
        {
            ListViewItem newitem = new ListViewItem(player.ToString());
            int nameStartIndex = player.ToString().IndexOf("Name: ") + "Name: ".Length;
            int nameEndIndex = player.ToString().IndexOf(',', nameStartIndex);
            newitem.ImageIndex = 0;
            if (nameStartIndex != -1 && nameEndIndex != -1)
            {
                string playerName = player.ToString().Substring(nameStartIndex, nameEndIndex - nameStartIndex).Trim();
                foreach (string line in File.ReadAllLines(PlayerImagePath))
                {
                    // Разделяем строку на имя игрока и путь к изображению
                    string[] parts = line.Split('=');
                    if (parts.Length < 2) continue; // Пропустить строки, если они неправильно отформатированы

                    string imageName = parts[0].Trim();
                    string imagePathI = line.Split('=')[1].Trim();
                    if (playerName == imageName)
                    {
                        // Проверка, если изображение уже добавлено в ImageList
                        if (!imageList.Images.ContainsKey(imageName))
                        {
                            // Добавляем изображение в ImageList с использованием имени игрока в качестве ключа
                            imageList.Images.Add(imageName, System.Drawing.Image.FromFile(imagePathI));
                        }
                        // Устанавливаем ImageKey для элемента списка
                        newitem.ImageKey = imageName;
                        break; // Выход из внутреннего цикла после нахождения соответствующего изображения
                    }
                    else
                    {
                        newitem.ImageIndex = 0;
                    }
                }
            }
            lvPlayers.Items.Add(newitem);
        }

        private void FillPlayersIntoLbPlayersHome(Country.Root root, List<Country.StartingEleven> StartingEleven, List<Country.Substitute> Substitutes)
        {
            foreach (var player in root.home_team_statistics.starting_eleven)
            {
                if (!(StartingEleven.Contains(player)) && !lvFavouritePlayers.Items.Cast<ListViewItem>().Any(i => i.Text.Contains(player.ToString())))
                {
                    StartingEleven.Add(player);
                }
            }


            foreach (var player in root.home_team_statistics.substitutes)
            {
                if (!(Substitutes.Contains(player)) && !lvFavouritePlayers.Items.Cast<ListViewItem>().Any(i => i.Text.Contains(player.ToString())))
                {
                    Substitutes.Add(player);
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
            {
                Settings settings = new Settings();
                settings.Show();
                this.Hide();
            }

        private void btnCreateRListOfPlayers_Click(object sender, EventArgs e)
        {
            FillRankingListPlayers();
        }

        private void btnCreateRListOfMatches_Click(object sender, EventArgs e)
        {
            FillRankingListMatches();
        }

        private void btnPrintRankingList_Click(object sender, EventArgs e)
        {

                PdfDocument pdf = new PdfDocument(new PdfWriter("RankingList.pdf"));
                Document document = new Document(pdf);

                // Создаем таблицу с тремя столбцами для имени игрока, голов и желтых карточек
                Table tablePlayers = new Table(5);

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
                            tablePlayers.AddHeaderCell("Image");
                            tablePlayers.AddHeaderCell("Player");
                            tablePlayers.AddHeaderCell("Goals");
                            tablePlayers.AddHeaderCell("Yellow Cards");
                            tablePlayers.AddHeaderCell("Attendance");
                        }
                        else if (language == "Croatian")
                        {
                            tablePlayers.AddHeaderCell("Image");
                            tablePlayers.AddHeaderCell("Igrač");
                            tablePlayers.AddHeaderCell("Ciljevi");
                            tablePlayers.AddHeaderCell("Žuti kartoni");
                            tablePlayers.AddHeaderCell("Posjećenost");
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

            // Заполняем таблицу данными из DataGridView
            foreach (DataGridViewRow row in dgvRankingListPlayers.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // Если это первая ячейка в строке (с изображением), конвертируем ее в iText.Layout.Element.Image
                    if (cell.ColumnIndex == 0 && cell.Value != null && cell.Value is System.Drawing.Image)
                    {
                        System.Drawing.Image sourceImage = (System.Drawing.Image)cell.Value;

                        // Создаем временный файл для изображения
                        string tempImagePath = Path.GetTempFileName();
                        sourceImage.Save(tempImagePath);

                        // Конвертируем временный файл в iText.Image
                        iText.Layout.Element.Image iTextImage = new iText.Layout.Element.Image(ImageDataFactory.Create(tempImagePath));
                        tablePlayers.AddCell(iTextImage);
                        File.Delete(tempImagePath);
                    }
                    else
                    {
                        // Добавляем текстовое значение ячейки в таблицу
                        tablePlayers.AddCell(cell.Value?.ToString() ?? "");
                    }
                }
            }

            // Добавляем таблицу в документ
                document.Add(tablePlayers);

                Paragraph matches = new Paragraph("Matches");
                document.Add(matches);

                Table tableMatches = new Table(5);

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
                            tableMatches.AddHeaderCell("ID");
                            tableMatches.AddHeaderCell("Location");
                            tableMatches.AddHeaderCell("Number of visitors");
                            tableMatches.AddHeaderCell("Host team");
                            tableMatches.AddHeaderCell("Guest team");
                        }
                        else if (language == "Croatian")
                        {
                            tableMatches.AddHeaderCell("Iskaznica");
                            tableMatches.AddHeaderCell("Mjesto");
                            tableMatches.AddHeaderCell("Broj posjetitelja");
                            tableMatches.AddHeaderCell("Ekipa domaćina");
                            tableMatches.AddHeaderCell("Gostujuća ekipa");
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

                

                foreach (DataGridViewRow row in dgvRankingListMatch.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        tableMatches.AddCell(cell.Value.ToString());
                    }
                }

                document.Add(tableMatches);

                document.Close();

                System.Diagnostics.Process.Start("RankingList.pdf");

        }

    }
    }
