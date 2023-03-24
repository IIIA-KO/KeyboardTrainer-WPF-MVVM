using KeyboardTrainerWPF.Core;
using KeyboardTrainerWPF.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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

            LoginCommand = new RelayCommand(Execute_LogIn);
            SignUpCommand = new RelayCommand(Execute_SignUp);
            LogOutCommand = new RelayCommand(Execute_LogOut);
            SaveCommand = new RelayCommand(Execute_Save);
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
                MessageBox.Show("Settings were saved", "Saved!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Properties.Settings.Default.Save();
                //window.Close();
            }
        }
        #endregion
    }
}
