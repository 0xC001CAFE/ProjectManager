using ProjectManager.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.Messaging.Messages
{
    public class NavigateMessage
    {
        public Type ViewModel { get; }

        public NavigateMessage(Type viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
