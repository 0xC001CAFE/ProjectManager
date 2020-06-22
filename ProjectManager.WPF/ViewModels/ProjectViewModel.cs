using ProjectManager.Domain.Models;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.Models;
using ProjectManager.WPF.ViewModels.Locator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        #region Properties for data binding

        private ProjectModel project;
        public ProjectModel Project
        {
            get => project;
            private set
            {
                project = value;

                OnPropertyChanged(nameof(Project));
            }
        }

        private ProjectTaskModel selectedTask;
        public ProjectTaskModel SelectedTask
        {
            get => selectedTask;
            set
            {
                selectedTask = value;

                messenger.Send(new SelectionChangedMessage<ProjectTaskModel>(selectedTask));
            }
        }

        #endregion

        public ProjectViewModel(IMessenger messenger,
                                IViewModelLocator viewModelLocator) : base(messenger, viewModelLocator)
        {
            #region Messenger

            messenger.Subscribe<SelectionChangedMessage<ProjectModel>>(message =>
            {
                Project = message.SelectedElement;
            });

            #endregion
        }
    }
}
