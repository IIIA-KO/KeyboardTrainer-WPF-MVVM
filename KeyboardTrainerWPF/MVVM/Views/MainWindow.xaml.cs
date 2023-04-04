using KeyboardTrainerWPF.Core;
using System.Windows;
using System.Windows.Input;

namespace KeyboardTrainerWPF
{
    public partial class MainWindow : Window, ISetLanguage
    {
        public MainWindow()
        {
            InitializeComponent();
            SetLanguage(Properties.Settings.Default.LanguageCode);
        }
        public void SetLanguage(string language)
        {
            switch (language)
            {
                case "uk-UA":
                    HomeRadioButton.Content = Properties.Languages.Ukrainian.Home;
                    TrainingRadioButton.Content = Properties.Languages.Ukrainian.Training;
                    RecordsTableRadioButton.Content = Properties.Languages.Ukrainian.RecorsTable;
                    SettingsRadioButton.Content = Properties.Languages.Ukrainian.RecorsTable;
                    break;

                default:
                    HomeRadioButton.Content = Properties.Languages.English.Home;
                    TrainingRadioButton.Content = Properties.Languages.English.Training;
                    RecordsTableRadioButton.Content = Properties.Languages.English.RecorsTable;
                    SettingsRadioButton.Content = Properties.Languages.English.RecorsTable;
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private bool isMaximazed = false;
        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (isMaximazed)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1280;
                    this.Height = 720;
                    isMaximazed = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    isMaximazed = true;
                }
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
