﻿using KeyboardTrainerWPF.Core;
using KeyboardTrainerWPF.Properties.Languages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    public class RegistrationViewModel : DependencyObject, ISetAppereance
    {
        #region Private Fields
        private readonly Action<string, string> _action;
        private readonly TextBox _login;
        private readonly PasswordBox _password;
        #endregion

        #region Public Constructors
        public RegistrationViewModel()
        {
            _login = new TextBox();
            _password = new PasswordBox();

            OKCommand = new RelayCommand(ExecuteOK, CanExecuteOK);
            SetAppereance(Properties.Settings.Default.DarkTheme);
        }

        public RegistrationViewModel(TextBox login, PasswordBox password, Action<string, string> action, string actionName)
        {
            _action = action;
            _login = login;
            _password = password;

            ActionName = actionName;

            OKCommand = new RelayCommand(ExecuteOK, CanExecuteOK);
            SetAppereance(Properties.Settings.Default.DarkTheme);
        }
        #endregion

        #region Dependency Properties
        public string ActionName
        {
            get { return (string)GetValue(ActionNameProperty); }
            set { SetValue(ActionNameProperty, value); }
        }
        public static readonly DependencyProperty ActionNameProperty =
            DependencyProperty.Register("ActionName", typeof(string), typeof(RegistrationViewModel), new PropertyMetadata(string.Empty));
        #endregion

        #region Commands
        public ICommand OKCommand { get; }
        private void ExecuteOK(object? obj)
        {
            if (obj is Window window)
            {
                try
                {
                    _action?.Invoke(_login?.Text ?? "", _password?.Password ?? "");

                    MessageBox.Show(
                        ActionName == Resources.RegLogIn ?
                        Resources.AccountLoginSuccess :
                        Resources.AccountSigninSuccess
                        , "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Properties.Settings.RestartToSave();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, $"{Resources.AccountNotFound}", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private bool CanExecuteOK(object? obj)
        {
            return _password.Password.Count(i => i < 32 && i > 126) == 0
                && _password.Password.Length >= 8
                && _password.Password.Any(char.IsUpper)
                && _password.Password.Any(char.IsDigit)
                && _login.Text.Length >= 4
                && _login.Text.Length <= 16
                && _login.Text.ToLower() != "guest";
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
                LinearGradientBrush gradientBrush = new LinearGradientBrush();
                gradientBrush.StartPoint = new Point(0, 0);
                gradientBrush.EndPoint = new Point(0, 1);

                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(38, 50, 56), 0));
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(66, 83, 92), 0.5));
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(90, 112, 125), 1));
                BackgroundColor = gradientBrush;
                TextColor = new SolidColorBrush(Color.FromRgb(245, 245, 245));
            }
            else
            {
                LinearGradientBrush gradientBrush = new LinearGradientBrush();
                gradientBrush.StartPoint = new Point(0, 0);
                gradientBrush.EndPoint = new Point(0, 1);

                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(245, 245, 245), 0));
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(215, 215, 215), 0.5));
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(185, 185, 185), 1));
                BackgroundColor = gradientBrush;

                TextColor = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            }
            SecondColor = new SolidColorBrush(Color.FromRgb(142, 171, 175));
        }
        #endregion
    }
}
