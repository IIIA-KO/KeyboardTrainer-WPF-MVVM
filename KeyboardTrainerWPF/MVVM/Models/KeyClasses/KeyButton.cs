using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
                BorderBrush = new SolidColorBrush(Colors.White),
                BorderThickness = new Thickness(1.5),
                Background = new SolidColorBrush(Color.FromArgb(1, 27, 38, 44)),
                CornerRadius = new CornerRadius(15)
            };

            var text = new TextBlock
            {
                Text = lower,
                FontSize = 24.0,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontFamily = new System.Windows.Media.FontFamily("/Fonts/Nunito_Sans/#NunitoSans"),
                FontWeight = FontWeights.Bold
            };

            text.Foreground = Brushes.White;

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