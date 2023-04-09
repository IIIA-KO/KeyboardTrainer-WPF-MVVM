using KeyboardTrainerWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    internal class MainViewModel : DependencyObject, ISetAppereance
    {
        #region Puvblic Commands
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand RecordsTableViewCommand { get; set; }
        public RelayCommand TrainViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        #endregion

        #region Public Fields
        public HomeViewModel HomeVM { get; set; }
        public RecordsTableViewModel RecordsTableVM { get; set; }
        public TrainViewModel TrainVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        #endregion


        #region Dependency Properties
        public string User
        {
            get { return (string)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(string), typeof(MainViewModel), new PropertyMetadata(Properties.Settings.Default.UserName));

        public object CurrentView
        {
            get { return (object)GetValue(CurrentViewProperty); }
            set 
            { 
                SetValue(CurrentViewProperty, value);
                User = Properties.Settings.Default.UserName; 
            }
        }
        public static readonly DependencyProperty CurrentViewProperty =
            DependencyProperty.Register("CurrentView", typeof(object), typeof(MainViewModel), new PropertyMetadata(0));
        #endregion


        #region Public Constractor
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            RecordsTableVM = new RecordsTableViewModel();
            TrainVM= new TrainViewModel();
            SettingsVM = new SettingsViewModel();

            CurrentView = HomeVM;
            User = Properties.Settings.Default.UserName;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVM;
            });

            RecordsTableViewCommand = new RelayCommand(x => 
            { 
                CurrentView = RecordsTableVM;
            });

            TrainViewCommand = new RelayCommand(x =>
            {
                CurrentView = TrainVM;
            });

            SettingsViewCommand = new RelayCommand(x =>
            {
                CurrentView = SettingsVM;
            });
            SetAppereance(Properties.Settings.Default.DarkTheme);
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
