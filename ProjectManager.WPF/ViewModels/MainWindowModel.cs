using ProjectManager.WPF.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
    public class MainWindowModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; private set; }

        public MainWindowModel(IMessenger messenger) : base(messenger)
        {
            CurrentViewModel = new MainAppViewModel(messenger);
        }
    }
}
