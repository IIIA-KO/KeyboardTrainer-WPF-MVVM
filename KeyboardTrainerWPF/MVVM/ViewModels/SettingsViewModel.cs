using KeyboardTrainerWPF.Core;
using KeyboardTrainerWPF.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    class SettingsViewModel : DependencyObject
    {
        #region Public Constuctor
        public SettingsViewModel()
        {
            Complexity = Properties.Settings.Default.Complexity;
            LanguageCode = Properties.Settings.Default.LanguageCode;

            LoginCommand = new RelayCommand(Execute_LogIn);
            SignUpCommand = new RelayCommand(Execute_SignUp);
            LogOutCommand = new RelayCommand(Execute_LogOut);
            SaveCommand = new RelayCommand(Execute_Save);
            //LanguageComboBoxSelectionChanged = new RelayCommand(ExecuteLanguageChanged);
        }
        #endregion

        #region Dependency Properties
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
                if(MessageBox.Show("Settings were saved", "Saved!", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                {
                    var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
                    Process.Start(currentExecutablePath);
                    Application.Current.Shutdown();
                }

            }
        }

        //public ICommand LanguageComboBoxSelectionChanged { get; }
        //private void ExecuteLanguageChanged(object? obj)
        //{
        //    if (obj is ComboBox box)
        //    {
        //        string[] languageCodes = new[]
        //        {
        //            "en-US",
        //            "uk-UA"
        //        };
        //        Properties.Settings.Default.LanguageCode = languageCodes[box.SelectedIndex];
        //        Properties.Settings.Default.Save();
        //    }
        //}
        #endregion
    }
}
