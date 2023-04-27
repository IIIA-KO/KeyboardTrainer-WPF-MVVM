using System.Windows.Controls;
using KeyboardTrainerWPF.MVVM.ViewModels;

namespace KeyboardTrainerWPF.MVVM.Views
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel(comboBoxLanguageSelect, comboBoxTextLanguageSelect, darkThemeCheckBox);
        }
    }
}
