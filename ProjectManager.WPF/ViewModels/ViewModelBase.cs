using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected readonly IMessenger messenger;

        public ViewModelBase(IMessenger messenger)
        {
            this.messenger = messenger;
        }
    }
}
