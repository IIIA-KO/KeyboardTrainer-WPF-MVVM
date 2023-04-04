using System.Globalization;
using System.Threading;
using System.Windows;

namespace KeyboardTrainerWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var languageCode = KeyboardTrainerWPF.Properties.Settings.Default.LanguageCode;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(languageCode);
            base.OnStartup(e);
        }
    }
}