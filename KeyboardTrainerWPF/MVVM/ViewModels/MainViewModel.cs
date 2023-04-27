using KeyboardTrainerModel.Interfaces;
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
        #region Private Fields
        private readonly IUserService users;
        private readonly IScoreService scores;
        private readonly ITextService texts;
        #endregion

        #region Public Commands
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand RecordsTableViewCommand { get; set; }
        public RelayCommand TrainViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        #endregion

        #region Public Fields
        public HomeViewModel HomeVM { get; set; }
        public RecordsTableViewModel RecordsTableVM { get; set; }
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
        public MainViewModel(IUserService userService, IScoreService scoreService, ITextService textService)
        {
            users = userService;
            scores = scoreService;
            texts = textService;

            HomeVM = new HomeViewModel(users, scores, texts);
            RecordsTableVM = new RecordsTableViewModel();
            SettingsVM = new SettingsViewModel(users, scores, texts);

            CurrentView = HomeVM;
            User = Properties.Settings.Default.UserName;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVM;
            });

            RecordsTableViewCommand = new RelayCommand(x =>
            {
                CurrentView = RecordsTableVM;
            });;

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