using KeyboardTrainerModel;
using KeyboardTrainerModel.Interfaces;
using KeyboardTrainerWPF.Core;
using KeyboardTrainerWPF.MVVM.Views;
using KeyboardTrainerWPF.Properties.Languages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    public class SettingsViewModel : DependencyObject, ISetAppereance
    {
        #region Private Fields
        private ComboBox _languageComboBox;
        private ComboBox _textLanguageComboBox;
        private CheckBox _checkBox;
        private readonly IUserService users;
        private readonly IScoreService scores;
        private readonly ITextService texts;
        #endregion

        #region Public Constuctors
        public SettingsViewModel(IUserService userService, IScoreService scoreService, ITextService textService)
        {
            users = userService;
            scores = scoreService;
            texts = textService;

            UserName = Properties.Settings.Default.UserName;
            Complexity = Properties.Settings.Default.Complexity;
            LanguageCode = Properties.Settings.Default.LanguageCode;
            IsDarkTheme = Properties.Settings.Default.DarkTheme;

            Languages = new ObservableCollection<string>() { "English", "Українська" };

            _languageComboBox = new ComboBox();
            _checkBox = new CheckBox();

            switch (Properties.Settings.Default.LanguageCode)
            {
                case "uk-UA":
                    LanguageSelectedIndex = 1;
                    break;

                default:
                    LanguageSelectedIndex = 0;
                    break;
            }
            switch (Properties.Settings.Default.TextLanguageCode)
            {
                case "uk-UA":
                    TextLanguageSelectedIndex = 1;
                    break;

                default:
                    TextLanguageSelectedIndex = 0;
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

            LoginCommand = new RelayCommand(ExecuteLogIn);
            SignUpCommand = new RelayCommand(ExecuteSignUp);
            LogOutCommand = new RelayCommand(ExecuteLogOut);
            SaveCommand = new RelayCommand(Execute_Save);
            LanguageComboBoxSelectionChanged = new RelayCommand(ExecuteLanguageChanged);
            TextLanguageBoxSelectionChanged = new RelayCommand(ExecuteTextLanguageChanged);
            ThemeChanged = new RelayCommand(ExecuteThemeChanged);
            SetAppereance(Properties.Settings.Default.DarkTheme);
        }

        public SettingsViewModel(ComboBox languageComboBox, ComboBox textLanguageComboBox, CheckBox checkBox)
        {
            var serviceProvider = ((App)Application.Current).Services;
            if (serviceProvider == null) throw new NullReferenceException(nameof(serviceProvider));
            users = serviceProvider.GetService<IUserService>();
            scores = serviceProvider.GetService<IScoreService>();
            texts = serviceProvider.GetService<ITextService>();

            UserName = Properties.Settings.Default.UserName;
            Complexity = Properties.Settings.Default.Complexity;
            LanguageCode = Properties.Settings.Default.LanguageCode;
            IsDarkTheme = Properties.Settings.Default.DarkTheme;

            Languages = new ObservableCollection<string>() { "English", "Українська" };

            _languageComboBox = languageComboBox;
            _textLanguageComboBox = textLanguageComboBox;
            _checkBox = checkBox;

            switch (Properties.Settings.Default.LanguageCode)
            {
                case "uk-UA":
                    LanguageSelectedIndex = 1;
                    break;

                default:
                    LanguageSelectedIndex = 0;
                    break;
            }
            switch (Properties.Settings.Default.TextLanguageCode)
            {
                case "uk-UA":
                    TextLanguageSelectedIndex = 1;
                    break;

                default:
                    TextLanguageSelectedIndex = 0;
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

            LoginCommand = new RelayCommand(ExecuteLogIn);
            SignUpCommand = new RelayCommand(ExecuteSignUp);
            LogOutCommand = new RelayCommand(ExecuteLogOut);
            SaveCommand = new RelayCommand(Execute_Save);
            LanguageComboBoxSelectionChanged = new RelayCommand(ExecuteLanguageChanged);
            TextLanguageBoxSelectionChanged = new RelayCommand(ExecuteTextLanguageChanged);
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

        public int LanguageSelectedIndex
        {
            get { return (int)GetValue(LanguageSelectedIndexProperty); }
            set { SetValue(LanguageSelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty LanguageSelectedIndexProperty =
            DependencyProperty.Register("LanguageSelectedIndex", typeof(int), typeof(SettingsViewModel), new PropertyMetadata(0));

        public int TextLanguageSelectedIndex
        {
            get { return (int)GetValue(TextLanguageSelectedIndexProperty); }
            set { SetValue(TextLanguageSelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty TextLanguageSelectedIndexProperty =
            DependencyProperty.Register("TextLanguageSelectedIndex", typeof(int), typeof(SettingsViewModel), new PropertyMetadata(0));
        #endregion


        #region Commands
        public ICommand LoginCommand { get; }
        private void ExecuteLogIn(object? obj) => new Registration(TryLogIn, $"{Resources.RegLogIn}").ShowDialog();

        public ICommand SignUpCommand { get; }
        private void ExecuteSignUp(object? obj) => new Registration(CreateAccount, $"{Resources.RegSignUp}").ShowDialog();

        public ICommand LogOutCommand { get; }
        private void ExecuteLogOut(object? obj)
        {
            if (Properties.Settings.Default.UserName != "Guest")
            {
                MessageBox.Show($"{Properties.Settings.Default.UserName}, {Resources.AccountQuitMessage}", $"{Resources.Quit}",
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
                if (MessageBox.Show($"{Resources.SettingsSavedMessage}", $"{Resources.Saved}", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
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
            if (_languageComboBox != null)
            {
                string[] languageCodes = new[] { "en-US", "uk-UA" };
                Properties.Settings.Default.LanguageCode = languageCodes[_languageComboBox.SelectedIndex];
                Properties.Settings.Default.Save();
            }
        }

        public ICommand TextLanguageBoxSelectionChanged { get; }
        private void ExecuteTextLanguageChanged(object? obj)
        {
            if (_textLanguageComboBox != null)
            {
                string[] languageCodes = new[] { "en-US", "uk-UA" };
                Properties.Settings.Default.TextLanguageCode = languageCodes[_textLanguageComboBox.SelectedIndex];
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

        #region Private Methods
        private void TryLogIn(string login, string password)
        {
            var user = users.GetUserByLogin(login);

            if (user != null && user.Password == password)
            {
                Properties.Settings.Default.UserName = login;
                Properties.Settings.Default.Save();
            }
            else throw new ArgumentException($"{Resources.LoginError}");
        }
        public void CreateAccount(string login, string password)
        {
            var user = users.GetUserByLogin(login);

            if (user != null)
                throw new ArgumentException($"{login} {Resources.AccountExits}.");

            try
            {
                users.Add(new User { Login = login, Password = password});
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Properties.Settings.Default.UserName = login;
            Properties.Settings.Default.Save();
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
                TextColor = new SolidColorBrush(Color.FromRgb(245, 245, 245));
            }
            else
            {
                BackgroundColor = new SolidColorBrush(Color.FromRgb(245, 245, 245));
                TextColor = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            }
            SecondColor = new SolidColorBrush(Color.FromRgb(142, 171, 175));
        }
        #endregion
    }
}
