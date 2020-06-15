using Microsoft.Extensions.DependencyInjection;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.ViewModels;
using ProjectManager.WPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        protected override void OnStartup(StartupEventArgs eventArgs)
        {
            base.OnStartup(eventArgs);

            ConfigureServices();

            var window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            #region Services

            services.AddSingleton<IMessenger, Messenger>();

            #endregion

            #region View models

            services.AddSingleton<MainWindowModel>();

            #endregion

            #region Views

            services.AddSingleton(provider => new MainWindow(provider.GetRequiredService<MainWindowModel>()));

            #endregion

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
