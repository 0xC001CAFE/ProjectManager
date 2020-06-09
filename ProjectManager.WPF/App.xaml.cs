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
        protected override void OnStartup(StartupEventArgs eventArgs)
        {
            var window = new MainWindow
            {
                DataContext = new MainWindowModel()
            };
            window.Show();

            base.OnStartup(eventArgs);
        }
    }
}
