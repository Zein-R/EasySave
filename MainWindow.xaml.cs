using System.Globalization;
using System.Windows;
using EasySave.NS_View;
using EasySave.NS_ViewModel;
using EasySave.NS_Model;
using System.Threading;
using System.Diagnostics;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Controls;

namespace EasySave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ----- Attributes -----
        public int[] selectedWorksId { get; set; }
        public Model model { get; set; }

        private MenuView menuView { get; set; }
        private AddWorkView addWorkView { get; set; }
        private SettingsView settingsView { get; set; }
        private ErrorView errorView { get; set; }

        public MenuViewModel menuViewModel { get; set; }
        public AddWorkViewModel addWorkViewModel { get; set; }
        public SettingsViewModel settingsViewModel { get; set; }

        // ----- Constructor -----
        public MainWindow()
        {
            if (Process.GetProcessesByName("EasySave").Length == 1)
            {

                // Initialize Model
                this.model = new Model();

                // Initialize ViewModel
                this.errorView = new ErrorView(model);
                this.menuViewModel = new MenuViewModel(model);

                // Load Language
                Langs.Lang.Culture = new CultureInfo(model.settings.language);

                // Set Main Window Datacontent
                menuView = new MenuView(menuViewModel, this);
                DataContext = menuView;

                // Initialize Main Window
                InitializeComponent();
            }
            else
            {
                MessageBox.Show(Langs.Lang.alreadyRunning);
                App.Current.Shutdown();
            }
        }

        private void ChangePage(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hellloooooo");
            var button = sender as RadioButton;
            bool isBackupRunning = false;

            // Check if a Backup is Running
            foreach (var work in this.menuViewModel.model.works)
            {
                if (work.colorProgressBar != "White")
                {
                    isBackupRunning = true;
                    break;
                }
            }

            // Prevent from Swhitching to Settings if a Backup is Running
            if (button.Tag.ToString() == "settings" && isBackupRunning)
            {
                // Call Error Message if a Works is Running
                this.menuViewModel.model.errorMsg?.Invoke("cantGoToSettings");
            }
            else
            {
                // Change Page
                this.ChangePage(button.Tag.ToString());
            }
        }


        // ----- Methods -----
        // Change Main Window Content (Change Datacontent)
        public void ChangePage(string _route)
        {
            switch (_route)
            {
                case "menu":
                    DataContext = menuView;
                    return;

                case "addWork":
                    if (addWorkView == null)
                    {
                        this.addWorkViewModel = new AddWorkViewModel(model);
                        addWorkView = new AddWorkView(addWorkViewModel, this);
                    }
                    DataContext = addWorkView;
                    return;

                case "settings":
                    if (settingsView == null)
                    {
                        this.settingsViewModel = new SettingsViewModel(model);
                        settingsView = new SettingsView(settingsViewModel, this);
                    }
                    DataContext = settingsView;
                    return;
            }
        }

        private void InitSocket(object sender, RoutedEventArgs e)
        {
            if (this.menuViewModel.listener == null)
            {
                this.menuViewModel.SocketOn();
                MessageBox.Show("Connection Successful");
            }
            else
            {
                MessageBox.Show("Socket running");
            }

        }

        private void btnReduce_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void OpenLogFiles_Click(object sender, RoutedEventArgs e)
        {
           /* string cheminFichier = @"";

            // Check if the folder exist
            if (System.IO.File.Exists(cheminFichier))
            {
                // Open the folder with notepade
                Process.Start("notepad.exe", cheminFichier);
            }
            else
            {
                MessageBox.Show("Le fichier spécifié n'existe pas.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }
    }
}
