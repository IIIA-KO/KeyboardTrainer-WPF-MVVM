using KeyboardTrainerWPF.MVVM.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace KeyboardTrainerWPF.MVVM.Views
{
    public partial class Registration : Window
    {
        public Registration(Action<string, string> action, string actionName)
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel(login, password, action, actionName);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
