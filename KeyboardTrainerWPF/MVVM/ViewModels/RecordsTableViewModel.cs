using KeyboardTrainerWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KeyboardTrainerWPF.MVVM.ViewModels
{
    class RecordsTableViewModel : DependencyObject, ISetAppereance
    {
        #region InterfaceImplementation
        public Brush TextColor { get; set; }
        public Brush SecondColor { get; set; }
        public Brush BackgroundColor { get; set; }
        public void SetAppereance(bool isDarkTheme)
        {
            if (isDarkTheme)
            {
                BackgroundColor = new SolidColorBrush(Color.FromRgb(38, 50, 56));
                TextColor = new SolidColorBrush(Color.FromRgb(236, 239, 241));
            }
            else
            {
                BackgroundColor = new SolidColorBrush(Color.FromRgb(207, 216, 220));
                TextColor = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            }
            SecondColor = new SolidColorBrush(Color.FromRgb(96, 125, 139));
            return;
        }
        #endregion
    }
}
