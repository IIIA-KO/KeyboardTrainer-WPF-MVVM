using KeyboardTrainerWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    internal class MainViewModel : DependencyObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand RecordsTableViewCommand { get; set; }
        public RelayCommand TrainViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public RecordsTableViewModel RecordsTableVM { get; set; }
        public TrainViewModel TrainVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }


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
        }
    }
}
