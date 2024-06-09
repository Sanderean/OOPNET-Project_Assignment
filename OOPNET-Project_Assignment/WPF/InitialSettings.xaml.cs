using DataLayer;
using OOPNET_Project_Assignment.DataLayer;
using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for InitialSettings.xaml
    /// </summary>
    public partial class InitialSettings : Window
    {
        private static string url;

        private static readonly string relativePath = @"DataLayer\settings.txt";

        private static readonly string SettingsFilePath = SettingsManager.GetSettingsPath(relativePath);
        public InitialSettings()
        {
            InitializeComponent();
            CheckLanguage();
            LanguageComboBox.SelectedIndex = 0;
            GenderComboBox.SelectedIndex = 0;
            ResolutionComboBox.SelectedIndex = 0;
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
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading file: " + ex.Message);
                }
            }
            return false;
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
                            lblLanguage.Content = "Choose language:";
                            lblGender.Content = "Choose gender:";
                            lblResolution.Content = "Choose resolution:";
                            cbReadMode.Content = "Read from file mode";
                            btnSave.Content = "Save";
                        }
                        else if (language == "Croatian")
                        {
                            lblLanguage.Content = "Odaberi jezik:";
                            lblGender.Content = "Odaberi spol:";
                            lblResolution.Content = "Odaberi rezoluciju:";
                            cbReadMode.Content = "Način čitanja iz datoteke";
                            btnSave.Content = "Spremi";
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {


            if (File.Exists(SettingsFilePath) && CheckFile())
            {
                WriteToFile(SettingsFilePath, LanguageComboBox.Text, GenderComboBox.Text, ResolutionComboBox.Text);
                this.DialogResult = true;
                this.Hide(); // Скрываем текущее окно
            }
            else
            {
                WriteToFile(SettingsFilePath, LanguageComboBox.Text, GenderComboBox.Text, ResolutionComboBox.Text);
                this.Hide(); // Скрываем текущее окно
            }

            try
            {
                WPFapplication WPFapplication = new WPFapplication();
                WPFapplication.Show(); // Открываем новое окно
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                if (File.Exists(SettingsFilePath))
                {
                    this.DialogResult = false;
                    this.Close();
                }
                else
                {
                    MessageBoxResult result = ShowExitConfirmation();
                    if(result == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }

                }
            }
        }

        private MessageBoxResult ShowExitConfirmation()
        {
            MessageBoxResult result = MessageBoxResult.None;

            // Создаем новое окно для подтверждения выхода
            Window confirmationWindow = new Window
            {
                Title = "Exit Confirmation",
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.ToolWindow,
                ResizeMode = ResizeMode.NoResize,
                Owner = this
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

            // Обработка нажатия клавиши Escape
            confirmationWindow.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    result = MessageBoxResult.No;
                    confirmationWindow.Close();
                }
                if(e.Key == Key.Enter)
                {
                    Application.Current.Shutdown();
                }
            };

            // Показываем окно подтверждения как диалог
            confirmationWindow.ShowDialog();

            return result;

        }

        private void WriteToFile(string settingsFilePath, string language, string gender, string resolution)
        {
            try
            {
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
                    MessageBox.Show("Error while assigning the gender");
                }
                using (StreamWriter writer = new StreamWriter(settingsFilePath))
                {
                    writer.WriteLine("Gender: " + gender);
                    writer.WriteLine("Language: " + language);
                    if (cbReadMode.IsChecked == true)
                    {
                        writer.WriteLine("FileRead: " + 1);
                    }
                    else
                    {
                        writer.WriteLine("FileRead: " + 0);
                    }
                    writer.WriteLine("Resolution: " + resolution);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
