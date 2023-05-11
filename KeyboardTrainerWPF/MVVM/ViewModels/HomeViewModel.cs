using KeyboardTrainerModel;
using KeyboardTrainerModel.Interfaces;
using KeyboardTrainerWPF.Core;
using KeyboardTrainerWPF.MVVM.Models.KeyClasses;
using KeyboardTrainerWPF.Properties.Languages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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
        private User _currentUser;
        private Text _filteredText;
        private double _complexity;
        #endregion

        #region Public Constructors
        public HomeViewModel(IUserService userService, IScoreService scoreService, ITextService textService)
        {
            users = userService;
            scores = scoreService;
            texts = textService;

            _keyboardButtons = new Dictionary<Key, KeyButton>();
            _textBox = new TextBox();
            _progressBar = new ProgressBar();

            Initialize();
        }

        public HomeViewModel(Dictionary<Key, KeyButton> keyboard, TextBox textBox, ProgressBar bar)
        {
            try
            {
                var serviceProvider = ((App)Application.Current).Services;
                if (serviceProvider == null) throw new NullReferenceException(nameof(serviceProvider));
                users = serviceProvider.GetService<IUserService>();
                scores = serviceProvider.GetService<IScoreService>();
                texts = serviceProvider.GetService<ITextService>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _keyboardButtons = keyboard;
            _textBox = textBox;
            _progressBar = bar;

            Initialize();
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
        public ICommand StartCommand { get; private set; }
        private void ExecuteStart(object? obj)
        {
            if (!IsStarted)
            {
                IsStarted = true;
                UpdateKeyboard();
                Fails = 0;
                Speed = 0.0;
                _text = string.Empty;

                //Заготований текс для демонстрації на захисті курсової роботи
                //switch (Properties.Settings.Default.TextLanguageCode)
                //{
                //    case "uk-UA":
                //        _filteredText = texts.GetTextById(32);
                //        break;

                //    default:
                //        _filteredText = texts.GetTextById(31);
                //        break;
                //}

                try
                {
                    _filteredText = texts.GetTextByLanguageCode(Properties.Settings.Default.TextLanguageCode)
                                         .Where(t => t.Complexity == Properties.Settings.Default.Complexity)
                                         .OrderBy(x => Random.Shared.Next())
                                         .FirstOrDefault();
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message, "Error. Null Reference Exception: Could not get data from database", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _textBox.Text = _filteredText.TextContent;
                _textBox.Focus();
                _progressBar.Maximum = _textBox.Text.Length;
                _progressBar.Value = 0;
                StartOrStop = $"{Resources.Stop}";
                _startTime = DateTime.Now;
                _speedTracker.Start();
            }
            else Stop();
        }

        public ICommand KeyDownCommand { get; private set; }
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
                    case Key.LeftAlt:
                    case Key.RightAlt:
                    case Key.Back:
                    case Key.Tab:
                    case Key.Enter:
                        return;

                    case Key.LeftShift:
                    case Key.RightShift:
                    case Key.CapsLock:
                        UpdateKeyboard();
                        return;

                    case Key.Space:
                        Type(' ');
                        return;

                    default:
                        Type(char.Parse(_keyboardButtons[key].Content.Text));
                        return;
                }
            }
        }
        public ICommand KeyUpCommand { get; private set; }
        private void ExecuteKeyUp(object? obj)
        {
            if (obj is KeyEventArgs e && _keyboardButtons.ContainsKey(e.Key))
            {
                _keyboardButtons[e.Key].KeyGrid.Background = Brushes.Transparent;
                UpdateKeyboard();
            }
        }
        private bool CanExecuteKeys(object? obj) => IsStarted;

        public ICommand ProgressbarValueChangedCommand { get; private set; }
        private void ExecuteProgressbarValueChanged(object? obj)
        {
            if (_progressBar.Value == _progressBar.Maximum)
            {
                _speedTracker.Stop();

                TimeSpan duration = _startTime - DateTime.Now;
                MessageBox.Show(
                    $"{Properties.Languages.Resources.Fails}: {Fails}\n{Properties.Languages.Resources.Speed}: {Speed}", "Game over!",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                if (User != "Guest")
                {
                    var score = new Score
                    {
                        User = _currentUser,
                        Text = _filteredText,
                        Complexity = _complexity,
                        Duration = duration,
                        Fails = this.Fails,
                        SessionBeginning = _startTime,
                        Speed = this.Speed,
                        Accuracy = Math.Round((1 - ((double)this.Fails / (double)_filteredText.TextContent.Length)) * 100, 2)
                    };
                    scores.Add(score);
                }
                Stop();
            }
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            _text = "";
            _speedTracker.Tick += SpeedTracking;
            _startTime = DateTime.Now;
            if (User != "Guest")
            {
                _currentUser = users.GetUserByLogin(User);
            }

            _complexity = Properties.Settings.Default.Complexity;

            StartCommand = new RelayCommand(ExecuteStart);
            KeyDownCommand = new RelayCommand(ExecuteKeyDown, CanExecuteKeys);
            KeyUpCommand = new RelayCommand(ExecuteKeyUp, CanExecuteKeys);
            ProgressbarValueChangedCommand = new RelayCommand(ExecuteProgressbarValueChanged);

            SetAppereance(Properties.Settings.Default.DarkTheme);
        }

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

                if (Properties.Settings.Default.ErrorSound)
                    System.Media.SystemSounds.Exclamation.Play();
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
            _progressBar.Value = 0;
            Speed = 0;
            Fails = 0;
            _textBox.Text = string.Empty;
            _textBox.Foreground = Brushes.Black;
            IsStarted = false;
            StartOrStop = $"{Resources.Start}";
        }
        private void SpeedTracking(object? sender, EventArgs e) =>
            Speed = 60 * Math.Round((_text.Length / (DateTime.Now - _startTime).TotalSeconds), 1);
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