using ProjectManager.Domain.Models;
using ProjectManager.WPF.Commands;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.ViewModels.Locator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectManager.WPF.ViewModels
{
    public class EditableProjectViewModel : ViewModelBase
    {
        #region Properties for data binding

        private Project editableProject;
        public Project EditableProject
        {
            get => editableProject;
            private set
            {
                editableProject = value;

                OnPropertyChanged(nameof(EditableProject));
            }
        }

        public ICommand CancelCommand { get; private set; }

        #endregion

        public EditableProjectViewModel(IMessenger messenger,
                                        IViewModelLocator viewModelLocator) : base(messenger, viewModelLocator)
        {
            CancelCommand = new RelayCommand(() =>
            {
                messenger.Send(new NavigateMessage<ProjectViewModel>(viewModelLocator.ProjectViewModel()));
            });
        }
    }
}
