using ProjectManager.WPF.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
    public class MainWindowModel : ViewModelBase
    {
        #region Properties for data binding

        public ViewModelBase CurrentViewModel { get; private set; }

        #endregion

        public MainWindowModel(IMessenger messenger) : base(messenger)
        {
            CurrentViewModel = new MainAppViewModel(messenger);
        }
    }
}
