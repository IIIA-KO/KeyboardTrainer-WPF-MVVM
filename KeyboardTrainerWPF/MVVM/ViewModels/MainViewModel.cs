using KeyboardTrainerWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand RecordsTableViewCommand { get; set; }
        public RelayCommand TrainViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public RecordsTableViewModel RecordsTableVM { get; set; }
        public TrainViewModel TrainVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }


        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

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
