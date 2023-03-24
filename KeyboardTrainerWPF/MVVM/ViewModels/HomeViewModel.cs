using KeyboardTrainerWPF.MVVM.Models.KeyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    class HomeViewModel : DependencyObject
    {
        #region Private Fields
        private Dictionary<Key, KeyButton> _keyboardButtons;
        private Random _rand;
        private DateTime _startTime;
        private int _correctlyTypedTextLength;
        private int _fails;

        private TextBox? _textBox;
        private string? _text;
        #endregion

        #region Public Constructors
        public HomeViewModel()
        {
            _keyboardButtons = new Dictionary<Key, KeyButton>();
            _rand = new Random();
            _startTime = DateTime.Now;
            _correctlyTypedTextLength = 0;
            _fails = 0;
        }

        public HomeViewModel(Grid keyboard, TextBox textBox)
        {
            _keyboardButtons = new Dictionary<Key, KeyButton>();
            foreach (KeyButton keyButton in keyboard.Children)
            {
                _keyboardButtons.Add(keyButton.KeyValue, keyButton);
            }

            _rand = new Random();
            _startTime = DateTime.Now;
            _correctlyTypedTextLength = 0;
            _fails = 0;

            _textBox = textBox;
        }
        #endregion

        #region Public Commands
        public ICommand StartCommand { get; }
        public ICommand KeyUpCommand { get; }
        public ICommand KeyDownCommand { get; }
        #endregion

        #region DependencyProperties
        public string? StartOrStop
        {
            get { return (string?)GetValue(PlayOrStopProperty); }
            set { SetValue(PlayOrStopProperty, value); }
        }
        public static readonly DependencyProperty PlayOrStopProperty =
            DependencyProperty.Register("StartOrStop", typeof(string), typeof(MainViewModel), new PropertyMetadata("Play"));
        public SolidColorBrush PlayBackground
        {
            get { return (SolidColorBrush)GetValue(PlayBackgroundProperty); }
            set { SetValue(PlayBackgroundProperty, value); }
        }
        public static readonly DependencyProperty PlayBackgroundProperty =
            DependencyProperty.Register("PlayBackground", typeof(SolidColorBrush), typeof(MainViewModel), new PropertyMetadata(Brushes.Lime));

        public bool IsStarted
        {
            get { return (bool)GetValue(IsGameStartedProperty); }
            set { SetValue(IsGameStartedProperty, value); }
        }
        public static readonly DependencyProperty IsGameStartedProperty =
            DependencyProperty.Register("IsStarted", typeof(bool), typeof(MainViewModel), new PropertyMetadata(false));

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(MainViewModel), new PropertyMetadata(0.0));

        public int Fails
        {
            get { return (int)GetValue(FailsProperty); }
            set { SetValue(FailsProperty, value); }
        }
        public static readonly DependencyProperty FailsProperty =
            DependencyProperty.Register("Fails", typeof(int), typeof(MainViewModel), new PropertyMetadata(0));

        public string? User
        {
            get { return (string)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(string), typeof(MainViewModel), new PropertyMetadata(Properties.Settings.Default.UserName));
        #endregion
    }
}
