using System.Globalization;
using System.Threading;
using System.Windows;

namespace KeyboardTrainerWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(KeyboardTrainerWPF.Properties.Settings.Default.LanguageCode);
            base.OnStartup(e);
        }
    }
}