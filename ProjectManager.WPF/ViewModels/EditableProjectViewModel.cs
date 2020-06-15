using ProjectManager.Domain.Models;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.ViewModels.Locator;
using System;
using System.Collections.Generic;
using System.Text;

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

        #endregion

        public EditableProjectViewModel(IMessenger messenger,
                                        IViewModelLocator viewModelLocator) : base(messenger, viewModelLocator)
        {

        }
    }
}
