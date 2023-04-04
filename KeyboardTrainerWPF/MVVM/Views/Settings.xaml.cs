using KeyboardTrainerWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl, ISetLanguage
    {
        public Settings()
        {
            InitializeComponent();
            SetLanguage(Properties.Settings.Default.LanguageCode);
            comboBoxLanguageSelect.SelectionChanged += comboBoxLanguageSelect_SelectionChanged;
        }

        public void SetLanguage(string language)
        {
            switch (language)
            {
                case "uk-UA":
                    labelComplexity.Content = Properties.Languages.Ukrainian.Complexity;
                    labelTextLanguage.Content = Properties.Languages.Ukrainian.TextLanguage;
                    rbEnglish.Content = Properties.Languages.Ukrainian.EnglishLang;
                    rbUkrainian.Content = Properties.Languages.Ukrainian.UkrainianLang;
                    btnLogIn.Content = Properties.Languages.Ukrainian.LoginIn;
                    btnSignUp.Content = Properties.Languages.Ukrainian.SignUp;
                    btnLogOut.Content = Properties.Languages.Ukrainian.LogOut;
                    labelTheme.Content = Properties.Languages.Ukrainian.Theme;
                    rbDark.Content = Properties.Languages.Ukrainian.Dark;
                    rbLight.Content = Properties.Languages.Ukrainian.Light;
                    labelAppLanguage.Content = Properties.Languages.Ukrainian.AppLanguage;
                    btnSaveSettings.Content = Properties.Languages.Ukrainian.SaveSettings;
                    gbGeneral.Header = Properties.Languages.Ukrainian.General;
                    gbAccount.Header = Properties.Languages.Ukrainian.Account;
                    gbAppereance.Header = Properties.Languages.Ukrainian.Appereance;
                    break;

                default:
                    labelComplexity.Content = Properties.Languages.English.Complexity;
                    labelTextLanguage.Content = Properties.Languages.English.TextLanguage;
                    rbEnglish.Content = Properties.Languages.English.EnglishLang;
                    rbUkrainian.Content = Properties.Languages.English.UkrainianLang;
                    btnLogIn.Content = Properties.Languages.English.LoginIn;
                    btnSignUp.Content = Properties.Languages.English.SignUp;
                    btnLogOut.Content = Properties.Languages.English.LogOut;
                    labelTheme.Content = Properties.Languages.English.Theme;
                    rbDark.Content = Properties.Languages.English.Dark;
                    rbLight.Content = Properties.Languages.English.Light;
                    labelAppLanguage.Content = Properties.Languages.English.AppLanguage;
                    btnSaveSettings.Content = Properties.Languages.English.SaveSettings;
                    gbGeneral.Header = Properties.Languages.English.General;
                    gbAccount.Header = Properties.Languages.English.Account;
                    gbAppereance.Header = Properties.Languages.English.Appereance;
                    break;
            }
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
