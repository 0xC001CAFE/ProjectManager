using System;

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
