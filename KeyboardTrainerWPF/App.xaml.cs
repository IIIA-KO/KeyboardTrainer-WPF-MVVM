using KeyboardTrainerDAL;
using KeyboardTrainerModel.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using KeyboardTrainerService;
using System.Threading;
using System.Windows;
using Text = KeyboardTrainerModel.Text;
using System.Collections.Generic;
using KeyboardTrainerModel;
using System.Windows.Controls;

namespace KeyboardTrainerWPF
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        public IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(KeyboardTrainerWPF.Properties.Settings.Default.LanguageCode);
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection()
                .AddScoped<DataContext>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IScoreService, ScoreService>()
                .AddSingleton<ITextService, TextService>()
                .AddSingleton<MainWindow>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
            Services = _serviceProvider;

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}