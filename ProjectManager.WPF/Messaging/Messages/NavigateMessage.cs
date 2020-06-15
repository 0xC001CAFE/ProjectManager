using ProjectManager.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.Messaging.Messages
{
    public class NavigateMessage<T> where T : ViewModelBase
    {
        public T ViewModel { get; private set; }

        public NavigateMessage(T viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
