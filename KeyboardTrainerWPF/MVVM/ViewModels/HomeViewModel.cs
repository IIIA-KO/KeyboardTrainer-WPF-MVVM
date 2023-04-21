﻿using KeyboardTrainerWPF.Core;
using KeyboardTrainerWPF.MVVM.Models.KeyClasses;
using KeyboardTrainerWPF.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using KeyboardTrainerWPF.Properties.Languages;
using KeyboardTrainerModel.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    public class HomeViewModel : DependencyObject, ISetAppereance
    {
        #region Private Fields
        private readonly IUserService users;
        private readonly IScoreService scores;
        private readonly ITextService texts;

        private Dictionary<Key, KeyButton> _keyboardButtons;
        private TextBox _textBox;
        private ProgressBar _progressBar;
        private string _text;
        private DispatcherTimer _speedTracker = new() { Interval = TimeSpan.FromMilliseconds(1000) };
        private DateTime _startTime;
        public double _complexity = Properties.Settings.Default.Complexity;
        #endregion

        #region Public Constructors
        public HomeViewModel(IUserService userService, IScoreService scoreService, ITextService textService)
        {
            users = userService;
            scores = scoreService;
            texts = textService;

            _keyboardButtons = new Dictionary<Key, KeyButton>();
            StartCommand = new RelayCommand(ExecuteStart);
            KeyDownCommand = new RelayCommand(ExecuteKeyDown, CanExecuteKeys);
            KeyUpCommand = new RelayCommand(ExecuteKeyUp, CanExecuteKeys);
            ProgressbarValueChangedCommand = new RelayCommand(ExecuteProgressbarValueChanged);

            _text = "";

            SetAppereance(Properties.Settings.Default.DarkTheme);
        }

        public HomeViewModel(Dictionary<Key, KeyButton> keyboard, TextBox textBox, ProgressBar bar)
        {
            var serviceProvider = ((App)Application.Current).Services;
            if (serviceProvider == null) throw new NullReferenceException(nameof(serviceProvider));
            users = serviceProvider.GetService<IUserService>();
            scores = serviceProvider.GetService<IScoreService>();
            texts = serviceProvider.GetService<ITextService>();

            _keyboardButtons = keyboard;
            _textBox = textBox;
            _progressBar = bar;
            _speedTracker.Tick += SpeedTracking;

            StartCommand = new RelayCommand(ExecuteStart);
            KeyDownCommand = new RelayCommand(ExecuteKeyDown, CanExecuteKeys);
            KeyUpCommand = new RelayCommand(ExecuteKeyUp, CanExecuteKeys);
            ProgressbarValueChangedCommand = new RelayCommand(ExecuteProgressbarValueChanged);

            _text = "";

            SetAppereance(Properties.Settings.Default.DarkTheme);
        }
        #endregion

        #region DependencyProperties
        public string StartOrStop
        {
            get { return (string)GetValue(StartOrStopProperty); }
            set { SetValue(StartOrStopProperty, value); }
        }
        public static readonly DependencyProperty StartOrStopProperty =
            DependencyProperty.Register("StartOrStop", typeof(string), typeof(HomeViewModel), new PropertyMetadata($"{Resources.Start}"));

        public bool IsStarted
        {
            get { return (bool)GetValue(IsStartedProperty); }
            set { SetValue(IsStartedProperty, value); }
        }
        public static readonly DependencyProperty IsStartedProperty =
            DependencyProperty.Register("IsStarted", typeof(bool), typeof(HomeViewModel), new PropertyMetadata(false));

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(HomeViewModel), new PropertyMetadata(0.0));

        public int Fails
        {
            get { return (int)GetValue(FailsProperty); }
            set { SetValue(FailsProperty, value); }
        }
        public static readonly DependencyProperty FailsProperty =
            DependencyProperty.Register("Fails", typeof(int), typeof(HomeViewModel), new PropertyMetadata(0));

        public string? User
        {
            get { return (string)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(string), typeof(HomeViewModel), new PropertyMetadata(Properties.Settings.Default.UserName));
        #endregion

        #region Public Commands
        public ICommand StartCommand { get; }
        private void ExecuteStart(object? obj)
        {
            if (!IsStarted)
            {
                IsStarted = true;
                UpdateKeyboard();
                Fails = 0;
                Speed = 0.0;
                _text = string.Empty;
                _textBox.Text = DataRecorder.GetText(_complexity);
                _textBox.Focus();
                _progressBar.Maximum = _textBox.Text.Length;
                _progressBar.Value = 0;
                StartOrStop = $"{Resources.Stop}";
                _startTime = DateTime.Now;
                _speedTracker.Start();
            }
            else Stop();
        }

        public ICommand KeyDownCommand { get; }
        private void ExecuteKeyDown(object? obj)
        {
            if (obj is KeyEventArgs e && _keyboardButtons.ContainsKey(e.Key))
            {
                Key key = e.Key;
                _keyboardButtons[key].KeyGrid.Background = SecondColor;

                switch (key)
                {
                    case Key.LeftCtrl:
                    case Key.RightCtrl:
                    case Key.LWin:
                    case Key.RWin:
                    case Key.RightAlt:
                    case Key.LeftAlt:
                    case Key.Back:
                        return;

                    case Key.LeftShift:
                    case Key.RightShift:
                    case Key.CapsLock:
                        UpdateKeyboard();
                        return;

                    case Key.Space:
                        Type(' ');
                        return;
                    case Key.Tab:
                        Type('\t');
                        return;
                    case Key.Enter:
                        Type('\n');
                        return;
                    default:
                        Type(char.Parse(_keyboardButtons[key].Content.Text as string ?? " "));
                        return;
                }
            }
        }
        public ICommand KeyUpCommand { get; }
        private void ExecuteKeyUp(object? obj)
        {
            if (obj is KeyEventArgs e && _keyboardButtons.ContainsKey(e.Key))
            {
                _keyboardButtons[e.Key].KeyGrid.Background = Brushes.Transparent;
                UpdateKeyboard();
            }
        }
        private bool CanExecuteKeys(object? obj) => IsStarted;

        public ICommand ProgressbarValueChangedCommand { get; }
        private void ExecuteProgressbarValueChanged(object? obj)
        {
            if (_progressBar.Value == _progressBar.Maximum)
            {
                Stop();
                TimeSpan duration = _startTime - DateTime.Now;
                MessageBox.Show(
                    $"Fails: {Fails}\nSpeed: {Speed}", "Game over!",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                if (User != "Guest")
                    DataRecorder.WriteResaults(new UserScoreModel(User, _startTime, duration, Fails, Speed, _complexity));
                _progressBar.Value = 0;
            }
        }
        #endregion

        #region Private Methods
        private void UpdateKeyboard()
        {
            try
            {
                bool shiftIsOn = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
                bool capsIsOn = Keyboard.IsKeyToggled(Key.CapsLock);
                foreach (KeyButton keyboardButton in _keyboardButtons.Values)
                {
                    keyboardButton.UpdateValue(capsIsOn, shiftIsOn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Type(char character)
        {
            if (_textBox.Text[0] != character)
            {
                Fails++;
                _textBox.Foreground = Brushes.Red;
                return;
            }

            _text += character;
            _textBox.Foreground = Brushes.Black;
            _textBox.Text = _textBox.Text.Remove(0, 1);
            _progressBar.Value++;
        }

        private void Stop()
        {
            _speedTracker.Stop();
            _textBox.Text = string.Empty;
            _progressBar.Value = 0;
            Speed = 0;
            Fails = 0;
            IsStarted = false;
            StartOrStop = $"{Resources.Start}";
        }
        private void SpeedTracking(object? sender, EventArgs e) =>
            Speed = 60 * Math.Round(_text.Length / (DateTime.Now - _startTime).TotalSeconds);
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