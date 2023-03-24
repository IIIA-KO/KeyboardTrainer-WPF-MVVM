using KeyboardTrainerWPF.Core;
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
    public class RegistrationViewModel : DependencyObject
    {
        #region Private Fields
        private Action<string, string>? _action;
        private TextBox? _login;
        private PasswordBox? _password;
        #endregion

        #region Public Constructor
        public RegistrationViewModel(TextBox? login, PasswordBox? password, Action<string, string>? action)
        {
            _action = action;
            _login = login;
            _password = password;

            OKCommand = new RelayCommand(Execute_OK, CanExecute_OK);
            CloseCommand = new RelayCommand(Execute_Close);
        }
        #endregion

        #region Commands
        public ICommand OKCommand { get; }
        private void Execute_OK(object? obj)
        {
            if (obj is Window window)
            {
                try
                {
                    _action?.Invoke(_login?.Text ?? "", _password?.Password ?? "");
                    window.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Account hasn't been found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private bool CanExecute_OK(object? obj)
            => _password?.Password.Count(i => i < 32 && i > 126) == 0 && _password.Password.Length > 6 && _login?.Text.Length >= 4 && _login.Text != "Guest";

        public ICommand CloseCommand { get; }
        private void Execute_Close(object? obj)
        {
            if (obj is Window window)
                window.Close();
        }
        #endregion
    }
}
