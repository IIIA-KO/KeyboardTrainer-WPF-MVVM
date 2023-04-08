using KeyboardTrainerWPF.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyboardTrainerWPF.MVVM.Views
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            switch(Properties.Settings.Default.LanguageCode)
            {
                case "uk-UA":
                    comboBoxLanguageSelect.SelectedIndex = 1;
                    break;

                default:
                    comboBoxLanguageSelect.SelectedIndex = 0;
                    break;
            }
            comboBoxLanguageSelect.SelectionChanged += comboBoxLanguageSelect_SelectionChanged;
        }

        private void comboBoxLanguageSelect_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox)
            {
                string[] languageCodes = new[] { "en-US", "uk-UA" };
                Properties.Settings.Default.LanguageCode = languageCodes[comboBoxLanguageSelect.SelectedIndex];
                Properties.Settings.Default.Save();
            }
        }
    }
}
