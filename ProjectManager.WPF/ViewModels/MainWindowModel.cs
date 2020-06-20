using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.ViewModels.Locator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
    public class MainWindowModel : ViewModelBase
    {
        #region Properties for data binding

        public ViewModelBase CurrentViewModel { get; private set; }

        private string overlayMessage;
        public string OverlayMessage
        {
            get => overlayMessage;
            private set
            {
                overlayMessage = value;

                OnPropertyChanged(nameof(OverlayMessage));
            }
        }

        #endregion

        public MainWindowModel(IMessenger messenger,
                               IViewModelLocator viewModelLocator) : base(messenger, viewModelLocator)
        {
            CurrentViewModel = viewModelLocator.MainAppViewModel();

            #region Messenger

            messenger.Subscribe<ExceptionOccurredMessage>(message =>
            {
                OverlayMessage = message.Exception.Message;
            });

            #endregion
        }
    }
}
