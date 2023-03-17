using KeyboardTrainerWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public object CurrentView
        {
            get { return (object)GetValue(CurrentViewProperty); }
            set { SetValue(CurrentViewProperty, value); }
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
