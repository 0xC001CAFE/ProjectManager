using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Services;
using ProjectManager.MongoDB;
using ProjectManager.MongoDB.Services;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Repositories;
using ProjectManager.WPF.ViewModels;
using ProjectManager.WPF.ViewModels.Locator;
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
            services.AddSingleton<IViewModelLocator, ViewModelLocator>();
            services.AddSingleton<IDatabaseAccess>(provider =>
            {
                return new DatabaseAccess("ProjectManager");
            });
            services.AddSingleton<IProjectDataService, ProjectDataService>();
            services.AddSingleton<IProjectRepository, ProjectRepository>();

            #endregion

            #region View models

            services.AddSingleton<MainWindowModel>();
            services.AddSingleton<MainAppViewModel>();
            services.AddSingleton<ProjectViewModel>();
            services.AddSingleton<EditableProjectViewModel>();
            services.AddSingleton<EditableTaskViewModel>();

            services.AddSingleton<ViewModelFactory<MainAppViewModel>>(provider =>
            {
                return () => provider.GetRequiredService<MainAppViewModel>();
            });
            services.AddSingleton<ViewModelFactory<ProjectViewModel>>(provider =>
            {
                return () => provider.GetRequiredService<ProjectViewModel>();
            });
            services.AddSingleton<ViewModelFactory<EditableProjectViewModel>>(provider =>
            {
                return () => provider.GetRequiredService<EditableProjectViewModel>();
            });
            services.AddSingleton<ViewModelFactory<EditableTaskViewModel>>(provider =>
            {
                return () => provider.GetRequiredService<EditableTaskViewModel>();
            });

            #endregion

            #region Views

            services.AddSingleton(provider =>
            {
                return new MainWindow(provider.GetRequiredService<MainWindowModel>());
            });

            #endregion

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
