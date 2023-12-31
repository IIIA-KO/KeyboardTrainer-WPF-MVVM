﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace KeyboardTrainerWPF.MVVM.Models.KeyClasses
{
    public class KeyButton
    {
        #region Public Properties
        public Key KeyValue { get; private set; }
        public string? LowerValue { get; private set; }
        public string? UpperValue { get; private set; }

        public Border KeyGrid { get; private set; }
        public TextBlock Content { get; private set; }
        #endregion

        #region Base Constructor for derived Key Classes
        public KeyButton(string? lower, string? upper, int row, int col, int colSpan, Key key)
        {
            LowerValue = lower;
            UpperValue = upper;
            KeyValue = key;

            var border = new Border
            {
                Margin = new Thickness(2.0),
                BorderBrush = Properties.Settings.Default.DarkTheme == true ? new SolidColorBrush(Color.FromRgb(245, 245, 245)) : new SolidColorBrush(Color.FromRgb(33, 33, 33)),
                BorderThickness = new Thickness(1.5),
                Background = Properties.Settings.Default.DarkTheme == true ? new SolidColorBrush(Color.FromRgb(38, 50, 56)) : new SolidColorBrush(Color.FromRgb(245, 245, 245)),
                CornerRadius = new CornerRadius(15)
            };

            var text = new TextBlock
            {
                Text = lower,
                FontSize = 24.0,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontFamily = new System.Windows.Media.FontFamily("/Fonts/Nunito_Sans/#NunitoSans"),
                Foreground = Properties.Settings.Default.DarkTheme == true ? new SolidColorBrush(Color.FromRgb(245, 245, 245)) : new SolidColorBrush(Color.FromRgb(33, 33, 33)),
                FontWeight = FontWeights.Bold
            };

            border.Child = text;
            Grid.SetRow(border, row);
            Grid.SetColumn(border, col);
            Grid.SetColumnSpan(border, colSpan);

            Content = text;
            KeyGrid = border;
        }
        #endregion

        #region Public Virtual Method
        public virtual void UpdateValue(bool capsIsPressed, bool shiftIsPressed) { }
        #endregion
    }
}