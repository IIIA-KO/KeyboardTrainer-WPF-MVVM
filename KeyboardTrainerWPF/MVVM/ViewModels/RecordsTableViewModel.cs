﻿using KeyboardTrainerModel;
using KeyboardTrainerModel.Interfaces;
using KeyboardTrainerWPF.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    public class RecordsTableViewModel : DependencyObject, ISetAppereance
    {
        #region Private Fields
        private readonly IScoreService scores;
        #endregion

        #region Public Constructors
        public RecordsTableViewModel()
        {
            try
            {
                var serviceProvider = ((App)Application.Current).Services;
                if (serviceProvider == null) throw new NullReferenceException(nameof(serviceProvider));
                scores = serviceProvider.GetService<IScoreService>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Initialize();
        }
        public RecordsTableViewModel(IScoreService scoreService)
        {
            scores = scoreService;

            Initialize();
        }
        #endregion

        #region DependencyProperties
        public ICollectionView ScoresCollection
        {
            get { return (ICollectionView)GetValue(ScoresCollectionProperty); }
            set { SetValue(ScoresCollectionProperty, value); }
        }
        public static readonly DependencyProperty ScoresCollectionProperty =
            DependencyProperty.Register("ScoresCollection", typeof(ICollectionView), typeof(RecordsTableViewModel), new PropertyMetadata(null));
        private ObservableCollection<Score> ScoresList;


        public string SearchByAnyField
        {
            get { return (string)GetValue(SearchByAnyFieldProperty); }
            set { SetValue(SearchByAnyFieldProperty, value); }
        }
        public static readonly DependencyProperty SearchByAnyFieldProperty =
            DependencyProperty.Register("SearchByAnyField", typeof(string), typeof(RecordsTableViewModel), new PropertyMetadata("", AllFieldsFilterChanged));
        private static void AllFieldsFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RecordsTableViewModel This)
            {
                This.ScoresCollection.Filter = null;
                This.ScoresCollection.Filter = This.PredicateByAnyField;
            }
        }
        private bool PredicateByAnyField(object obj)
        {
            if (obj is Score score)
                return score.ToString().Contains(SearchByAnyField);
            return false;
        }
        #endregion

        #region Commands
        public ICommand RemoveScoreCommand { get; private set; }
        private void ExecuteRemoveScore(object? obj)
        {
            Score score = obj as Score;
            if (score != null)
            {
                try
                {
                    if (MessageBox.Show(Properties.Languages.Resources.RecordsDeleteQuestion, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        ScoresList.Remove(score);
                        scores.Delete(s => s.Id == score.Id, true);
                    }
                    else return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unexpected Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        private bool CanExecuteRemoveScore(object? obj)
        {
            Score score = obj as Score;
            if (score != null)
            {
                return ScoresList.Count > 0 && score.User.Login == Properties.Settings.Default.UserName;
            }
            return false;
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            SearchByAnyField = "";
            ScoresList = new ObservableCollection<Score>(scores.Search(s => s != null, true));
            ScoresCollection = CollectionViewSource.GetDefaultView(ScoresList);
            ScoresCollection.Filter = PredicateByAnyField;

            RemoveScoreCommand = new RelayCommand(ExecuteRemoveScore, CanExecuteRemoveScore);

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
