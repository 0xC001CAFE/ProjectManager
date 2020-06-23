using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Models;
using ProjectManager.WPF.ViewModels.Locator;

namespace ProjectManager.WPF.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected readonly IMessenger messenger;
        protected readonly IViewModelLocator viewModelLocator;

        public ViewModelBase(IMessenger messenger, IViewModelLocator viewModelLocator)
        {
            this.messenger = messenger;
            this.viewModelLocator = viewModelLocator;
        }
    }
}
