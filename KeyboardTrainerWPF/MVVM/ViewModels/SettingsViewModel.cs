using KeyboardTrainerWPF.Core;
using KeyboardTrainerWPF.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    public class SettingsViewModel : DependencyObject, ISetAppereance
    {
        #region Private Fields
        private ComboBox _comboBox;
        private CheckBox _checkBox;
        #endregion

        #region Public Constuctors
        public SettingsViewModel()
        {
            UserName = Properties.Settings.Default.UserName;
            Complexity = Properties.Settings.Default.Complexity;
            LanguageCode = Properties.Settings.Default.LanguageCode;
            IsDarkTheme = Properties.Settings.Default.DarkTheme;

            Languages = new ObservableCollection<string>() { "English", "Українська" };


            _comboBox = new ComboBox();
            _checkBox = new CheckBox();

            switch (Properties.Settings.Default.LanguageCode)
            {
                case "uk-UA":
                    SelectedIndex = 1;
                    break;

                default:
                    SelectedIndex = 0;
                    break;
            }
            switch (Properties.Settings.Default.DarkTheme)
            {
                case true:
                    _checkBox.IsChecked = true;
                    break;

                default:
                    _checkBox.IsChecked = false;
                    break;
            }

            LoginCommand = new RelayCommand(Execute_LogIn);
            SignUpCommand = new RelayCommand(Execute_SignUp);
            LogOutCommand = new RelayCommand(Execute_LogOut);
            SaveCommand = new RelayCommand(Execute_Save);
            LanguageComboBoxSelectionChanged = new RelayCommand(ExecuteLanguageChanged);
            ThemeChanged = new RelayCommand(ExecuteThemeChanged);
            SetAppereance(Properties.Settings.Default.DarkTheme);
        }

        public SettingsViewModel(ComboBox comboBox, CheckBox checkBox)
        {
            UserName = Properties.Settings.Default.UserName;
            Complexity = Properties.Settings.Default.Complexity;
            LanguageCode = Properties.Settings.Default.LanguageCode;
            IsDarkTheme = Properties.Settings.Default.DarkTheme;

            Languages = new ObservableCollection<string>() { "English", "Українська" };

            _comboBox = comboBox;
            _checkBox = checkBox;

            switch (Properties.Settings.Default.LanguageCode)
            {
                case "uk-UA":
                    SelectedIndex = 1;
                    break;

                default:
                    SelectedIndex = 0;
                    break;
            }
            switch (Properties.Settings.Default.DarkTheme)
            {
                case true:
                    _checkBox.IsChecked = true;
                    break;

                default:
                    _checkBox.IsChecked = false;
                    break;
            }

            LoginCommand = new RelayCommand(Execute_LogIn);
            SignUpCommand = new RelayCommand(Execute_SignUp);
            LogOutCommand = new RelayCommand(Execute_LogOut);
            SaveCommand = new RelayCommand(Execute_Save);
            LanguageComboBoxSelectionChanged = new RelayCommand(ExecuteLanguageChanged);
            ThemeChanged = new RelayCommand(ExecuteThemeChanged);
            SetAppereance(Properties.Settings.Default.DarkTheme);
        }
        #endregion

        #region Public Properties
        public ObservableCollection<string> Languages { get; set; }
        #endregion


        #region Dependency Properties
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(SettingsViewModel),
                new PropertyMetadata(Properties.Settings.Default.UserName));

        public double Complexity
        {
            get { return (double)GetValue(ComplexityProperty); }
            set { SetValue(ComplexityProperty, value); }
        }
        public static readonly DependencyProperty ComplexityProperty =
            DependencyProperty.Register("Complexity", typeof(double), typeof(SettingsViewModel),
                new PropertyMetadata(Properties.Settings.Default.Complexity));

        public bool IsDarkTheme
        {
            get { return (bool)GetValue(IsDarkThemeProperty); }
            set { SetValue(IsDarkThemeProperty, value); }
        }
        public static readonly DependencyProperty IsDarkThemeProperty =
            DependencyProperty.Register("IsDarkTheme", typeof(bool), typeof(SettingsViewModel),
                new PropertyMetadata(Properties.Settings.Default.DarkTheme));

        public string LanguageCode
        {
            get { return (string)GetValue(LanguageCodeProperty); }
            set { SetValue(LanguageCodeProperty, value); }
        }
        public static readonly DependencyProperty LanguageCodeProperty =
            DependencyProperty.Register("LanguageCode", typeof(string), typeof(SettingsViewModel),
                new PropertyMetadata(Properties.Settings.Default.LanguageCode));

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(SettingsViewModel), new PropertyMetadata(0));
        #endregion


        #region Commands
        public ICommand LoginCommand { get; }
        private void Execute_LogIn(object? obj) => new Registration(DataRecorder.ThrowAnExceptionIfAccountHasNotBeenFound).ShowDialog();

        public ICommand SignUpCommand { get; }
        private void Execute_SignUp(object? obj) => new Registration(DataRecorder.WriteAccount).ShowDialog();

        public ICommand LogOutCommand { get; }
        private void Execute_LogOut(object? obj)
        {
            if (Properties.Settings.Default.UserName != "Guest")
            {
                MessageBox.Show($"{Properties.Settings.Default.UserName}, you have successfuly quit your account", "Quit",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Properties.Settings.Default.UserName = "Guest";
            }
        }

        public ICommand SaveCommand { get; }
        private void Execute_Save(object? obj)
        {
            if (obj is UserControl window)
            {
                Properties.Settings.Default.Complexity = Complexity;
                Properties.Settings.Default.Save();
                if (MessageBox.Show("Settings were saved", "Saved!", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                {
                    var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
                    Process.Start(currentExecutablePath);
                    Application.Current.Shutdown();
                }

            }
        }

        public ICommand LanguageComboBoxSelectionChanged { get; }
        private void ExecuteLanguageChanged(object? obj)
        {
            if (_comboBox != null)
            {
                string[] languageCodes = new[] { "en-US", "uk-UA" };
                Properties.Settings.Default.LanguageCode = languageCodes[_comboBox.SelectedIndex];
                Properties.Settings.Default.Save();
            }
        }

        public ICommand ThemeChanged { get; }
        
        private void ExecuteThemeChanged(object? obj)
        {
            if (_checkBox != null)
            {
                Properties.Settings.Default.DarkTheme = (bool)_checkBox.IsChecked;
                Properties.Settings.Default.Save();
            }
        }
        #endregion


        #region InterfaceImplementation
        public Brush TextColor { get; set; }
        public Brush SecondColor { get; set; }
        public Brush BackgroundColor { get; set; }
        public void SetAppereance(bool isDarkTheme)
        {
            if (isDarkTheme)
            {
                BackgroundColor = new SolidColorBrush(Color.FromRgb(38, 50, 56));
                TextColor = new SolidColorBrush(Color.FromRgb(236, 239, 241));
            }
            else
            {
                BackgroundColor = new SolidColorBrush(Color.FromRgb(207, 216, 220));
                TextColor = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            }
            SecondColor = new SolidColorBrush(Color.FromRgb(96, 125, 139));
            return;
        }
        #endregion
    }
}
